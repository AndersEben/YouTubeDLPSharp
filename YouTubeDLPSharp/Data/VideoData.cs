using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace YouTubeDLPSharp.Data
{
    /// <summary>
    /// Represents the video metadata for one video as extracted by youtube-dlp.
    /// </summary>
    public class VideoData
    {

        [JsonProperty("_type")]
        public MetadataType ResultType { get; set; }
        // If data refers to a playlist:
        [JsonProperty("entries")]
        public VideoData[] Entries { get; set; }

        [JsonProperty("ie_key")]
        public string IE_Key { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("formats")]
        public FormatData[] Formats { get; set; }
        [JsonProperty("thumbnails")]
        public ThumbnailData[] Thumbnails { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("upload_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? UploadDate { get; set; }
        [JsonProperty("uploader")]
        public string Uploader { get; set; }
        [JsonProperty("uploader_id")]
        public string UploaderID { get; set; }
        [JsonProperty("uploader_url")]
        public string UploaderUrl { get; set; }
        [JsonProperty("channel_id")]
        public string ChannelID { get; set; }
        [JsonProperty("channel_url")]
        public string ChannelUrl { get; set; }
        [JsonProperty("duration")]
        public float? Duration { get; set; }
        [JsonProperty("view_count")]
        public long? ViewCount { get; set; }
        [JsonProperty("average_rating")]
        public double? AverageRating { get; set; }
        [JsonProperty("age_limit")]
        public int? AgeLimit { get; set; }
        [JsonProperty("webpage_url")]
        public string WebpageUrl { get; set; }
        [JsonProperty("categories")]
        public string[] Categories { get; set; }
        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("is_live")]
        public bool? IsLive { get; set; }
        [JsonProperty("was_live")]
        public bool? WasLive { get; set; }
        [JsonProperty("live_status")]
        public string LiveStatus { get; set; }
        [JsonProperty("subtitles")]
        public Dictionary<string, SubtitleData[]> Subtitles { get; set; }
        [JsonProperty("like_count")]
        public long? LikeCount { get; set; }
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("availability")]
        public string Availability { get; set; }
        [JsonProperty("original_url")]
        public string OriginalUrl { get; set; }
        [JsonProperty("webpage_url_basename")]
        public string WebpageUrlBasename { get; set; }
        [JsonProperty("extractor")]
        public string Extractor { get; set; }
        [JsonProperty("extractor_key")]
        public string ExtractorKey { get; set; }
        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
        [JsonProperty("display_id")]
        public string DisplayID { get; set; }
        [JsonProperty("fps")]
        public int? FPS { get; set; }



        [JsonProperty("fulltitle")]
        public string FullTitle { get; set; }
        [JsonProperty("ext")]
        public string Extension { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
        [JsonProperty("_filename")]
        public string Filename { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "yyyyMMdd";
        }
    }
}
