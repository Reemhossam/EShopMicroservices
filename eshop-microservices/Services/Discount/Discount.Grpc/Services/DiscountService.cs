﻿ using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(AppDbContext _db,ILogger<DiscountService> logger)
        :DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = _db.Coupons.FirstOrDefault(c=> c.ProductName == request.ProductName);
            if (coupon == null)
            {
                coupon= new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }
            logger.LogInformation("Discount is retrieved for productName: {productName}, Amount: {Amount}",
                coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
              throw new RpcException(new Status(StatusCode.InvalidArgument, "invalid request object."));
            _db.Coupons.Add(coupon);
            await _db.SaveChangesAsync();
            logger.LogInformation("Discount is successfully created. ProductName : {productName}",coupon.ProductName);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "invalid request object."));
            _db.Coupons.Update(coupon);
            await _db.SaveChangesAsync();
            logger.LogInformation("Discount is successfully updated. ProductName : {productName}", coupon.ProductName);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = _db.Coupons.FirstOrDefault(c => c.ProductName == request.ProductName);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName= {request.ProductName} is not found."));
            _db.Coupons.Remove(coupon);
            await _db.SaveChangesAsync();
            logger.LogInformation("Discount is successfully deleted. ProductName : {productName}", request.ProductName);
            return new DeleteDiscountResponse { Success = true};
        }
    }
}
