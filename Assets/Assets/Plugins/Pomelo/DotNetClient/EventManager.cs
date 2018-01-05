namespace Pomelo.DotNetClient
{
    using SimpleJson;
    using System;
    using System.Collections.Generic;

    public class EventManager : IDisposable
    {
        private Dictionary<uint, Action<JsonObject>> callBackMap = new Dictionary<uint, Action<JsonObject>>();
        private Dictionary<string, List<Action<JsonObject>>> eventMap = new Dictionary<string, List<Action<JsonObject>>>();

        public void AddCallBack(uint id, Action<JsonObject> callback)
        {
            if ((id > 0) && (callback != null))
            {
                this.callBackMap.Add(id, callback);
            }
        }

        public void AddOnEvent(string eventName, Action<JsonObject> callback)
        {
            List<Action<JsonObject>> list = null;
            if (this.eventMap.TryGetValue(eventName, out list))
            {
                list.Add(callback);
            }
            else
            {
                list = new List<Action<JsonObject>> {
                    callback
                };
                this.eventMap.Add(eventName, list);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            this.callBackMap.Clear();
            this.eventMap.Clear();
        }

        public void InvokeCallBack(uint id, JsonObject data)
        {
            if (this.callBackMap.ContainsKey(id))
            {
                this.callBackMap[id](data);
            }
        }

        public void InvokeOnEvent(string route, JsonObject msg)
        {
            if (this.eventMap.ContainsKey(route))
            {
                List<Action<JsonObject>> list = this.eventMap[route];
                foreach (Action<JsonObject> action in list)
                {
                    action(msg);
                }
            }
        }
    }
}

