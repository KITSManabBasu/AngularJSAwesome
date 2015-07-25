using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWithAngularJs.Models
{
    public class CustomerOrders
    {
        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; }
    }
}