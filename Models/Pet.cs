using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public Customer? Customer { get; set; }
        public int SerialNum { get; set; }
        public string Breed { get; set; } = string.Empty;
        public string VisualDes { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;

    }
}