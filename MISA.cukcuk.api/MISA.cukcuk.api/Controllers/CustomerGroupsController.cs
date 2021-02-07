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
            var serviceResult = customerGroupService.GetCustomerGroup();
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
    }
}

