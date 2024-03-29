﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeCourse.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery() { UserId = _sharedIdentityService.GetUserId });
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrder([FromBody]CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);
            return CreateActionResultInstance(response);
        }

    }
}

