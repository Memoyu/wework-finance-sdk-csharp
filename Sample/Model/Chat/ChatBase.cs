using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sample
{
    public class ChatBase
    {
        [JsonProperty("msgid")]
        public string MsgId { get; set; }

        public string Action { get; set; }

        public string From { get; set; }

        [JsonProperty("tolist")]
        public List<string> ToList { get; set; }

        [JsonProperty("roomid")]
        public string RoomId { get; set; }

        [JsonProperty("msgtime")]
        public long MsgTime { get; set; }

        [JsonProperty("msgtype")]
        public string MsgType { get; set; }
    }
}
