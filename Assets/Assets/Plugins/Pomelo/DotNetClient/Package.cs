namespace Pomelo.DotNetClient
{
    using System;

    public class Package
    {
        public byte[] body;
        public int length;
        public PackageType type;

        public Package(PackageType type, byte[] body)
        {
            this.type = type;
            this.length = body.Length;
            this.body = body;
        }
    }
}

