using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDLPSharp.Options
{
    /// <summary>
    /// Possible video merging formats.
    /// </summary>
    public enum DownloadMergeFormat
    {
        Unspecified, Mp4, Mkv, Ogg, Webm, Flv
    }

    /// <summary>
    /// Possible audio formats for audio conversion.
    /// </summary>
    public enum AudioConversionFormat
    {
        Best, Aac, Flac, Mp3, M4a, Opus, Vorbis, Wav, Alac
    }

    /// <summary>
    /// Possible video formats for video conversion.
    /// </summary>
    public enum VideoRecodeFormat
    {
        None, Mp4, Mkv, Ogg, Webm, Flv, Avi
    }
}
