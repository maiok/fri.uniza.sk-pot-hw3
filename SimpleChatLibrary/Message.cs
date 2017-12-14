using System;
using System.Runtime.Serialization;

namespace SimpleChatLibrary
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public User FromUser { get; set; }
        public User ToUser { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public byte[] ImageBytes { get; set; }

        public Message()
        {
            Time = DateTime.Now;
        }


        public override string ToString()
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss} {1}", Time, Text);
        }
    }
}
