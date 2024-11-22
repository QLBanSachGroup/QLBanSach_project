using System;

namespace DTO_QLBS
{
    public class DiscountDTO
    {
        public int ID { get; set; }
        public string DiscountCode { get; set; }
        public string DiscountName { get; set; }
        public string Description { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } // "Active" / "Inactive"
    }
}
