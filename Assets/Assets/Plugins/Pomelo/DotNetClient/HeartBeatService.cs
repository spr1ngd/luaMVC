namespace Pomelo.DotNetClient
{
    using System;
    using System.Timers;

    public class HeartBeatService
    {
        private int interval;
        private Protocol protocol;
        public int timeout;
        private Timer timer;

        public HeartBeatService(int timeout, Protocol protocol)
        {
            this.interval = timeout * 0x3e8;
            this.protocol = protocol;
        }

        internal void resetTimeout()
        {
            this.timeout = 0;
        }

        public void sendHeartBeat(object source, ElapsedEventArgs e)
        {
            if (this.timeout > (this.interval * 2))
            {
                this.protocol.close();
            }
            this.protocol.send(PackageType.PKG_HEARTBEAT);
        }

        public void start()
        {
            if (this.interval >= 0x3e8)
            {
                this.timer = new Timer();
                this.timer.Interval = this.interval;
                this.timer.Elapsed += new ElapsedEventHandler(this.sendHeartBeat);
                this.timer.Enabled = true;
                this.timeout = 0;
            }
        }

        public void stop()
        {
            if (this.timer != null)
            {
                this.timer.Enabled = false;
                this.timer.Dispose();
            }
        }
    }
}

