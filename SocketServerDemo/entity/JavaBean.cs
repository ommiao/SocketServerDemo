
using SocketServerDemo.utils;

namespace SocketServerDemo.entity
{
    public abstract class JavaBean
    {
        public string toJson()
        {
            return JsonUtil.ToJSON(this);
        }

        public static T fromJson<T>(string json) where T : JavaBean
        {
            T t;
            t = JsonUtil.FromJSON<T>(json);
            return t;
        }

    }
}
