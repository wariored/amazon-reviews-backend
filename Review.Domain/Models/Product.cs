using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Review.Domain.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        
        public string Title { get; set; }

        public Store Store { get; set; }

        public IEnumerable<ProductReview> Reviews { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}