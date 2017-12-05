namespace Pomelo.Protobuf
{
    using SimpleJson;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class MsgDecoder
    {
        public MsgDecoder(JsonObject protos)
        {
            if (protos == null)
            {
                protos = new JsonObject();
            }
            this.protos = protos;
            this.util = new Util();
        }

        public JsonObject decode(string route, byte[] buf)
        {
            this.buffer = buf;
            this.offset = 0;
            object obj2 = null;
            if (this.protos.TryGetValue(route, out obj2))
            {
                JsonObject msg = new JsonObject();
                return this.decodeMsg(msg, (JsonObject) obj2, this.buffer.Length);
            }
            return null;
        }

        private void decodeArray(List<object> list, string type, JsonObject proto)
        {
            if (this.util.isSimpleType(type))
            {
                int num = (int) Pomelo.Protobuf.Decoder.decodeUInt32(this.getBytes());
                for (int i = 0; i < num; i++)
                {
                    list.Add(this.decodeProp(type, null));
                }
            }
            else
            {
                list.Add(this.decodeProp(type, proto));
            }
        }

        private double decodeDouble()
        {
            double num = BitConverter.Int64BitsToDouble((long) this.ReadRawLittleEndian64());
            this.offset += 8;
            return num;
        }

        private float decodeFloat()
        {
            float num = BitConverter.ToSingle(this.buffer, this.offset);
            this.offset += 4;
            return num;
        }

        private JsonObject decodeMsg(JsonObject msg, JsonObject proto, int length)
        {
            while (this.offset < length)
            {
                int num;
                object obj3;
                object obj4;
                object obj7;
                object obj8;
                if (this.getHead().TryGetValue("tag", out num))
                {
                    object obj5;
                    string str;
                    object obj2 = null;
                    if (((proto.TryGetValue("__tags", out obj2) && ((JsonObject) obj2).TryGetValue(num.ToString(), out obj3)) && (proto.TryGetValue(obj3.ToString(), out obj4) && ((JsonObject) obj4).TryGetValue("option", out obj5))) && ((str = obj5.ToString()) != null))
                    {
                        if (!(str == "optional") && !(str == "required"))
                        {
                            if (str == "repeated")
                            {
                                goto Label_00E5;
                            }
                        }
                        else
                        {
                            object obj6;
                            if (((JsonObject) obj4).TryGetValue("type", out obj6))
                            {
                                msg.Add(obj3.ToString(), this.decodeProp(obj6.ToString(), proto));
                            }
                        }
                    }
                }
                continue;
            Label_00E5:
                if (!msg.TryGetValue(obj3.ToString(), out obj7))
                {
                    msg.Add(obj3.ToString(), new List<object>());
                }
                if (msg.TryGetValue(obj3.ToString(), out obj7) && ((JsonObject) obj4).TryGetValue("type", out obj8))
                {
                    this.decodeArray((List<object>) obj7, obj8.ToString(), proto);
                }
            }
            return msg;
        }

        private JsonObject decodeObject(string type, JsonObject proto)
        {
            object obj2;
            object obj3;
            if (((proto != null) && proto.TryGetValue("__messages", out obj2)) && ((JsonObject) obj2).TryGetValue(type, out obj3))
            {
                int num = (int) Pomelo.Protobuf.Decoder.decodeUInt32(this.getBytes());
                JsonObject msg = new JsonObject();
                return this.decodeMsg(msg, (JsonObject) obj3, this.offset + num);
            }
            return new JsonObject();
        }

        private object decodeProp(string type, JsonObject proto)
        {
            switch (type)
            {
                case "uInt32":
                    return Pomelo.Protobuf.Decoder.decodeUInt32(this.getBytes());

                case "int32":
                case "sInt32":
                    return Pomelo.Protobuf.Decoder.decodeSInt32(this.getBytes());

                case "float":
                    return this.decodeFloat();

                case "double":
                    return this.decodeDouble();

                case "string":
                    return this.decodeString();
            }
            return this.decodeObject(type, proto);
        }

        private string decodeString()
        {
            int count = (int) Pomelo.Protobuf.Decoder.decodeUInt32(this.getBytes());
            string str = Encoding.UTF8.GetString(this.buffer, this.offset, count);
            this.offset += count;
            return str;
        }

        private byte[] getBytes()
        {
            byte num2;
            List<byte> list = new List<byte>();
            int offset = this.offset;
            do
            {
                num2 = this.buffer[offset];
                list.Add(num2);
                offset++;
            }
            while (num2 >= 0x80);
            this.offset = offset;
            int count = list.Count;
            byte[] buffer = new byte[count];
            for (int i = 0; i < count; i++)
            {
                buffer[i] = list[i];
            }
            return buffer;
        }

        private Dictionary<string, int> getHead()
        {
            int num = (int) Pomelo.Protobuf.Decoder.decodeUInt32(this.getBytes());
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add("type", num & 7);
            dictionary.Add("tag", num >> 3);
            return dictionary;
        }

        private ulong ReadRawLittleEndian64()
        {
            ulong num = this.buffer[this.offset];
            ulong num2 = this.buffer[this.offset + 1];
            ulong num3 = this.buffer[this.offset + 2];
            ulong num4 = this.buffer[this.offset + 3];
            ulong num5 = this.buffer[this.offset + 4];
            ulong num6 = this.buffer[this.offset + 5];
            ulong num7 = this.buffer[this.offset + 6];
            ulong num8 = this.buffer[this.offset + 7];
            return (((((((num | (num2 << 8)) | (num3 << 0x10)) | (num4 << 0x18)) | (num5 << 0x20)) | (num6 << 40)) | (num7 << 0x30)) | (num8 << 0x38));
        }

        private byte[] buffer { get; set; }

        private int offset { get; set; }

        private JsonObject protos { get; set; }

        private Util util { get; set; }
    }
}

