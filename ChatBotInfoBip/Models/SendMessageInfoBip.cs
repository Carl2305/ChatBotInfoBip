using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBotInfoBip.Models
{
    public class SendMessageInfoBip
    {
        public string from { get; set; } = "447860099299";
        public string to { get; set; }
        public string channel { get; set; } = "WHATSAPP";
        public string contentType { get; set; } = "TEXT";
        public ContentMessage content { get; set; }

    }
    public class ContentMessage
    {
        public string text { get; set; }
    }


    public class SendMessageInfoBip2
    {
        public string from { get; set; } = "447860099299";
        public string to { get; set; }
        public ContentMessage content { get; set; }

    }
}
