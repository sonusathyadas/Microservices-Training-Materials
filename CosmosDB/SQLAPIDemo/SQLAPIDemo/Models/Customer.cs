using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAPIDemo.Models
{

    public class Customer
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public Order[] Orders { get; set; }
    }
    public class Order
    {

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Item[] Items { get; set; }
    }
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
