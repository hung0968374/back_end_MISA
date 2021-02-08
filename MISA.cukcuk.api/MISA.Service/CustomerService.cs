using System;
using System.Collections.Generic;
using System.Text;
using MISA.Common.Model;
using MISA.DataLayer;

namespace MISA.Service
{
    public class CustomerService
    {
        public ServiceResult GetCustomers()
        {
            var serviceResult = new ServiceResult();
            var dbContext = new CustomerRepostory();
            serviceResult.Data = dbContext.GetData();
            //serviceResult.Data = dbContext.GetData<Customer>(" SELECT * FROM Customer c WHERE c.CustomerCode LIKE CONCAT('%',@CustomerCode,'%') AND PhoneNumber LIKE CONCAT('%',@PhoneNumber,'%');", new { CustomerCode = "KH88617" , phoneNumber = "09"});
            return serviceResult;
        }
        public ServiceResult GetCustomersTop100()
        {
            var serviceResult = new ServiceResult();
            var dbContext = new CustomerRepostory();
            serviceResult.Data = dbContext.GetCustomerTop100();
            return serviceResult;
        }
        public ServiceResult InsertCustomer(Customer customer)
        {
            var serviceResult = new ServiceResult();
            var errorMsg = new ErrorMsg();
            var dbContext = new CustomerRepostory();
          
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
