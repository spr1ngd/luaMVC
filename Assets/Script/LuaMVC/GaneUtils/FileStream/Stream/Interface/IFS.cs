
using System;
using System.Collections.Generic;

namespace LuaMVC
{
    public interface IFS 
    {
        T File2Object<T>(string fileName) where T : class, new();
        T[] File2Array<T>(string fileName) where T : class, new();
        IList<T> File2List<T>(string fileName) where T : class, new();

        void Object2File<T>(string fileName, T obj) where T : class, new();
        void Object2File<T>(string fileName, T obj, Action callback) where T : class, new();

        void Array2File<T>(string fileName, T[] arr) where T : class, new();
        void Array2File<T>(string fileName, T[] arr, Action callback) where T : class, new();

        void List2File<T>(string fileName, IList<T> list) where T : class, new();
        void List2File<T>(string fileName, IList<T> list, Action callback) where T : class, new();
    }
}