namespace Pomelo.DotNetClient
{
    using Pomelo.Protobuf;
    using SimpleJson;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MessageProtocol
    {
        private Dictionary<ushort, string> abbrs = new Dictionary<ushort, string>();
        private JsonObject decodeProtos = new JsonObject();
        private Dictionary<string, ushort> dict = new Dictionary<string, ushort>();
        private JsonObject encodeProtos = new JsonObject();
        public const int MSG_Route_Limit = 0xff;
        public const int MSG_Route_Mask = 1;
        public const int MSG_Type_Mask = 7;
        private Pomelo.Protobuf.Protobuf protobuf;
        private Dictionary<uint, string> reqMap;

        public MessageProtocol(JsonObject dict, JsonObject serverProtos, JsonObject clientProtos)
        {
            foreach (string str in dict.Keys)
            {
                ushort num = Convert.ToUInt16(dict[str]);
                this.dict[str] = num;
                this.abbrs[num] = str;
            }
            this.protobuf = new Pomelo.Protobuf.Protobuf(clientProtos, serverProtos);
            this.encodeProtos = clientProtos;
            this.decodeProtos = serverProtos;
            this.reqMap = new Dictionary<uint, string>();
        }

        private int byteLength(string msg)
        {
            return Encoding.UTF8.GetBytes(msg).Length;
        }

        public Message decode(byte[] buffer)
        {
            string str;
            JsonObject obj2;
            byte num = buffer[0];
            int offset = 1;
            MessageType type = ((MessageType) (num >> 1)) & ((MessageType) 7);
            uint key = 0;
            if (type == MessageType.MSG_RESPONSE)
            {
                int num4;
                key = Pomelo.Protobuf.Decoder.decodeUInt32(offset, buffer, out num4);
                if ((key <= 0) || !this.reqMap.ContainsKey(key))
                {
                    return null;
                }
                str = this.reqMap[key];
                this.reqMap.Remove(key);
                offset += num4;
            }
            else
            {
                if (type != MessageType.MSG_PUSH)
                {
                    return null;
                }
                if ((num & 1) == 1)
                {
                    ushort num5 = this.readShort(offset, buffer);
                    str = this.abbrs[num5];
                    offset += 2;
                }
                else
                {
                    byte count = buffer[offset];
                    offset++;
                    str = Encoding.UTF8.GetString(buffer, offset, count);
                    offset += count;
                }
            }
            byte[] bytes = new byte[buffer.Length - offset];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = buffer[i + offset];
            }
            if (this.decodeProtos.ContainsKey(str))
            {
                obj2 = this.protobuf.decode(str, buffer);
            }
            else
            {
                obj2 = (JsonObject) SimpleJson.DeserializeObject(Encoding.UTF8.GetString(bytes));
            }
            return new Message(type, key, str, obj2);
        }

        public byte[] encode(string route, JsonObject msg)
        {
            return this.encode(route, 0, msg);
        }

        public byte[] encode(string route, uint id, JsonObject msg)
        {
            byte[] bytes;
            int num = this.byteLength(route);
            if (num > 0xff)
            {
                throw new Exception("Route is too long!");
            }
            byte[] target = new byte[num + 6];
            int offset = 1;
            byte num3 = 0;
            if (id > 0)
            {
                byte[] source = Pomelo.Protobuf.Encoder.encodeUInt32(id);
                this.writeBytes(source, offset, target);
                offset += source.Length;
            }
            else
            {
                num3 = (byte) (num3 | 2);
            }
            if (this.dict.ContainsKey(route))
            {
                ushort num4 = this.dict[route];
                this.writeShort(offset, num4, target);
                num3 = (byte) (num3 | 1);
                offset += 2;
            }
            else
            {
                target[offset++] = (byte) num;
                this.writeBytes(Encoding.UTF8.GetBytes(route), offset, target);
                offset += num;
            }
            target[0] = num3;
            if (this.encodeProtos.ContainsKey(route))
            {
                bytes = this.protobuf.encode(route, msg);
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes(msg.ToString());
            }
            byte[] buffer4 = new byte[offset + bytes.Length];
            for (int i = 0; i < offset; i++)
            {
                buffer4[i] = target[i];
            }
            for (int j = 0; j < bytes.Length; j++)
            {
                buffer4[offset + j] = bytes[j];
            }
            if (id > 0)
            {
                this.reqMap.Add(id, route);
            }
            return buffer4;
        }

        private ushort readShort(int offset, byte[] bytes)
        {
            ushort num = 0;
            num = (ushort) (num + ((ushort) (bytes[offset] << 8)));
            return (ushort) (num + bytes[offset + 1]);
        }

        private void writeBytes(byte[] source, int offset, byte[] target)
        {
            for (int i = 0; i < source.Length; i++)
            {
                target[offset + i] = source[i];
            }
        }

        private void writeInt(int offset, uint value, byte[] bytes)
        {
            bytes[offset] = (byte) ((value >> 0x18) & 0xff);
            bytes[offset + 1] = (byte) ((value >> 0x10) & 0xff);
            bytes[offset + 2] = (byte) ((value >> 8) & 0xff);
            bytes[offset + 3] = (byte) (value & 0xff);
        }

        private void writeShort(int offset, ushort value, byte[] bytes)
        {
            bytes[offset] = (byte) ((value >> 8) & 0xff);
            bytes[offset + 1] = (byte) (value & 0xff);
        }
    }
}

