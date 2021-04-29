using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyApp.Models
{
    public class Quote
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
    }
}
