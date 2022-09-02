using System;
using System.ComponentModel;
using System.Linq;

namespace Sample
{
    public class ChatMsgType
    {
        [Description("[文本消息]")]
        public const string Text = "text";

        [Description("[图片]")]
        public const string Image = "image";

        [Description("[撤回消息]")]
        public const string Revoke = "revoke";

        [Description("[其他消息]")]
        public const string Agree = "agree";

        [Description("[其他消息]")]
        public const string Disagree = "disagree";

        [Description("[语音]")]
        public const string Voice = "voice";

        [Description("[视频]")]
        public const string Video = "video";

        [Description("[名片]")]
        public const string Card = "card";

        [Description("[位置]")]
        public const string Location = "location";

        [Description("[自定义表情]")]
        public const string Emotion = "emotion";

        [Description("[文件]")]
        public const string File = "file";

        [Description("[链接]")]
        public const string Link = "link";

        [Description("[小程序消息]")]
        public const string WeApp = "weapp";

        [Description("[聊天记录]")]
        public const string ChatRecord = "chatrecord";

        [Description("[待办消息]")]
        public const string Todo = "todo";

        [Description("[投票消息]")]
        public const string Vote = "vote";

        [Description("[填表消息]")]
        public const string Collect = "collect";

        [Description("[红包消息]")]
        public const string RedPacket = "redpacket";

        [Description("[会议邀请]")]
        public const string Meeting = "meeting";

        [Description("[切换企业]")]
        public const string Switch = "switch";

        [Description("[在线文档]")]
        public const string DocMsg = "docmsg";

        [Description("[MarkDown]")]
        public const string Markdown = "markdown";

        [Description("[图文消息]")]
        public const string News = "news";

        [Description("[日程消息]")]
        public const string Calendar = "calendar";

        [Description("[混合消息]")]
        public const string Mixed = "mixed";

        [Description("[音频存档消息]")]
        public const string MeetingVoiceCall = "meeting_voice_call";

        [Description("[音频共享消息]")]
        public const string VoipDocShare = "voip_doc_share";

        [Description("[互通红包消息]")]
        public const string ExternalRedPacket = "external_redpacket";

        [Description("[视频号]")]
        public const string SphFeed = "sphfeed";

        public static string GetDescription(string str)
        {
            var type = new ChatMsgType();
            var prop = type.GetType().GetFields().FirstOrDefault(p => p.GetValue(type).ToString() == str);
            if (prop == null)
                return "[未知消息]";

            var attrs = prop.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (!attrs.Any())
                return "[未知消息]";
            var desc = (DescriptionAttribute)attrs[0];
            return desc.Description;
        }

        public static Type GetMsgType(string type) => type switch
        {
            Text => typeof(ChatText),
            Image => typeof(ChatImage),
            Revoke => typeof(ChatRevoke),
            Agree => typeof(ChatAgree),
            Disagree => typeof(ChatDisagree),
            Voice => typeof(ChatVoice),
            Video => typeof(ChatVideo),
            Card => typeof(ChatCard),
            Location => typeof(ChatLocation),
            Emotion => typeof(ChatEmotion),
            File => typeof(ChatFile),
            Link => typeof(ChatLink),
            WeApp => typeof(ChatWeApp),
            ChatRecord => typeof(ChatChatRecord),
            Todo => typeof(ChatTodo),
            Vote => typeof(ChatVote),
            Collect => typeof(ChatCollect),
            RedPacket => typeof(ChatRedPacket),
            Meeting => typeof(ChatMeeting),
            Switch => typeof(ChatSwitch),
            DocMsg => typeof(ChatDocMsg),
            Markdown => typeof(ChatMarkDown),
            News => typeof(ChatNews),
            Calendar => typeof(ChatCalendar),
            Mixed => typeof(ChatMixed),
            ExternalRedPacket => typeof(ChatExternalRedPacket),
            SphFeed => typeof(ChatSphFeed),
            _ => typeof(ChatBase)
        };
    }
}
