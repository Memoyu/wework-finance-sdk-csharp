using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sample
{
    public class ChatCalendar : ChatBase
    {
        public ChatCalendarDetail Calendar { get; set; }

        public class ChatCalendarDetail
        {
            /// <summary>
            /// 消息标题。String类型
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 日程组织者。String类型
            /// </summary>
            [JsonProperty("creatorname")]
            public string CreatorName { get; set; }

            /// <summary>
            /// 日程参与人。数组，内容为String类型
            /// </summary>
            [JsonProperty("attendeename")]
            public List<string> AttendeeName { get; set; }

            /// <summary>
            /// 链接图片url。String类型
            /// </summary>
            [JsonProperty("starttime")]
            public long StartTime { get; set; }

            /// <summary>
            /// 日程结束时间。Utc时间，单位秒
            /// </summary>
            [JsonProperty("endtime")]
            public long EndTime { get; set; }

            /// <summary>
            /// 日程地点。String类型
            /// </summary>
            public string Place { get; set; }

            /// <summary>
            /// 日程备注。String类型
            /// </summary>
            public string Remarks { get; set; }
        }
    }
}
