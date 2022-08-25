using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int GroomerID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public DateTime CustomerSince { get; set; } = DateTime.Now;
        public GroomDay Day { get; set; }
        public GroomFrequency Frequency { get; set; }
    }
}