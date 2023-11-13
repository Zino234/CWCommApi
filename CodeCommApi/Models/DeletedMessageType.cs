using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CodeCommApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeletedMessageType
    {
        [EnumMember(Value = "NotDeleted")]
        NotDeleted,

        [EnumMember(Value = "DeletedForSender")]
        DeletedForSender,

        [EnumMember(Value = "DeletedForReceiver")]
        DeletedForReceiver,

        [EnumMember(Value = "DeletedForAll")]
        DeletedForAll
    }
}
