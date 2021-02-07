using System;
using System.Collections.Generic;
using System.Text;
using MISA.Common.Model;
using MISA.DataLayer;

namespace MISA.Service
{
    public class CustomerService
    {
        public ServiceResult InsertCustomer(Customer customer)
        {
            var serviceResult = new ServiceResult();
            var errorMsg = new ErrorMsg();
            DbContext dbContext = new DbContext();
          
            // validate ma khach hang
            if (customer.CustomerCode == null || customer.CustomerCode == string.Empty)
            {
                errorMsg.UserMsg = MISA.Common.Properties.Resources.ErrorService_EmptyCustomerCode;
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
                return serviceResult;
            }
            
            var isExists = dbContext.checkCustomerCodeExists(customer.CustomerCode);
            if (isExists == true)
            {
                errorMsg.UserMsg = MISA.Common.Properties.Resources.ErrorService_DuplicateCustomerCode;
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
                return  serviceResult;
            }

            //validate sdt
            isExists = dbContext.checkCustomerPhoneNumberExists(customer.PhoneNumber);
            if (isExists == true)
            {
                errorMsg.UserMsg = MISA.Common.Properties.Resources.ErrorService_DuplicateCustomerPhoneNumber;
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
                return serviceResult;
            }
            ////////// validate du lieu ok thi thuc hien them moi
            var res = dbContext.InsertObject(customer);
            if (res > 0)
            {
                serviceResult.Success = true;
                serviceResult.Data = res;
                return serviceResult;
            }
            else
            {
                serviceResult.Success = true;
                serviceResult.Data = res;
                return serviceResult;
            }
        }
    }
}
