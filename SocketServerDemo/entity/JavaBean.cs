
using SocketServerDemo.utils;

namespace SocketServerDemo.entity
{
    abstract class JavaBean
    {
        public string toJson()
        {
            return JsonUtil.ToJSON(this);
        }

    }
}
