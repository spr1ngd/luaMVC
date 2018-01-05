namespace Pomelo.DotNetClient
{
    using System;

    public class PackageProtocol
    {
        public const int HEADER_LENGTH = 4;

        public static Package decode(byte[] buf)
        {
            PackageType type = (PackageType) buf[0];
            byte[] body = new byte[buf.Length - 4];
            for (int i = 0; i < body.Length; i++)
            {
                body[i] = buf[i + 4];
            }
            return new Package(type, body);
        }

        public static byte[] encode(PackageType type)
        {
            byte[] buffer = new byte[4];
            buffer[0] = Convert.ToByte(type);
            return buffer;
        }

        public static byte[] encode(PackageType type, byte[] body)
        {
            int num = 4;
            if (body != null)
            {
                num += body.Length;
            }
            byte[] buffer = new byte[num];
            int index = 0;
            buffer[index++] = Convert.ToByte(type);
            buffer[index++] = Convert.ToByte((int) ((body.Length >> 0x10) & 0xff));
            buffer[index++] = Convert.ToByte((int) ((body.Length >> 8) & 0xff));
            buffer[index++] = Convert.ToByte((int) (body.Length & 0xff));
            while (index < num)
            {
                buffer[index] = body[index - 4];
                index++;
            }
            return buffer;
        }
    }
}

