namespace Pomelo.DotNetClient
{
    using System;

    public enum TransportState
    {
        closed = 3,
        readBody = 2,
        readHead = 1
    }
}

