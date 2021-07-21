using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    [Serializable]
    public class loginReq
    {
        public string call { get; set; }
        public Data data;
    }

    [Serializable]
    public class Data
    {
     
        public string username { get; set; }
        public string fjalekalimi { get; set; }

    }

}
