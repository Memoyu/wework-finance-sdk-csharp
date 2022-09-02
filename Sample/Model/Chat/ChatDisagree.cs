using Newtonsoft.Json;

namespace Sample
{
    public class ChatDisagree : ChatBase
    {
        public ChatDisagreeDetail Disagree { get; set; }

        public class ChatDisagreeDetail
        {
            /// <summary>
            /// 同意/不同意协议者的userid，外部企业默认为external_userid。String类型
            /// </summary>
            [JsonProperty("userid")]
            public string UserId { get; set; }

            /// <summary>
            /// 同意/不同意协议的时间，utc时间，ms单位
            /// </summary>
            [JsonProperty("disagree_time")]
            public long DisagreeTime { get; set; }
        }
    }
}
