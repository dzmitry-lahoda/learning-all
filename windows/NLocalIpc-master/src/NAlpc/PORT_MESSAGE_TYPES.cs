namespace NAlpc
{
    public enum PORT_MESSAGE_TYPES : ushort
    {

        LPC_REQUEST = 1,
        LPC_REPLY = 2,
        LPC_DATAGRAM = 3,
        LPC_LOST_REPLY = 4,
        LPC_PORT_CLOSED = 5,
        LPC_CLIENT_DIED = 6,
        LPC_EXCEPTION = 7,
        LPC_DEBUG_EVENT = 8,
        LPC_ERROR_EVENT = 9,
        LPC_CONNECTION_REQUEST = 10,

        ALPC_REQUEST = 0x2000 | LPC_REQUEST,
        ALPC_CONNECTION_REQUEST = 0x2000 | LPC_CONNECTION_REQUEST
    }
}