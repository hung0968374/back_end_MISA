using Microsoft.AspNetCore.Mvc;
using MISA.Common.Model;
using MISA.Service;
using System.Collections.Generic;

namespace MISA.cukcuk.api.Controllers
{
    [Route("api/v1/customer-groups")]
    [ApiController]
    public class CustomerGroupsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var customerGroupService = new CustomerGroupService();
            var serviceResult = customerGroupService.GetData();
            var customerGroups = serviceResult.Data as List<CustomerGroup>;
            if (customerGroups.Count == 0)
            {
                return StatusCode(204, serviceResult.Data);
            }
            else
            {
                return StatusCode(200, serviceResult.Data);
            }
        }
        [HttpPost]
        public IActionResult PostCustomerGroup(CustomerGroup customerGroup)
        {
          
            var customerService = new CustomerGroupService();
            var res = customerService.InsertCustomerGroup(customerGroup);
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

