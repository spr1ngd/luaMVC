namespace Pomelo.Protobuf
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Encoder
    {
        public static int byteLength(string msg)
        {
            return Encoding.UTF8.GetBytes(msg).Length;
        }

        public static byte[] encodeFloat(float n)
        {
            byte[] bytes = BitConverter.GetBytes(n);
            if (!BitConverter.IsLittleEndian)
            {
                Util.Reverse(bytes);
            }
            return bytes;
        }

        public static byte[] encodeSInt32(int n)
        {
            uint num = (n < 0) ? ((uint) ((Math.Abs(n) * 2) - 1)) : ((uint) (n * 2));
            return encodeUInt32(num);
        }

        public static byte[] encodeSInt32(string n)
        {
            return encodeSInt32(Convert.ToInt32(n));
        }

        public static byte[] encodeUInt32(string n)
        {
            return encodeUInt32(Convert.ToUInt32(n));
        }

        public static byte[] encodeUInt32(uint n)
        {
            List<byte> list = new List<byte>();
            do
            {
                uint num = n % 0x80;
                uint num2 = n >> 7;
                if (num2 != 0)
                {
                    num += 0x80;
                }
                list.Add(Convert.ToByte(num));
                n = num2;
            }
            while (n != 0);
            return list.ToArray();
        }
    }
}

