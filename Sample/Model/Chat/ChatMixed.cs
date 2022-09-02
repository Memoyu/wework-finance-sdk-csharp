using System.Collections.Generic;

namespace Sample
{
    public class ChatMixed : ChatBase
    {
        public ChatMixedDetail Mixed { get; set; }

        public class ChatMixedDetail
        {
            public List<ChatMixedDetailItem> Item { get; set; }
        }

        public class ChatMixedDetailItem
        {
            public string Type { get; set; }

            public string Content { get; set; }
        }
    }
}
