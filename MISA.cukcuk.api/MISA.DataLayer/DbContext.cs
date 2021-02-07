using Dapper;
using MISA.Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.DataLayer
{
    public class DbContext
    {
        #region DECLARE
        string _connectionString = "" +
            "Host = 103.124.92.43;" +
            "Port = 3306;" +
            "Database = MS1_17_NguyenHuuHung_CukCuk;" +
            "User Id = nvmanh;" +
            "Password = 12345678;";
        IDbConnection _dbConnection;
        #endregion

        #region Constructor
        public DbContext()
        {
            _dbConnection = new MySqlConnector.MySqlConnection(_connectionString);
        }
        #endregion

        #region method
        public IEnumerable<MISAEntity> GetAll<MISAEntity>()
        {
            var className = typeof(MISAEntity).Name;
            var entities = _dbConnection.Query<MISAEntity>($"select * from {className}", commandType: CommandType.Text);
            return entities;
        }
        public IEnumerable<MISAEntity> GetAll<MISAEntity>(string sqlCommand, CommandType commandType = CommandType.Text)
        {
            var className = typeof(MISAEntity).Name;
            var entities = _dbConnection.Query<MISAEntity>(sqlCommand, commandType: CommandType.Text);
            return entities;
        }
        
        /// <summary>
        /// Them moi ban ghi vao object
        /// </summary>
        /// <param name="entity">object can them moi</param>
        /// <return>So luong ban ghi</return>
        /// CreatedBy: NhHung
        public int InsertObject(object entity)
        {
            var res = _dbConnection.Execute("Proc_InsertCustomer", param: entity, commandType: CommandType.StoredProcedure);
            return res;
        }
        #endregion
        /// <summary>
        /// Kiểm tra mã khách hàng xem đã tồn tại hay chưa
        /// </summary>
        /// <param name="customerCode">Mã cần kiểm tra</param>
        /// <returns>true: đã tồn tại, false: chưa tồn tại</returns>
        public bool checkCustomerCodeExists (string customerCode)
        {
            var sql = $"SELECT CustomerCode From Customer AS C where C.CustomerCode = '{customerCode}'";
            var customerCodeExists = _dbConnection.Query<string>(sql).FirstOrDefault();
            if (customerCodeExists != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra số điện thoại của khách hàng xem đã tồn tại hay chưa
        /// </summary>
        /// <param name="customerCode">Số cần kiểm tra</param>
        /// <returns>true: đã tồn tại, false: chưa tồn tại</returns>
        public bool checkCustomerPhoneNumberExists(string phoneNumber)
        {
            var sqlSelectPhoneNumber = $"SELECT PhoneNumber From Customer AS C where C.PhoneNumber = '{phoneNumber}'";
            var phoneNumberExists = _dbConnection.Query<string>(sqlSelectPhoneNumber).FirstOrDefault();
            if (phoneNumberExists != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
