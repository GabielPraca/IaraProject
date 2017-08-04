using SQLite;
using System;
using System.Collections.Generic;

namespace SQLiteModels
{
    public class PersonalTask
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int personalTaskID { get; set; }
        public string email { get; set; }
        public bool sun { get; set; }
        public bool mon { get; set; }
        public bool tue { get; set; }
        public bool wed { get; set; }
        public bool thu { get; set; }
        public bool fri { get; set; }
        public bool sat { get; set; }
        public bool repeat { get; set; }
        public DateTime taskDay { get; set; }
        public string description { get; set; }

        //controls
        public bool deleted { get; set; }
        public bool synchronizedInToMobile { get; set; }
        public bool synchronizedInToServer { get; set; }

        public override string ToString()
        {
            return string.Format("[PersonalTask: personalTaskID={0}," +
                                                 "email={1}," +
                                                 "sun={2}," +
                                                 "mon={3}," +
                                                 "tue={4}," +
                                                 "wed={5}," +
                                                 "thu={6}," +
                                                 "fri={7}," +
                                                 "sat={8}," +
                                                 "repeat={9}," +
                                                 "taskDay={10}," +
                                                 "description={11}]" +
                                                 "deleted={12}" +
                                                 "synchronizedInToMobile={13}" +
                                                 "synchronizedInToServer={14}",
                                                 personalTaskID,
                                                 email,
                                                 sun,
                                                 mon,
                                                 tue,
                                                 wed,
                                                 thu,
                                                 fri,
                                                 sat,
                                                 repeat,
                                                 taskDay,
                                                 description, 
                                                 deleted,
                                                 synchronizedInToMobile,
                                                 synchronizedInToServer);
        }   
    }
}