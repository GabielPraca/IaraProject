using System.Collections.Generic;

namespace StorageManager
{
    public interface IStoreManager
    {
        void SaveOrUpdateObject();
        bool CreateTable();
        void DeleteObject();
        List<T> GetAllObjects<T>();
        T GetUser<T>();
    }
}
