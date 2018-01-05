
using System;
using System.Collections.Generic;
using System.IO;
namespace LuaMVC
{
    public class FileStream : IFS
    {
        protected virtual string ReadFile( string filePath )
        {
            if(!File.Exists(filePath))
                throw new Exception("File is not exist in " + filePath);
            using (StreamReader reader = new StreamReader(filePath))
                return reader.ReadToEnd();
        }
        protected virtual void WriteFile( string filePath , string content )
        {
            if (!File.Exists(filePath))
                File.Create(filePath).Dispose();
            using (StreamWriter writer = new StreamWriter(filePath))
                writer.Write(content);
        }

        public virtual T File2Object<T>(string fileName) where T : class, new()
        {
            return new T();
        }

        public virtual T[] File2Array<T>(string fileName) where T : class, new()
        {
            return null;
        }

        public virtual IList<T> File2List<T>(string fileName) where T : class, new()
        {
            return null;
        }

        public virtual void Object2File<T>(string fileName, T obj) where T : class, new()
        {
            
        }

        public virtual void Object2File<T>(string fileName, T obj, Action callback) where T : class, new()
        {
            
        }

        public virtual void Array2File<T>(string fileName, T[] arr) where T : class, new()
        {
            
        }

        public virtual void Array2File<T>(string fileName, T[] arr, Action callback) where T : class, new()
        {
            
        }

        public virtual void List2File<T>(string fileName, IList<T> list) where T : class, new()
        {
           
        }

        public virtual void List2File<T>(string fileName, IList<T> list, Action callback) where T : class, new()
        {
            
        }
    }
}