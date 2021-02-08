﻿using Microsoft.AspNetCore.Mvc;
using MISA.Common.Model;
using MISA.Service;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.cukcuk.api.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Lấy danh sách khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: NhHung (04/02/2020)
        [HttpGet]
        public IActionResult GetCustomers()
        {
            CustomerService customerService = new CustomerService();
            var serviceResult = customerService.GetData();
            var customers = serviceResult.Data as List<Customer>;
            if (customers.Count == 0)
            {
                return StatusCode(204, serviceResult.Data);
            }
            else
            {
                return StatusCode(200, serviceResult.Data);
            }
        }
        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <returns>Response tương ứng</returns>

        [HttpPost]
        public IActionResult PostCustomer(Customer customer)
        {
            CustomerService customerService = new CustomerService();
            var res = customerService.InsertCustomer(customer);
            if (res.Success == false)
            {
                return StatusCode(400, res.Data);
            }
            else if (res.Success == true && (int)res.Data > 0)
            {
                return StatusCode(201, res.Data);
            }
            else
            {
                return StatusCode(200, res.Data);
            }
        }
    }
}
