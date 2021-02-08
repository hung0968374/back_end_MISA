using MISA.Common.Model;
using MISA.Service.Interfaces;

namespace MISA.Service.Services
{
    public class CustomerServiceV2:BaseService<Customer>, ICustomerService
    {
        protected override bool ValidateData(Customer entity, ErrorMsg errorMsg = null)
        {
            return true;
        }
    }
}
