using Dapper;
using MISA.Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.DataLayer
{
    public class DbContext<MISAEntity>
    {
        #region DECLARE
        protected string _connectionString = "" +
            "Host = 103.124.92.43;" +
            "Port = 3306;" +
            "Database = MS1_17_NguyenHuuHung_CukCuk;" +
            "User Id = nvmanh;" +
            "Password = 12345678;";
        protected IDbConnection _dbConnection;
        #endregion

        #region Constructor
        public DbContext()
        {
            _dbConnection = new MySqlConnector.MySqlConnection(_connectionString);
        }
        #endregion
        public IEnumerable<MISAEntity> GetAll()
        {
            var className = typeof(MISAEntity).Name;

            var entities = _dbConnection.Query<MISAEntity>($"select * from {className}", commandType: CommandType.Text);

            return entities;
        }
        #region method
        public IEnumerable<MISAEntity> GetAll(string sqlCommand , CommandType commandType = CommandType.Text)
        {
            var entities = _dbConnection.Query<MISAEntity>(sqlCommand, commandType: commandType);
            return entities;
        }
        /// <summary>
        /// Lấy dữ liệu theo nhiều tiêu chí
        /// </summary>
        /// <typeparam name="MISAEntity">Type của object</typeparam>
        /// <param name="sqlCommand">Câu lệnh truyền vào</param>
        /// <param name="parameters">Tham số truyền vào</param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<MISAEntity> GetData(string sqlCommand = null, object parameters = null, CommandType commandType = CommandType.Text)
        {
            var className = typeof(MISAEntity).Name;
            if (sqlCommand == null)
            {
                sqlCommand = $"Select * from {className}";
            }
            var data = _dbConnection.Query<MISAEntity>(sqlCommand, param: parameters, commandType: commandType);
            return data;
        }
        /// <summary>
        /// Them moi ban ghi vao object
        /// </summary>
        /// <param name="entity">object can them moi</param>
        /// <return>So luong ban ghi</return>
        /// CreatedBy: NhHung
        public int InsertObject(object entity)
        {
            var sqlPropName = string.Empty;
            var sqlPropValue = string.Empty;
            var className = typeof(MISAEntity).Name;
            // Lấy các property của object
            var properties = typeof(MISAEntity).GetProperties();
            // Duyệt property, lấy tên và giá trị của property
            //(tên là tên param trong câu truy vấn sql)
            // (value: giá trị là giá trị param tương ứng trong câu lênh SQL)

            foreach (var property in properties)
            {
                var propName = property.Name;
                var propValue = property.GetValue(entity);
                sqlPropName = sqlPropName + $",{propName}";
                if (property.PropertyType == typeof(Guid) || property.PropertyType == typeof(string))
                {
                    if (propName.ToLower() == $"{className}Id".ToLower())
                        sqlPropValue = sqlPropValue + $",'{Guid.NewGuid()}'";
                    else
                        sqlPropValue = sqlPropValue + $",'{propValue}'";
                }
                else if (property.PropertyType == typeof(Guid?)) {
                    if (propValue == null)
                    {
                        sqlPropValue = sqlPropValue + $",NULL";
                    }
                    else
                    {
                        sqlPropValue = sqlPropValue + $",'{propValue}'";
                    }
                }
                else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    var dateTime = (DateTime)propValue;
                    string dateTimeString = dateTime.ToString("yyyy-MM-dd hh:mm:ss");
                    sqlPropValue = sqlPropValue + $",'{dateTimeString}'";
                }
                else
                {
                    sqlPropValue = sqlPropValue + $",{propValue}";
                }
            }
            sqlPropName = sqlPropName.Remove(0, 1);
            sqlPropValue = sqlPropValue.Remove(0, 1);
            var sqlInsertFinal = $"insert into {className} ({sqlPropName}) value ({sqlPropValue})";
            var res = _dbConnection.Execute(sqlInsertFinal, commandType: CommandType.Text);
            return res;
        }
        #endregion
    }
}
