using MISA.Common.Model;
using MISA.DataLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Service
{
    public class BaseService<MISAEntity>
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
            var dbContext = new DbContext<MISAEntity>();
            var serviceResult = new ServiceResult();
            var isValid = true;
            //Xử lý nghiệp vụ
            isValid = ValidateData(entity);
            //Gửi lên dataLayer thêm mới vào database
            if (isValid)
            {
                serviceResult.Data = dbContext.InsertObject(entity);
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.MISACode = "999";
            }
            return serviceResult;
        }
        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng cần validate</param>
        /// <returns>true: dữ liệu hợp lệ, false: dữ liệu k hợp lệ</returns>
        private bool ValidateData(MISAEntity entity)
        {
            return false;
        }
    }
}
