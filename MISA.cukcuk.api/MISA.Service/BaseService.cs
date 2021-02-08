using MISA.Common.Model;
using MISA.DataLayer;
using MISA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Service
{
    public class BaseService<MISAEntity>:IBaseService<MISAEntity>
    {
        public virtual ServiceResult GetData()
        {
            var serviceResult = new ServiceResult();
            var dbContext = new DbContext<MISAEntity>();
            serviceResult.Data = dbContext.GetAll();
            return serviceResult;
        }
        /// <summary>
        /// THêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Trả về kết quả ServiceResult</returns>
        public virtual ServiceResult Insert(MISAEntity entity)
        {
            var serviceResult = new ServiceResult();
            var dbContext = new DbContext<MISAEntity>();
            var errorMsg = new ErrorMsg(); 
            //Xử lý nghiệp vụ
            var isValid = ValidateData(entity, errorMsg);
            //Gửi lên dataLayer thêm mới vào database
            if (isValid == true)
            {
                var res = dbContext.InsertObject(entity);
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
        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng cần validate</param>
        /// <returns>true: dữ liệu hợp lệ, false: dữ liệu k hợp lệ</returns>
        protected virtual bool ValidateData(MISAEntity entity, ErrorMsg errorMsg = null)
        {
            return true;
        }
    }
}
