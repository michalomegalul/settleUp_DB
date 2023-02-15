using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dluznik3.Module
{
    public class Group
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public List<Transactions> Transactions { get; set; }
    }
}
