using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManager
{
    class GenericMethods
    {
        public List<T> GetAll<T>() where T : new()
        {
            try
            {
                var result = databaseConn.Table<T>().AsEnumerable<T>().ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
