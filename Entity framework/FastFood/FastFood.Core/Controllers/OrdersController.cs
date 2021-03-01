﻿namespace FastFood.Core.Controllers
{
    using System;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Models;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Orders;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var vieworders = this.context.Items
                 .Select(x => new CreateOrderViewModel { ItemId = x.Id, ItemName = x.Name })
                 .ToList();
            var viewOrder = new CreateOrderViewModel
            {
                Items = this.context.Items.Select(x => x.Id).ToList(),
                Employees = this.context.Employees.Select(x => x.Id).ToList(),
                ItemNames = this.context.Items.Select(x => x.Name).ToList(),
                EmploeyeNames=this.context.Employees.Select
            };
            var ordersViewModels=this.context.ProjectTo<>
            var orderViewModel = mapper.Map<CreateOrderViewModel>()

            return this.View(orderViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }
            var order = mapper.Map<Order>(model);
            var orderItem = mapper.Map<OrderItem>(model);
            orderItem.Order = order;
         
            this.context.Orders.Add(order);
            this.context.OrderItems.Add(orderItem);

            this.context.SaveChanges();


            return this.RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            var orders = this.context.Orders
                    .ProjectTo<OrderAllViewModel>(mapper.ConfigurationProvider)
                    .ToList();
            return this.View(orders);
        }
    }
}
