using Newtonsoft.Json;

namespace Sample
{
    public class ChatExternalRedPacket : ChatBase
    {
        [JsonProperty("redpacket")]
        public ChatExternalRedPacketDetail RedPacket { get; set; }

        public class ChatExternalRedPacketDetail
        {
            /// <summary>
            /// 红包消息类型。1 普通红包、2 拼手气群红包、3 激励群红包。Uint32类型
            /// </summary>
            public int Type { get; set; }

            /// <summary>
            /// 红包祝福语。String类型
            /// </summary>
            public string Wish { get; set; }

            /// <summary>
            /// 红包总个数。Uint32类型
            /// </summary>
            [JsonProperty("totalcnt")]
            public int TotalCnt { get; set; }

            /// <summary>
            /// 红包总金额。Uint32类型，单位为分。
            /// </summary>
            [JsonProperty("totalamount")]
            public int TotalAmount { get; set; }
        }
    }
}
