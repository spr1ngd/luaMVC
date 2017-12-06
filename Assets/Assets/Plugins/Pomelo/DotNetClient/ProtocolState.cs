namespace Pomelo.DotNetClient
{
    using System;

    public enum ProtocolState
    {
        closed = 4,
        handshaking = 2,
        start = 1,
        working = 3
    }
}

