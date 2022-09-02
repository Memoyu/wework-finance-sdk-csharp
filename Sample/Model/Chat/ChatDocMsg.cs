using Newtonsoft.Json;

namespace Sample
{
    public class ChatDocMsg : ChatBase
    {
        public ChatDocMsgDetail Doc { get; set; }

        public class ChatDocMsgDetail
        {
            /// <summary>
            /// 在线文档名称
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 在线文档链接
            /// </summary>
            [JsonProperty("link_url")]
            public string LinkUrl { get; set; }

            /// <summary>
            /// 在线文档创建者。本企业成员创建为userid；外部企业成员创建为external_userid
            /// </summary>
            [JsonProperty("doc_creator")]
            public string DocCreator { get; set; }
        }
    }
}
