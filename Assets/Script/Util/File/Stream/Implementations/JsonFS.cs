
namespace Game
{
    public class JsonFS : FileStream
    {
        private static JsonFS _instance;
        public static JsonFS Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new JsonFS();
                return _instance;
            }
        }

        public override T File2Object<T>(string fileName)
        {
            return SimpleJson.SimpleJson.DeserializeObject<T>(ReadFile(FilePath.JsonConfigFilePath(fileName)));
        }

        public override void Object2File<T>(string fileName, T obj)
        {
            WriteFile(FilePath.JsonConfigFilePath(fileName), SimpleJson.SimpleJson.SerializeObject(obj));
        }
    }
}