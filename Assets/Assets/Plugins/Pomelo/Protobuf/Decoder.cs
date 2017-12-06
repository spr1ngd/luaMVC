namespace Pomelo.Protobuf
{
    using System;
    using System.Runtime.InteropServices;

    public class Decoder
    {
        public static int decodeSInt32(byte[] bytes)
        {
            uint num = decodeUInt32(bytes);
            int num2 = ((num % 2) == 1) ? -1 : 1;
            return Convert.ToInt32((long) ((((num % 2) + num) / 2) * num2));
        }

        public static uint decodeUInt32(byte[] bytes)
        {
            int num;
            return decodeUInt32(0, bytes, out num);
        }

        public static uint decodeUInt32(int offset, byte[] bytes, out int length)
        {
            uint num = 0;
            length = 0;
            for (int i = offset; i < bytes.Length; i++)
            {
                length++;
                uint num3 = Convert.ToUInt32(bytes[i]);
                num += Convert.ToUInt32((double) ((num3 & 0x7f) * Math.Pow(2.0, (double) (7 * (i - offset)))));
                if (num3 < 0x80)
                {
                    return num;
                }
            }
            return num;
        }
    }
}

