using SocketServerDemo.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerDemo.socket.message
{
    abstract class AbstractMessageWrapper<M, T> where T : WrapperBody where M : AbstractMessageWrapper<M, T>
    {
        private MessageBase message = new MessageBase();

        public AbstractMessageWrapper()
        {
            
        }

        public AbstractMessageWrapper(T body)
        {
            message.Body = body.toJson();
        }

        public AbstractMessageWrapper(String message)
        {
            this.message = MessageBase.fromJson<MessageBase>(message);
        }

        public string GetStringMessage()
        {
            return message.toJson();
        }

        public T GetWrapperBody()
        {
            return JavaBean.fromJson<T>(message.Body);
        }

        public M Action(String action)
        {
            message.Action = action;
            return (M)this;
        }

        public string GetAction()
        {
            return message.Action;
        }

        public void SetBody(T body)
        {
            message.Body = body.toJson();
        }

    }
}
