using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivan
{
    public class user
    {
        public int id { get; set; }
        public string role { get; set; }

        public user(int id, string role)
        {
            this.id = id;
            this.role = role;
        }
    }
}
