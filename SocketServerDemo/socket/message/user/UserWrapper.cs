namespace SocketServerDemo.socket.message.user
{
    class UserWrapper : AbstractMessageWrapper<UserWrapper, UserBody>
    {

        public UserWrapper() : base()
        {

        }

        public UserWrapper(string message) : base(message)
        {

        }

    }
}
