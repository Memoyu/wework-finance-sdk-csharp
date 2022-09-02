using Newtonsoft.Json;

namespace Sample
{
    public class ChatCard : ChatBase
    {
        public ChatCardDetail Card { get; set; }

        public class ChatCardDetail
        {
            /// <summary>
            /// 名片所有者的id，同一公司是userid，不同公司是external_userid。String类型
            /// </summary>
            [JsonProperty("userid")]
            public string UserId { get; set; }

            /// <summary>
            /// 名片所有者所在的公司名称。String类型
            /// </summary>
            [JsonProperty("corpname")]
            public string CorpName { get; set; }
        }
    }
}
