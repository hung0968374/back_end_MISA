using System;
using System.Collections.Generic;
using System.Text;
using MISA.Common.Model;
using MISA.DataLayer;

namespace MISA.Service
{
    public class CustomerService:BaseService<Customer>
    {
        public override ServiceResult GetData()
        {
            var serviceResult = new ServiceResult();
            var dbContext = new CustomerRepostory();
            serviceResult.Data = dbContext.GetCustomerTop100();
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
            var isValid = true;
            isValid = ValidateData(customer,  errorMsg);
            if (isValid == true)
            {
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
            else
            {
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
            }
            return serviceResult;
        }
        private bool ValidateData(Customer customer, ErrorMsg errorMsg)
        {
            var serviceResult = new ServiceResult();
            var dbContext = new CustomerRepostory();
            var isValid = true;
            // validate ma khach hang
            if (customer.CustomerCode == null || customer.CustomerCode == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.Common.Properties.Resources.ErrorService_EmptyCustomerCode);
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
                isValid = false;
            }

            var isExists = dbContext.checkCustomerCodeExists(customer.CustomerCode);
            if (isExists == true)
            {
                errorMsg.UserMsg.Add(MISA.Common.Properties.Resources.ErrorService_DuplicateCustomerCode);
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
                isValid = false;
            }

            //validate sdt
            isExists = dbContext.checkCustomerPhoneNumberExists(customer.PhoneNumber);
            if (isExists == true)
            {
                errorMsg.UserMsg.Add(MISA.Common.Properties.Resources.ErrorService_DuplicateCustomerPhoneNumber);
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
                isValid = false;
            }
            return isValid;
        }
    }
}
