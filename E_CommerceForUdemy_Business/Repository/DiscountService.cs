﻿using E_CommerceForUdemy_Business.RabbitMQSender;
using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_DataAccess;
using E_CommerceForUdemy_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_Business.Repository
{
    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRabbitMQMessageSender _messageSender;
        public DiscountService(ApplicationDbContext context,IRabbitMQMessageSender messageSender)
        {
            _context = context;
            _messageSender = messageSender;
        }

        public async Task<string> CouponCode(Discount discount)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();
            _messageSender.SendMessage(discount, "discountQueue");
            return discount.CouponCode;
        }

        public async Task<bool> ImplementCoupon(string couponCode)
        {
            var result = await _context.Discounts.FirstOrDefaultAsync(x => x.CouponCode.ToLower().Equals(couponCode.ToLower()));
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
