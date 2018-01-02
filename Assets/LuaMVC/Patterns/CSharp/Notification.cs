
namespace PureMVC.Patterns
{
    public interface INotification
    {
        string ToString();
        object Body { get; set; }
        string Name { get; }
        string Type { get; set; }
    }

    public class Notification : INotification
    {
        public object Body { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public Notification(string name) : this(name, null, null)
        {
        }
        public Notification(string name, object body) : this(name, body, null)
        {
        }
        public Notification(string name, object body, string type)
        {
            this.Name = name;
            this.Body = body;
            this.Type = type;
        }

        public override string ToString()
        {
            return ((("Notification Name: " + this.Name) + "\nBody:" + ((this.Body == null) ? "null" : this.Body.ToString())) + "\nType:" + ((this.Type == null) ? "null" : this.Type));
        }
    }
}

