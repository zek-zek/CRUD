using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRUD
{
    public class person
    {
        private string fname, lname, email;
        private int age;
        public person(string _Fname, String _Lname, int _Age, string _Email)
        {
            this.Fname = _Fname;
            this.Lname = _Lname;
            this.Age = _Age;
            this.Email = _Email;
        }

        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public string Email { get => email; set => email = value; }
        public int Age { get => age; set => age = value; }

       
    }
}
