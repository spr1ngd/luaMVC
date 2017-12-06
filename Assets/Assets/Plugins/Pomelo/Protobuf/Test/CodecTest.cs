namespace Pomelo.Protobuf.Test
{
    using Pomelo.Protobuf;
    using System;

    public class CodecTest
    {
        public static bool EncodeSInt32Test(int count)
        {
            Random random = new Random();
            int num = -1;
            for (int i = 0; i < count; i++)
            {
                num *= -1;
                int n = random.Next(0, 0x7fffffff) * num;
                int num4 = Decoder.decodeSInt32(Encoder.encodeSInt32(n));
                if (n != num4)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool EncodeUInt32Test(int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                uint n = (uint) random.Next(0, 0x7fffffff);
                uint num3 = Decoder.decodeUInt32(Encoder.encodeUInt32(n));
                if (n != num3)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Run()
        {
            bool flag = true;
            bool flag2 = false;
            DateTime now = DateTime.Now;
            flag2 = EncodeSInt32Test(0x2710);
            DateTime time2 = DateTime.Now;
            Console.WriteLine("Encode sint32 test finished , result is : {1}, cost time : {0}", (TimeSpan) (time2 - now), flag2);
            if (!flag2)
            {
                flag = false;
            }
            now = DateTime.Now;
            flag2 = EncodeUInt32Test(0x2710);
            time2 = DateTime.Now;
            Console.WriteLine("Encode uint32 test finished , result is : {1}, cost time : {0}", (TimeSpan) (time2 - now), flag2);
            if (!flag2)
            {
                flag = false;
            }
            return flag;
        }
    }
}

