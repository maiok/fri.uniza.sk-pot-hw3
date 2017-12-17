using System;
using System.Runtime.Serialization;

namespace SimpleChatLibrary
{
    [DataContract]
    public class Message
    {
        public Message()
        {
            Time = DateTime.Now;
        }

        [DataMember]
        public User FromUser { get; set; }

        [DataMember]
        public User ToUser { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        public override string ToString()
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss} {1}", Time, Text);
        }
    }
}