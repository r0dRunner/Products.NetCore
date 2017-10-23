using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Products.NetCore.Entity;
using Products.NetCore.Repository.Helpers.Interfaces;
using Products.NetCore.Repository.Interfaces;

namespace Products.NetCore.Repository
{
    public class ProductRepository : IProductRepository
    {
        #region Properties
        private readonly IConnectionManager _connectionManager;
        #endregion

        #region Constructors
        public ProductRepository(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<ProductEntity>> RetrieveAsync()
        {
            IEnumerable<ProductEntity> result = new List<ProductEntity>();

            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("RetrieveProducts", connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    result = await ToProductEntities(reader);
                }
            }

            return result;
        }

        public async Task<ProductEntity> RetrieveByIdAsync(Guid id)
        {
            ProductEntity result = null;

            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("RetrieveProductById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    result = ToProductEntity(reader);
                }
            }

            return result;
        }

        public async Task<IEnumerable<ProductEntity>> RetrieveByNameAsync(string name)
        {
            IEnumerable<ProductEntity> result = new List<ProductEntity>();

            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("RetrieveProductsByName", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", name);
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    result = await ToProductEntities(reader);
                }
            }

            return result;
        }

        public async Task<ProductEntity> CreateAsync(ProductEntity entity)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("CreateProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                if (entity.Id != Guid.Empty)
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                }
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@DeliveryPrice", entity.DeliveryPrice);
                await connection.OpenAsync();

                var Id = await command.ExecuteScalarAsync();
                entity.Id = Guid.Parse(Id.ToString());
            }

            return entity;
        }

        public async Task UpdateAsync(ProductEntity entity)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("UpdateProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", entity.Id);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@DeliveryPrice", entity.DeliveryPrice);
                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("DeleteProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        private static ProductEntity ToProductEntity(SqlDataReader reader)
        {
            var productEntity = new ProductEntity
            {
                Id = Guid.Parse(reader["Id"].ToString()),
                Name = reader["Name"].ToString(),
                Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                Price = Decimal.Parse(reader["Price"].ToString()),
                DeliveryPrice = Decimal.Parse(reader["DeliveryPrice"].ToString())
            };

            return productEntity;
        }

        private static async Task<IEnumerable<ProductEntity>> ToProductEntities(SqlDataReader reader)
        {
            var productEntities = new List<ProductEntity>();

            while (await reader.ReadAsync())
            {
                productEntities.Add(ToProductEntity(reader));
            }

            return productEntities;
        }
        #endregion
    }
}
