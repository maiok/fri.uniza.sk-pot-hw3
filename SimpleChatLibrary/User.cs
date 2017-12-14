using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatLibrary
{
    public class User
    {
        public IClient Connection;

        public string NickName { get; set; }
    }
}
