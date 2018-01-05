
namespace LuaMVC
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

        /// <summary>
        /// The filename does not need to add extension.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public override T File2Object<T>(string fileName)
        {
            return SimpleJson.SimpleJson.DeserializeObject<T>(ReadFile(FilePath.JsonConfigFilePath(fileName + ".json")));
        }

        /// <summary>
        /// The filename does not need to add extension.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override void Object2File<T>(string fileName, T obj)
        {
            WriteFile(FilePath.JsonConfigFilePath(fileName + ".json"), SimpleJson.SimpleJson.SerializeObject(obj));
        }
    }
}