using SQLite;
using System;

namespace SQLiteModels
{
    public class User
    {
        [PrimaryKey, Unique]
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public DateTime updtDTime { get; set; }

        //controls
        public bool deleted { get; set; }
        public bool synchronizedInToMobile { get; set; }
        public bool synchronizedInToServer { get; set; }

        public override string ToString()
        {
            return string.Format("[User: email={0}," +
                                         "userName={1}," +
                                         "password={2}," +
                                         "updtDTime={3}," + 
                                         "deleted={4}," +
                                         "synchronizedInToMobile={5}," +
                                         "synchronizedInToServer={6}]", 
                                          email,
                                          userName, 
                                          password, 
                                          updtDTime, 
                                          deleted, 
                                          synchronizedInToMobile, 
                                          synchronizedInToServer);
        }
    }
}