namespace Pomelo.Protobuf
{
    using System;
    using System.Collections.Generic;

    public class Util
    {
        private Dictionary<string, int> typeMap;
        private string[] types;

        public Util()
        {
            this.initTypeMap();
            this.types = new string[] { "uInt32", "sInt32", "int32", "uInt64", "sInt64", "float", "double" };
        }

        public int containType(string type)
        {
            int num;
            int num2 = 2;
            if (this.typeMap.TryGetValue(type, out num))
            {
                num2 = num;
            }
            return num2;
        }

        private void initTypeMap()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add("uInt32", 0);
            dictionary.Add("sInt32", 0);
            dictionary.Add("int32", 0);
            dictionary.Add("double", 1);
            dictionary.Add("string", 2);
            dictionary.Add("float", 5);
            dictionary.Add("message", 2);
            this.typeMap = dictionary;
        }

        public bool isSimpleType(string type)
        {
            int length = this.types.Length;
            for (int i = 0; i < length; i++)
            {
                if (type == this.types[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static void Reverse(byte[] bytes)
        {
            int index = 0;
            for (int i = bytes.Length - 1; index < i; i--)
            {
                byte num = bytes[index];
                bytes[index] = bytes[i];
                bytes[i] = num;
                index++;
            }
        }
    }
}

