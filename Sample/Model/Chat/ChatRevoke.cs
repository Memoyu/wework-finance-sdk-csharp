using Newtonsoft.Json;

namespace Sample
{
    public class ChatRevoke : ChatBase
    {
        public ChatRevokeDetail Revoke { get; set; }

        public class ChatRevokeDetail
        {
            /// <summary>
            /// 标识撤回的原消息的msgid。String类型
            /// </summary>
            [JsonProperty("pre_msgid")]
            public string PreMsgId { get; set; }
        }
    }
}
