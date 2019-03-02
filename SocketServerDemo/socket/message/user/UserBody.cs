using System.Collections.Generic;

namespace SocketServerDemo.socket.message.user
{
    class UserBody : WrapperBody
    {

        public string Event { get; set; }

        public List<User> CurrentUsers { get; set; }

        public User ChangedUser { get; set; }

        public bool isUserLogon()
        {
            return EventDefine.EVENT_USER_LOGON.Equals(Event);
        }

        public bool isUserLogout()
        {
            return EventDefine.EVENT_USER_LOGOUT.Equals(Event);
        }

    }
}
