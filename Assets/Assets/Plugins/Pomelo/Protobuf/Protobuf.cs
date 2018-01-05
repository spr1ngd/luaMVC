namespace Pomelo.Protobuf
{
    using SimpleJson;
    using System;

    public class Protobuf
    {
        private MsgDecoder decoder;
        private MsgEncoder encoder;

        public Protobuf(JsonObject encodeProtos, JsonObject decodeProtos)
        {
            this.encoder = new MsgEncoder(encodeProtos);
            this.decoder = new MsgDecoder(decodeProtos);
        }

        public JsonObject decode(string route, byte[] buffer)
        {
            return this.decoder.decode(route, buffer);
        }

        public byte[] encode(string route, JsonObject msg)
        {
            return this.encoder.encode(route, msg);
        }
    }
}

