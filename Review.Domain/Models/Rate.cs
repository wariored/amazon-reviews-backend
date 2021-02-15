using System;

namespace Review.Domain.Models
{
    public class Rate
    {
        public string Description { get; set; }

        public Customer Customer { get; set; }
        
        private long _ratingValue;

        public long RatingValue
        {
            get => _ratingValue;
            set
            {
                if (value < 0 || value > 5)
                {
                    throw new ArgumentOutOfRangeException($"{value} is not valid. Rating value should be between 0 and 5");
                }
                _ratingValue = value;
            }
        }

        public string Title { get; set; }
    }
}