using Dapper;
using MySql.Data.MySqlClient;
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

        #endregion

        #region method
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>collection object</returns>
        /// createdBy: NHHung
        public IEnumerable<object> GetAll()
        {
            _dbConnection = new MySqlConnection(_connectionString);
            var customers = _dbConnection.Query<object>("Proc_GetCustomers", commandType: CommandType.StoredProcedure);
            return customers;
        }
        /// <summary>
        /// Them moi ban ghi vao object
        /// </summary>
        /// <param name="entity">object can them moi</param>
        /// <return>So luong ban ghi</return>
        /// CreatedBy: NhHung
        public int InsertObject(object entity)
        {

            _dbConnection = new MySqlConnection(_connectionString);
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
            _dbConnection = new MySqlConnection(_connectionString);
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
            _dbConnection = new MySqlConnection(_connectionString);
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
