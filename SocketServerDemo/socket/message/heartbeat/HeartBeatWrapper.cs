namespace SocketServerDemo.socket.message.heartbeat
{
    class HeartBeatWrapper : AbstractMessageWrapper<HeartBeatWrapper, HeartBeatBody>
    {
        public HeartBeatWrapper() : base()
        {
        }

        public HeartBeatWrapper(string message) : base(message)
        {
        }

    }
}
