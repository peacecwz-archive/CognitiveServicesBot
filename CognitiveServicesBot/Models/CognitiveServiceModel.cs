using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveServicesBot.Models
{

    public class CognitiveServiceModel
    {
        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }
        [JsonProperty("adult")]
        public Adult Adult { get; set; }
        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }
        [JsonProperty("description")]
        public Description Description { get; set; }
        [JsonProperty("requestId")]
        public string RequestId { get; set; }
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
        [JsonProperty("faces")]
        public List<Face> Faces { get; set; }
        [JsonProperty("color")]
        public Color Color { get; set; }
        [JsonProperty("imageType")]
        public Imagetype ImageType { get; set; }
    }

    public class Adult
    {
        [JsonProperty("isAdultContent")]
        public bool IsAdultContent { get; set; }
        [JsonProperty("isRacyContent")]
        public bool IsRacyContent { get; set; }
        [JsonProperty("adultScore")]
        public float AdultScore { get; set; }
        [JsonProperty("racyScore")]
        public float RacyScore { get; set; }
    }

    public class Description
    {
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        [JsonProperty("captions")]
        public List<Caption> Captions { get; set; }
    }

    public class Caption
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
    }

    public class Color
    {
        [JsonProperty("dominantColorForeground")]
        public string DominantColorForeground { get; set; }
        [JsonProperty("dominantColorBackground")]
        public string DominantColorBackground { get; set; }
        [JsonProperty("dominantColors")]
        public List<string> DominantColors { get; set; }
        [JsonProperty("accentColor")]
        public string AccentColor { get; set; }
        [JsonProperty("isBWImg")]
        public bool IsBWImg { get; set; }
    }

    public class Imagetype
    {
        [JsonProperty("clipArtType")]
        public int ClipArtType { get; set; }
        [JsonProperty("lineDrawingType")]
        public int LineDrawingType { get; set; }
    }

    public class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("score")]
        public float Score { get; set; }
        [JsonProperty("detail")]
        public Detail Detail { get; set; }
    }

    public class Detail
    {
        [JsonProperty("celebrities")]
        public List<Celebrity> Celebrities { get; set; }
    }

    public class Celebrity
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("faceRectangle")]
        public Facerectangle FaceRectangle { get; set; }
        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }

    public class Facerectangle
    {
        [JsonProperty("left")]
        public int Left { get; set; }
        [JsonProperty("top")]
        public int Top { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class Tag
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }

    public class Face
    {
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("faceRectangle")]
        public Facerectangle FaceRectangle { get; set; }
    }

}