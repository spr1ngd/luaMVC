namespace Pomelo.Protobuf
{
    using SimpleJson;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class MsgEncoder
    {
        public MsgEncoder(JsonObject protos)
        {
            if (protos == null)
            {
                protos = new JsonObject();
            }
            this.protos = protos;
            this.util = new Util();
        }

        private bool checkMsg(JsonObject msg, JsonObject proto)
        {
            foreach (string str in proto.Keys)
            {
                object obj3;
                JsonObject obj5;
                object obj6;
                object obj7;
                string str2;
                JsonObject obj2 = (JsonObject) proto[str];
                if (!obj2.TryGetValue("option", out obj3) || ((str2 = obj3.ToString()) == null))
                {
                    continue;
                }
                if (!(str2 == "required"))
                {
                    if (str2 == "optional")
                    {
                        goto Label_008F;
                    }
                    if (str2 == "repeated")
                    {
                        goto Label_00F8;
                    }
                    continue;
                }
                if (msg.ContainsKey(str))
                {
                    continue;
                }
                return false;
            Label_008F:
                obj5 = (JsonObject) proto["__messages"];
                object obj4 = obj2["type"];
                if (msg.ContainsKey(str) && obj5.ContainsKey(obj4.ToString()))
                {
                    this.checkMsg((JsonObject) msg[str], (JsonObject) obj5[obj4.ToString()]);
                }
                continue;
            Label_00F8:
                if ((obj2.TryGetValue("type", out obj4) && msg.TryGetValue(str, out obj6)) && ((JsonObject) proto["__messages"]).TryGetValue(obj4.ToString(), out obj7))
                {
                    List<object> list = (List<object>) obj6;
                    foreach (object obj8 in list)
                    {
                        if (!this.checkMsg((JsonObject) obj8, (JsonObject) obj7))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public byte[] encode(string route, JsonObject msg)
        {
            byte[] buffer = null;
            object obj2;
            if (this.protos.TryGetValue(route, out obj2))
            {
                if (!this.checkMsg(msg, (JsonObject) obj2))
                {
                    return null;
                }
                int num = Pomelo.Protobuf.Encoder.byteLength(msg.ToString()) * 2;
                int offset = 0;
                byte[] buffer2 = new byte[num];
                offset = this.encodeMsg(buffer2, offset, (JsonObject) obj2, msg);
                buffer = new byte[offset];
                for (int i = 0; i < offset; i++)
                {
                    buffer[i] = buffer2[i];
                }
            }
            return buffer;
        }

        private int encodeArray(List<object> msg, JsonObject value, int offset, byte[] buffer, JsonObject proto)
        {
            object obj2;
            object obj3;
            if (value.TryGetValue("type", out obj2) && value.TryGetValue("tag", out obj3))
            {
                if (this.util.isSimpleType(obj2.ToString()))
                {
                    offset = this.writeBytes(buffer, offset, this.encodeTag(obj2.ToString(), Convert.ToInt32(obj3)));
                    offset = this.writeBytes(buffer, offset, Pomelo.Protobuf.Encoder.encodeUInt32((uint) msg.Count));
                    foreach (object obj4 in msg)
                    {
                        offset = this.encodeProp(obj4, obj2.ToString(), offset, buffer, null);
                    }
                    return offset;
                }
                foreach (object obj5 in msg)
                {
                    offset = this.writeBytes(buffer, offset, this.encodeTag(obj2.ToString(), Convert.ToInt32(obj3)));
                    offset = this.encodeProp(obj5, obj2.ToString(), offset, buffer, proto);
                }
            }
            return offset;
        }

        private int encodeMsg(byte[] buffer, int offset, JsonObject proto, JsonObject msg)
        {
            foreach (string str in msg.Keys)
            {
                object obj2;
                object obj3;
                object obj6;
                string str2;
                if ((proto.TryGetValue(str, out obj2) && ((JsonObject) obj2).TryGetValue("option", out obj3)) && ((str2 = obj3.ToString()) != null))
                {
                    if (!(str2 == "required") && !(str2 == "optional"))
                    {
                        if (str2 == "repeated")
                        {
                            goto Label_00E4;
                        }
                    }
                    else
                    {
                        object obj4;
                        object obj5;
                        if (((JsonObject) obj2).TryGetValue("type", out obj4) && ((JsonObject) obj2).TryGetValue("tag", out obj5))
                        {
                            offset = this.writeBytes(buffer, offset, this.encodeTag(obj4.ToString(), Convert.ToInt32(obj5)));
                            offset = this.encodeProp(msg[str], obj4.ToString(), offset, buffer, proto);
                        }
                    }
                }
                continue;
            Label_00E4:
                if (msg.TryGetValue(str, out obj6) && (((List<object>) obj6).Count > 0))
                {
                    offset = this.encodeArray((List<object>) obj6, (JsonObject) obj2, offset, buffer, proto);
                }
            }
            return offset;
        }

        private int encodeProp(object value, string type, int offset, byte[] buffer, JsonObject proto)
        {
            object obj2;
            object obj3;
            switch (type)
            {
                case "uInt32":
                    this.writeUInt32(buffer, ref offset, value);
                    return offset;

                case "int32":
                case "sInt32":
                    this.writeInt32(buffer, ref offset, value);
                    return offset;

                case "float":
                    this.writeFloat(buffer, ref offset, value);
                    return offset;

                case "double":
                    this.writeDouble(buffer, ref offset, value);
                    return offset;

                case "string":
                    this.writeString(buffer, ref offset, value);
                    return offset;
            }
            if (proto.TryGetValue("__messages", out obj2) && ((JsonObject) obj2).TryGetValue(type, out obj3))
            {
                byte[] buffer2 = new byte[Pomelo.Protobuf.Encoder.byteLength(value.ToString())];
                int num = 0;
                num = this.encodeMsg(buffer2, num, (JsonObject) obj3, (JsonObject) value);
                offset = this.writeBytes(buffer, offset, Pomelo.Protobuf.Encoder.encodeUInt32((uint) num));
                for (int i = 0; i < num; i++)
                {
                    buffer[offset] = buffer2[i];
                    offset++;
                }
            }
            return offset;
        }

        private byte[] encodeTag(string type, int tag)
        {
            int num = this.util.containType(type);
            return Pomelo.Protobuf.Encoder.encodeUInt32((uint) ((tag << 3) | num));
        }

        private int writeBytes(byte[] buffer, int offset, byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                buffer[offset] = bytes[i];
                offset++;
            }
            return offset;
        }

        private void writeDouble(byte[] buffer, ref int offset, object value)
        {
            this.WriteRawLittleEndian64(buffer, offset, (ulong) BitConverter.DoubleToInt64Bits((double) value));
            offset += 8;
        }

        private void writeFloat(byte[] buffer, ref int offset, object value)
        {
            this.writeBytes(buffer, offset, Pomelo.Protobuf.Encoder.encodeFloat((float) value));
            offset += 4;
        }

        private void writeInt32(byte[] buffer, ref int offset, object value)
        {
            offset = this.writeBytes(buffer, offset, Pomelo.Protobuf.Encoder.encodeSInt32(value.ToString()));
        }

        private void WriteRawLittleEndian64(byte[] buffer, int offset, ulong value)
        {
            buffer[offset++] = (byte) value;
            buffer[offset++] = (byte) (value >> 8);
            buffer[offset++] = (byte) (value >> 0x10);
            buffer[offset++] = (byte) (value >> 0x18);
            buffer[offset++] = (byte) (value >> 0x20);
            buffer[offset++] = (byte) (value >> 40);
            buffer[offset++] = (byte) (value >> 0x30);
            buffer[offset++] = (byte) (value >> 0x38);
        }

        private void writeString(byte[] buffer, ref int offset, object value)
        {
            int byteCount = Encoding.UTF8.GetByteCount(value.ToString());
            offset = this.writeBytes(buffer, offset, Pomelo.Protobuf.Encoder.encodeUInt32((uint) byteCount));
            byte[] bytes = Encoding.UTF8.GetBytes(value.ToString());
            this.writeBytes(buffer, offset, bytes);
            offset += byteCount;
        }

        private void writeUInt32(byte[] buffer, ref int offset, object value)
        {
            offset = this.writeBytes(buffer, offset, Pomelo.Protobuf.Encoder.encodeUInt32(value.ToString()));
        }

        private Pomelo.Protobuf.Encoder encoder { get; set; }

        private JsonObject protos { get; set; }

        private Util util { get; set; }
    }
}

