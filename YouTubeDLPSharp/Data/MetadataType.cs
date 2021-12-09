using System.Runtime.Serialization;


namespace YouTubeDLPSharp.Data
{
    /// <summary>
    /// Possible types of media fetched by youtube-dlp.
    /// </summary>
    public enum MetadataType
    {
        [EnumMember(Value = "video")]
        Video,
        [EnumMember(Value = "playlist")]
        Playlist,
        [EnumMember(Value = "multi_video")]
        MultiVideo,
        [EnumMember(Value = "url")]
        Url,
        [EnumMember(Value = "url_transparent")]
        UrlTransparent
    }
}
