using Newtonsoft.Json;

namespace Sample
{
    public class ChatAgree : ChatBase
    {
        public ChatAgreeDetail Agree { get; set; }

        public class ChatAgreeDetail
        {
            /// <summary>
            /// 同意/不同意协议者的userid，外部企业默认为external_userid。String类型
            /// </summary>
            [JsonProperty("userid")]
            public string UserId { get; set; }

            /// <summary>
            /// 同意/不同意协议的时间，utc时间，ms单位
            /// </summary>
            [JsonProperty("agree_time")]
            public long AgreeTime { get; set; }
        }
    }
}
