using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace EasyLogger.DbStorage.Interface
{
    public interface IAnyStorage<T>
        where T : class
    {
        ConcurrentDictionary<string, T> DataMap { get; }

        T GetByName(string name, string defaultName);


        void AddOrUpdate(string name, T val);


        void Remove(string name);


        void Clear();
    }
}
