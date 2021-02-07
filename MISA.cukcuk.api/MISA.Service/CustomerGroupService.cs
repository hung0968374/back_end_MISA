using MISA.Common.Model;
using MISA.DataLayer;

namespace MISA.Service
{
    public class CustomerGroupService
    {
        /// <summary>
        /// Lấy toàn bộ danh sách nhóm khách hàng
        /// </summary>
        /// <returns>ServiceResult</returns>
        /// CreatedBy: NHHUNG
        public ServiceResult GetCustomerGroup()
        {
            var serviceResult = new ServiceResult();
            var dbContext = new DbContext();
            serviceResult.Data = dbContext.GetAll<CustomerGroup>();
            return serviceResult;
        }
    }
}
