﻿using AutoMapper;
using E_CommerceForUdemy_Business.RabbitMQOrderSender;
using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_Common;
using E_CommerceForUdemy_DataAccess;
using E_CommerceForUdemy_DataAccess.Data;
using E_CommerceForUdemy_DataAccess.ViewModel;
using ECommerce_ForUdemy_Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceForUdemy_Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IRabbitMQOrderMessageSender _messageSender;

        public OrderRepository(ApplicationDbContext db, IMapper mapper,IRabbitMQOrderMessageSender messageSender)
        {
            _db = db;
            _mapper = mapper;
            _messageSender = messageSender;
        }

        //public async Task<OrderHeaderDTO> CancelOrder(int id)
        //{
        //    var orderHeader = await _db.OrderHeaders.FindAsync(id);
        //    if (orderHeader == null)
        //    {
        //        return new OrderHeaderDTO();
        //    }

        //    if (orderHeader.Status == Keys.Status_Pending)
        //    {
        //        orderHeader.Status = Keys.Status_Cancelled;
        //        await _db.SaveChangesAsync();
        //    }
        //    if (orderHeader.Status == Keys.Status_Confirmed)
        //    {
        //        //refund
        //        var options = new Stripe.RefundCreateOptions
        //        {
        //            Reason = Stripe.RefundReasons.RequestedByCustomer,
        //            PaymentIntent = orderHeader.PaymentIntentId
        //        };

        //        var service = new Stripe.RefundService();
        //        Stripe.Refund refund = service.Create(options);

        //        orderHeader.Status = SD.Status_Refunded;
        //        await _db.SaveChangesAsync();
        //    }

        //    return _mapper.Map<OrderHeader, OrderHeaderDTO>(orderHeader);
        //}

        public async Task<OrderDTO> Create(OrderDTO objDTO)
        {
            try
            {
                var obj = _mapper.Map<OrderDTO, Order>(objDTO);
                _db.OrderHeaders.Add(obj.OrderHeader);
                await _db.SaveChangesAsync();

                foreach (var details in obj.OrderDetails)
                {
                    details.OrderHeaderId = obj.OrderHeader.Id;
                }
                 _db.OrderDetails.AddRange(obj.OrderDetails);
                await _db.SaveChangesAsync();

                return new OrderDTO()
                {
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDTO>(obj.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDTO>>(obj.OrderDetails).ToList()
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objDTO;
        }

        public async Task<int> Delete(int id)
        {
            var objHeader = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id);
            if (objHeader != null)
            {
                IEnumerable<OrderDetail> objDetail = _db.OrderDetails.Where(u => u.OrderHeaderId == id);

                _db.OrderDetails.RemoveRange(objDetail);
                _db.OrderHeaders.Remove(objHeader);
                return _db.SaveChanges();
            }
            return 0;
        }

        public async Task<OrderDTO> Get(int id)
        {
            Order order = new()
            {
                OrderHeader = _db.OrderHeaders.FirstOrDefault(u => u.Id == id),
                OrderDetails = _db.OrderDetails.Where(u => u.OrderHeaderId == id),
            };
            if (order != null)
            {
                return _mapper.Map<Order, OrderDTO>(order);
            }
            return new OrderDTO();
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null)
        {

            List<Order> OrderFromDb = new List<Order>();
            IEnumerable<OrderHeader> orderHeaderList = _db.OrderHeaders;
            IEnumerable<OrderDetail> orderDetailList = _db.OrderDetails;

            foreach (OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = orderDetailList.Where(u => u.OrderHeaderId == header.Id),
                };
                OrderFromDb.Add(order);
            }
            //do some filtering #TODO

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(OrderFromDb);

        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(int id)
        {
            var data = await _db.OrderHeaders.FindAsync(id);
            if (data == null)
            {
                return new OrderHeaderDTO();
            }
            if (data.Status == Keys.Status_Pending)
            {
                data.Status = Keys.Status_Confirmed;
                await _db.SaveChangesAsync();
                var response = _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
                _messageSender.SendMessage(response, "orderServiceQueue");

                return response;
            }
            return new OrderHeaderDTO();
        }

        public async Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO objDTO)
        {
            if (objDTO != null)
            {
                var orderHeaderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == objDTO.Id);
                orderHeaderFromDb.FirstName = objDTO.FirstName;
                orderHeaderFromDb.Phone = objDTO.Phone;
                orderHeaderFromDb.Address = objDTO.Address;
                orderHeaderFromDb.AdditionalInformation = objDTO.AdditionalInformation;
                orderHeaderFromDb.PostalCode = objDTO.PostalCode;
                orderHeaderFromDb.Status = objDTO.Status;

                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(orderHeaderFromDb);
            }
            return new OrderHeaderDTO();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _db.OrderHeaders.FindAsync(orderId);
            if (data == null)
            {
                return false;
            }
            data.Status = status;
            if (status == Keys.Status_Shipped)
            {
                data.ShippingDate = DateTime.Now;
            }
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
