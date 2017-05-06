using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using CognitiveServicesBot.Services;
using System.Linq;

namespace CognitiveServicesBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            string resultMessage = "";

            if (activity.Attachments != null && activity.Attachments.Count == 1)
            {
                Attachment file = activity.Attachments[0];
                Stream imageStream = await GetAttachmentAsStream(file.ContentUrl);
                if (imageStream != null)
                {
                    string imageUrl = await StorageHelper.Instance.UploadFile(imageStream, file.Name, GetExtension(file.Name));
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        var AIResult = await CognitiveServices.Instance.DescribeImage(imageUrl);
                        if (AIResult != null)
                        {
                            resultMessage = AIResult.Description.Captions.FirstOrDefault()?.Text;
                        }
                        else
                        {
                            resultMessage = "Image describing has failed";
                        }
                    }
                    else
                        resultMessage = "Image uploading has failed";
                }
                else
                    resultMessage = "Image getting has failed";
            }

            await context.PostAsync(resultMessage);

            context.Wait(MessageReceivedAsync);
        }

        private async Task<Stream> GetAttachmentAsStream(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                return await response.Content?.ReadAsStreamAsync();
            }
        }

        private string GetExtension(string contentType)
        {
            Dictionary<string, string> contentTypes = new Dictionary<string, string>()
            {
                {"image/png",".png" },
                {"image/jpg",".jpg" },
                {"image/jpeg",".jpeg" }
            };
            if (contentTypes.ContainsKey(contentType))
                return contentTypes[contentType];
            return null;
        }
    }
}