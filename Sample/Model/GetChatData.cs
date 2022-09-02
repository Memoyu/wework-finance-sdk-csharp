using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sample
{
    public class GetChatData
    {
        /// <summary>
        /// 聊天记录数据内容。数组类型。包括seq、msgid等内容
        /// </summary>
        [JsonProperty("chatdata")]
        public List<ChatData> ChatDatas { get; set; }

        public string Errmsg { get; set; }

        public int Errcode { get; set; }

        public class ChatData
        {
            /// <summary>
            /// 消息的seq值，标识消息的序号。再次拉取需要带上上次回包中最大的seq。Uint64类型，范围0-pow(2,64)-1
            /// </summary>
            public long Seq;

            /// <summary>
            /// 消息id，消息的唯一标识，企业可以使用此字段进行消息去重。String类型。msgid以_external结尾的消息，表明该消息是一条外部消息。
            /// </summary>
            [JsonProperty("msgid")]
            public string MsgId;

            /// <summary>
            /// 加密此条消息使用的公钥版本号。Uint32类型
            /// </summary>
            [JsonProperty("publickey_ver")]
            public int PublicKeyVer;

            /// <summary>
            /// 使用publickey_ver指定版本的公钥进行非对称加密后base64加密的内容，需要业务方先base64 decode处理后，再使用指定版本的私钥进行解密，得出内容。String类型
            /// </summary>
            [JsonProperty("encrypt_random_key")]
            public string EncryptRandomKey;

            /// <summary>
            /// 消息密文。需要业务方使用将encrypt_random_key解密得到的内容，与encrypt_chat_msg，传入sdk接口DecryptData,得到消息明文。String类型
            /// </summary>
            [JsonProperty("encrypt_chat_msg")]
            public string EncryptChatMsg;
        }
    }
}
