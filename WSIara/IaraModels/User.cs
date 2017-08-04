using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaraModels
{
    public class User
    {
        [Key]
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public DateTime updtDTime { get; set; }

        //controls
        public bool deleted { get; set; }
        public bool synchronizedInToMobile { get; set; }
        public bool synchronizedInToServer { get; set; }
    }
}
