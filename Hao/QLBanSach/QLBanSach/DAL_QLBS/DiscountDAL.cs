using System;
using System.Collections.Generic;
using System.Linq;
using DTO_QLBS;

namespace DAL_QLBS
{
    public class DiscountDAL
    {
        private BookManagementDataContext db;

        public DiscountDAL()
        {
            db = new BookManagementDataContext();
        }

        // Lấy tất cả giảm giá
        public List<DiscountDTO> GetAllDiscounts()
        {
            try
            {
                return (from d in db.discounts
                        select new DiscountDTO
                        {
                            ID = d.id,
                            DiscountCode = d.discount_code,
                            DiscountName = d.discount_name,
                            Description = d.description,
                            DiscountPercentage = d.discount_percentage,
                            DiscountAmount = d.discount_amount,
                            StartDate = d.start_date,
                            EndDate = d.end_date,
                            Status = d.status == 1 ? "Active" : "Inactive"
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching discounts: {ex.Message}");
            }
        }

        // Thêm giảm giá
        public bool AddDiscount(discount newDiscount)
        {
            try
            {
                if (newDiscount == null)
                    throw new ArgumentNullException(nameof(newDiscount), "Discount cannot be null.");

                db.discounts.InsertOnSubmit(newDiscount);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding discount: {ex.Message}");
            }
        }

        // Cập nhật giảm giá
        public bool UpdateDiscount(discount updatedDiscount)
        {
            try
            {
                if (updatedDiscount == null)
                    throw new ArgumentNullException(nameof(updatedDiscount), "Discount cannot be null.");

                var discount = db.discounts.FirstOrDefault(d => d.id == updatedDiscount.id);
                if (discount == null)
                    throw new InvalidOperationException("Discount not found in the database.");

                discount.discount_code = updatedDiscount.discount_code;
                discount.discount_name = updatedDiscount.discount_name;
                discount.description = updatedDiscount.description;
                discount.discount_percentage = updatedDiscount.discount_percentage;
                discount.discount_amount = updatedDiscount.discount_amount;
                discount.start_date = updatedDiscount.start_date;
                discount.end_date = updatedDiscount.end_date;
                discount.status = updatedDiscount.status;

                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating discount: {ex.Message}");
            }
        }

        // Xóa giảm giá
        public bool DeleteDiscount(int id)
        {
            try
            {
                var discount = db.discounts.FirstOrDefault(d => d.id == id);
                if (discount == null)
                    throw new InvalidOperationException("Discount not found in the database.");

                db.discounts.DeleteOnSubmit(discount);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting discount: {ex.Message}");
            }
        }
    }
}
