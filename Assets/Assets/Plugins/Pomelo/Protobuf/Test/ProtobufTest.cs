namespace Pomelo.Protobuf.Test
{
    using Pomelo.Protobuf;
    using SimpleJson;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ProtobufTest
    {
        public static bool equal(JsonObject a, JsonObject b)
        {
            ICollection<string> keys = a.Keys;
            ICollection<string> collection1 = b.Keys;
            foreach (string str in keys)
            {
                Console.WriteLine(a[str].GetType());
                if (a[str].GetType().ToString() == "SimpleJson.JsonObject")
                {
                    equal((JsonObject) a[str], (JsonObject) b[str]);
                }
            }
            return false;
        }

        private static void print(byte[] bytes, int offset, int length)
        {
            for (int i = offset; i < length; i++)
            {
                Console.Write(Convert.ToString(bytes[i], 0x10) + " ");
            }
            Console.WriteLine();
        }

        public static JsonObject read(string name)
        {
            StreamReader reader = new StreamReader(name);
            return (JsonObject) SimpleJson.DeserializeObject(reader.ReadToEnd());
        }

        public static void Run()
        {
            JsonObject encodeProtos = read("./protos.json");
            JsonObject obj3 = read("./msg.json");
            Pomelo.Protobuf.Protobuf protobuf = new Pomelo.Protobuf.Protobuf(encodeProtos, encodeProtos);
            foreach (string str in obj3.Keys)
            {
                JsonObject msg = (JsonObject) obj3[str];
                byte[] buffer = protobuf.encode(str, msg);
                protobuf.decode(str, buffer);
            }
            Console.WriteLine("finish protobuf test");
        }
    }
}

