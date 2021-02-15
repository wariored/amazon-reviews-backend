using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Review.Domain.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public Showroom Showroom { get; set; }

        public IEnumerable<Rate> Rates { get; set; }
    }
}