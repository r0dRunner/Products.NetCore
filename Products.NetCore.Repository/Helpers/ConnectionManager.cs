using System;
using System.Collections.Generic;
using System.Text;
using Products.NetCore.Repository.Helpers.Interfaces;
using System.Data.SqlClient;

namespace Products.NetCore.Repository.Helpers
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly string _connectionString;

        public ConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
