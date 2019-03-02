using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerDemo.socket.message.user
{
    class EventDefine
    {

        //用户登入
        public const string EVENT_USER_LOGON = "EVENT_USER_LOGON";

        //用户登出
        public const string EVENT_USER_LOGOUT = "EVENT_USER_LOGOUT";

        //用户登入成功
        public const string EVENT_USER_LOGON_SUCCESS = "EVENT_USER_LOGON_SUCCESS";

        //用户登出成功
        public const string EVENT_USER_LOGOUT_SUCCESS = "EVENT_USER_LOGOUT_SUCCESS";

        //用户加入
        public const string EVENT_USER_IN = "EVENT_USER_IN";

        //用户退出
        public const string EVENT_USER_OUT = "EVENT_USER_OUT";

    }
}
