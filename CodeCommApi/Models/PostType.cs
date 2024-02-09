using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace CodeCommApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PostType
    {
        [EnumMember(Value = "Text")]
        Text,
        [EnumMember(Value = "Video")]
        Video,
        [EnumMember(Value = "Image")]
        Image,
        [EnumMember(Value = "Job")]
        Job,
    }
}