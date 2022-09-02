using Newtonsoft.Json;

namespace Sample
{
    public class ChatMeeting : ChatBase
    {
        public ChatMeetingDetail Meeting { get; set; }

        public class ChatMeetingDetail
        {
            /// <summary>
            /// 会议主题。String类型
            /// </summary>
            public string Topic { get; set; }

            /// <summary>
            /// 会议开始时间。Utc时间
            /// </summary>
            [JsonProperty("starttime")]
            public long StartTime { get; set; }

            /// <summary>
            /// 会议结束时间。Utc时间
            /// </summary>
            [JsonProperty("endtime")]
            public long EndTime { get; set; }

            /// <summary>
            /// 会议地址。String类型
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// 会议备注。String类型。
            /// </summary>
            public string Remarks { get; set; }

            /// <summary>
            /// 会议消息类型。101发起会议邀请消息、102处理会议邀请消息。Uint32类型
            /// </summary>
            [JsonProperty("meetingtype")]
            public int MeetingType { get; set; }

            /// <summary>
            /// 会议id。方便将发起、处理消息进行对照。uint64类型
            /// </summary>
            [JsonProperty("meetingid")]
            public long MeetingId { get; set; }

            /// <summary>
            /// 会议邀请处理状态。1 参加会议、2 拒绝会议、3 待定、4 未被邀请、5 会议已取消、6 会议已过期、7 不在房间内。Uint32类型。只有meetingtype为102的时候此字段才有内容。
            /// </summary>
            public int Status { get; set; }
        }
    }
}
