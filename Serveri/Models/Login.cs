using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveri.Models
{
    [Serializable]
    class LoginModel
    {

        public string username { get; set; }
        public string fjalekalimi { get; set; }
        public string saltedPWfromdb { get; set; }
        public string saltfromdb { get; set; }


    }
}
