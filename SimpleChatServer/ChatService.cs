using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;
using SimpleChatLibrary;

namespace SimpleChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        private readonly List<Message> _messages = new List<Message>();

        // Thread-Safe dictionary, musi byt nastavene ConcurrencyMode.Multiple
        private readonly ConcurrentDictionary<string, User> _registeredUsers = new ConcurrentDictionary<string, User>();

        private User _loggedUser;

        public List<Message> GetMessages()
        {
            return _messages;
        }

        public List<User> GetUsers()
        {
            var listUsers = new List<User>();
            foreach (var user in _registeredUsers)
                listUsers.Add(user.Value);
            return listUsers;
        }

        public List<string> GetUserNickNames()
        {
            var listUsers = new List<string>();
            foreach (var user in _registeredUsers)
                listUsers.Add(user.Key);
            return listUsers;
        }

        public void SendMessage(string toUser, string text)
        {
            var message = new Message();
            // Kontrola prihlaseneho uzivatela sa vykona na strane klienta
            message.FromUser = _loggedUser;
            message.ToUser = toUser.Trim() != "" ? GetUserByNickName(toUser) : null;
            message.Text = text;

            // Pridanie spravy do chatu
            _messages.Add(message);
        }

        public void SendImage(string toUser, string imagePath)
        {
            var message = new Message();
            message.FromUser = _loggedUser;
            message.ToUser = toUser.Trim() != "" ? GetUserByNickName(toUser) : null;
            message.ImagePath = imagePath;

            // Pridanie obrazku do chatu
            _messages.Add(message);
        }

        public bool RegisterUser(string nickname)
        {
            var newUser = new User();
            newUser.NickName = nickname;

            return _registeredUsers.TryAdd(nickname, newUser);
        }

        public bool LoginAs(string nickname)
        {
            _loggedUser = GetUserByNickName(nickname);
            return _loggedUser != null;
        }

        public bool IsUserLogged()
        {
            return _loggedUser != null;
        }

        public User GetUserByNickName(string nickname)
        {
            foreach (var user in _registeredUsers)
                if (user.Key.ToLower() == nickname.ToLower())
                    return user.Value;

            return null;
        }

        public Message GetLastInsertMessage()
        {
            return _messages[0];
            //return null;
        }
    }
}