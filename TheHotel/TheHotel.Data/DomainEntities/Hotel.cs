using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Data.DomainEntities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }

        public List<Customer> Customers { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }

        public List<Customer> Customers { get; set; }
    }
}
