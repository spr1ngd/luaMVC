namespace Pomelo.DotNetClient
{
    using System;

    internal class StateObject
    {
        internal byte[] buffer = new byte[0x400];
        public const int BufferSize = 0x400;
    }
}

