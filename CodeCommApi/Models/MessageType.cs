using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CodeCommApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MessageType
    {
        [EnumMember(Value = "Text")]
        Text,

        [EnumMember(Value = "Audio")]
        Audio,

        [EnumMember(Value = "Video")]
        Video,

        [EnumMember(Value = "Gif")]
        Gif,

        [EnumMember(Value = "File")]
        File,

        [EnumMember(Value = "VoiceNote")]
        VoiceNote
    }
}
