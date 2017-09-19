using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaraModels
{
    public class PersonalTask
    {
        [Key]
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
        public bool finalized { get; set; }
        public bool synchronizedInToMobile { get; set; }
        public bool synchronizedInToServer { get; set; }
    }
}
