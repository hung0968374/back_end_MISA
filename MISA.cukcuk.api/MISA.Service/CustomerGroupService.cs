using MISA.Common.Model;
using MISA.DataLayer;

namespace MISA.Service
{
    public class CustomerGroupService:BaseService<CustomerGroup>
    {
        public ServiceResult InsertCustomerGroup(CustomerGroup customerGroup)
        {
            var serviceResult = new ServiceResult();
            var dbContext = new DbContext<CustomerGroup>();
            serviceResult.Data = dbContext.InsertObject(customerGroup);
            return serviceResult;
        }
    }
}
