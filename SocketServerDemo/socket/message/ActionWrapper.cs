namespace SocketServerDemo.socket.message
{
    class ActionWrapper : MessageWrapper<ActionWrapper, WrapperBody>
    {

        public ActionWrapper(string message) : base(message)
        {
            
        }

    }
}
