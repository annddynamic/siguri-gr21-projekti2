using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{  
   
   
    public class RegisterReq
    {
        public string call { get; set; }
        public Person person;

    }

    [Serializable]
     public class Person
    {
        public string emri { get; set; }
        public string mbiemri { get; set; }
        public string username { get; set; }
        public string fjalekalimi { get; set; }
        public string konfirmoFjalkalimin { get; set; }

    }

}
