using System;
using System.Collections.Generic;
using DTO_QLBS;
using DAL_QLBS;

namespace BLL_QLBS
{
    public class DiscountBLL
    {
        private DiscountDAL discountDAL;

        public DiscountBLL()
        {
            discountDAL = new DiscountDAL();
        }

        public List<DiscountDTO> GetAllDiscounts()
        {
            try
            {
                return discountDAL.GetAllDiscounts();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetAllDiscounts: {ex.Message}");
            }
        }

        public bool AddDiscount(DiscountDTO discount)
        {
            try
            {
                if (discount == null)
                    throw new ArgumentNullException(nameof(discount), "Discount cannot be null.");

                var newDiscount = new discount
                {
                    discount_code = discount.DiscountCode,
                    discount_name = discount.DiscountName,
                    description = discount.Description,
                    discount_percentage = discount.DiscountPercentage,
                    discount_amount = discount.DiscountAmount,
                    start_date = discount.StartDate,
                    end_date = discount.EndDate,
                    status = discount.Status.ToLower() == "active" ? (byte)1 : (byte)0
                };

                return discountDAL.AddDiscount(newDiscount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AddDiscount: {ex.Message}");
            }
        }

        public bool UpdateDiscount(DiscountDTO discount)
        {
            try
            {
                if (discount == null || discount.ID <= 0)
                    throw new ArgumentException("Invalid discount data.");

                var updatedDiscount = new discount
                {
                    id = discount.ID,
                    discount_code = discount.DiscountCode,
                    discount_name = discount.DiscountName,
                    description = discount.Description,
                    discount_percentage = discount.DiscountPercentage,
                    discount_amount = discount.DiscountAmount,
                    start_date = discount.StartDate,
                    end_date = discount.EndDate,
                    status = discount.Status.ToLower() == "active" ? (byte)1 : (byte)0
                };

                return discountDAL.UpdateDiscount(updatedDiscount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in UpdateDiscount: {ex.Message}");
            }
        }

        public bool DeleteDiscount(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid discount ID.");

                return discountDAL.DeleteDiscount(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in DeleteDiscount: {ex.Message}");
            }
        }
    }
}
