using MISA.Common.Model;
using MISA.DataLayer;

namespace MISA.Service
{
    public class CustomerGroupService:BaseService<CustomerGroup>
    {
        protected override bool ValidateData(CustomerGroup entity, ErrorMsg errorMsg = null)
        {
            return true;
        }
    }
}
