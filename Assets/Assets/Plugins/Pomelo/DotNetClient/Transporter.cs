namespace Pomelo.DotNetClient
{
    using System;
    using System.Net.Sockets;

    public class Transporter
    {
        private IAsyncResult asyncReceive;
        private IAsyncResult asyncSend;
        private byte[] buffer;
        private int bufferOffset;
        private byte[] headBuffer = new byte[4];
        public const int HeadLength = 4;
        private Action<byte[]> messageProcesser;
        private bool onReceiving;
        private bool onSending;
        private int pkgLength;
        private Socket socket;
        private StateObject stateObject = new StateObject();
        private TransportState transportState;

        public Transporter(Socket socket, Action<byte[]> processer)
        {
            this.socket = socket;
            this.messageProcesser = processer;
            this.transportState = TransportState.readHead;
        }

        internal void close()
        {
            this.transportState = TransportState.closed;
            if (this.onReceiving)
            {
                this.socket.EndReceive(this.asyncReceive);
            }
            if (this.onSending)
            {
                this.socket.EndSend(this.asyncSend);
            }
        }

        private void endReceive(IAsyncResult asyncReceive)
        {
            if (this.transportState != TransportState.closed)
            {
                StateObject asyncState = (StateObject) asyncReceive.AsyncState;
                int length = this.socket.EndReceive(asyncReceive);
                this.onReceiving = false;
                if (length > 0)
                {
                    this.processBytes(asyncState.buffer, 0, length);
                    if (this.transportState != TransportState.closed)
                    {
                        this.receive();
                    }
                }
                else
                {
                    Console.WriteLine("server disconnect !");
                }
            }
        }

        private void print(byte[] bytes, int offset, int length)
        {
            for (int i = offset; i < length; i++)
            {
                Console.Write(Convert.ToString(bytes[i], 0x10) + " ");
            }
            Console.WriteLine();
        }

        internal void processBytes(byte[] bytes, int pos, int length)
        {
            if (this.transportState == TransportState.readHead)
            {
                this.readHead(bytes, pos, length);
            }
            else if (this.transportState == TransportState.readBody)
            {
                this.readBody(bytes, pos, length);
            }
        }

        private void readBody(byte[] bytes, int pos, int length)
        {
            if ((pos + this.pkgLength) <= length)
            {
                this.writeBytes(bytes, pos, this.pkgLength, 4, this.buffer);
                pos += this.pkgLength;
                this.messageProcesser(this.buffer);
                this.bufferOffset = 0;
                this.pkgLength = 0;
                if (this.transportState != TransportState.closed)
                {
                    this.transportState = TransportState.readHead;
                }
                if (pos < length)
                {
                    this.processBytes(bytes, pos, length);
                }
            }
            else
            {
                this.writeBytes(bytes, pos, length - pos, this.buffer);
                this.transportState = TransportState.readBody;
            }
        }

        private bool readHead(byte[] bytes, int pos, int length)
        {
            int num = length - pos;
            int num2 = 4 - this.bufferOffset;
            if (num >= num2)
            {
                this.writeBytes(bytes, pos, num2, this.headBuffer);
                this.pkgLength = ((this.headBuffer[1] << 0x10) + (this.headBuffer[2] << 8)) + this.headBuffer[3];
                this.buffer = new byte[4 + this.pkgLength];
                this.writeBytes(this.headBuffer, 0, num2, this.buffer);
                pos += num2;
                this.bufferOffset = 4;
                this.transportState = TransportState.readBody;
                if (pos < length)
                {
                    this.processBytes(bytes, pos, length);
                }
                return true;
            }
            this.writeBytes(bytes, pos, num, this.headBuffer);
            this.bufferOffset += num;
            return false;
        }

        public void receive()
        {
            this.asyncReceive = this.socket.BeginReceive(this.stateObject.buffer, 0, this.stateObject.buffer.Length, SocketFlags.None, new AsyncCallback(this.endReceive), this.stateObject);
            this.onReceiving = true;
        }

        public void send(byte[] buffer)
        {
            if (this.transportState != TransportState.closed)
            {
                this.asyncSend = this.socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(this.sendCallback), this.socket);
                this.onSending = true;
            }
        }

        private void sendCallback(IAsyncResult asyncSend)
        {
            if (this.transportState != TransportState.closed)
            {
                this.socket.EndSend(asyncSend);
                this.onSending = false;
            }
        }

        public void start()
        {
            this.receive();
        }

        private void writeBytes(byte[] source, int start, int length, byte[] target)
        {
            this.writeBytes(source, start, length, 0, target);
        }

        private void writeBytes(byte[] source, int start, int length, int offset, byte[] target)
        {
            for (int i = 0; i < length; i++)
            {
                target[offset + i] = source[start + i];
            }
        }
    }
}

