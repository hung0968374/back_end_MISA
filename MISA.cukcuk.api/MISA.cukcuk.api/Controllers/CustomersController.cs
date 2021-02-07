using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using MISA.Service;
using MISA.Common.Model;

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
            var connectionString = "" +
            "Host = 103.124.92.43;" +
            "Port = 3306;" +
            "Database = MS1_17_NguyenHuuHung_CukCuk;" +
            "User Id = nvmanh;" +
            "Password = 12345678;";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            var customers = dbConnection.Query<object>("SELECT * FROM Customer");
            return Ok(customers);
        }
        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <returns>Response tương ứng</returns>

        [HttpPost]
        public IActionResult PostCustomer(Customer customer)
        {
            //try
            //{
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
            
            //catch(Exception)
            //{
            //    return StatusCode(500, "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp");
            //}
            
        }
    }
}
