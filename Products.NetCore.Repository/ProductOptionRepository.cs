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
    public class ProductOptionRepository : IProductOptionRepository
    {
        #region Properties
        public readonly IConnectionManager _connectionManager;
        #endregion

        #region Constructors
        public ProductOptionRepository(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<ProductOptionEntity>> RetrieveAsync()
        {
            IEnumerable<ProductOptionEntity> result = new List<ProductOptionEntity>();

            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("RetrieveProductOptions", connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    result = await ToProductOptionEntities(reader);
                }
            }

            return result;
        }

        public async Task<ProductOptionEntity> RetrieveByIdAsync(Guid id)
        {
            ProductOptionEntity result = null;

            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("RetrieveProductOptionsById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    result = ToProductOptionEntity(reader);
                }
            }

            return result;
        }

        public async Task<IEnumerable<ProductOptionEntity>> RetrieveByProductIdAsync(Guid productId)
        {
            IEnumerable<ProductOptionEntity> result = new List<ProductOptionEntity>();

            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("RetrieveProductOptionsByProductId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", productId);
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    result = await ToProductOptionEntities(reader);
                }
            }

            return result;
        }

        public async Task<ProductOptionEntity> CreateAsync(ProductOptionEntity entity)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("CreateProductOption", connection);
                command.CommandType = CommandType.StoredProcedure;
                if (entity.Id != Guid.Empty)
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                }
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);
                await connection.OpenAsync();

                var Id = await command.ExecuteScalarAsync();
                entity.Id = Guid.Parse(Id.ToString());
            }

            return entity;
        }

        public async Task UpdateAsync(ProductOptionEntity entity)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("UpdateProductOption", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", entity.Id);
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);
                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("DeleteProductOption", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        private ProductOptionEntity ToProductOptionEntity(SqlDataReader reader)
        {
            var productOptionEntity = new ProductOptionEntity
            {
                Id = Guid.Parse(reader["Id"].ToString()),
                ProductId = Guid.Parse(reader["ProductId"].ToString()),
                Name = reader["Name"].ToString(),
                Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString()
            };

            return productOptionEntity;
        }

        private async Task<IEnumerable<ProductOptionEntity>> ToProductOptionEntities(SqlDataReader reader)
        {
            var productOptionEntities = new List<ProductOptionEntity>();

            while (await reader.ReadAsync())
            {
                productOptionEntities.Add(ToProductOptionEntity(reader));
            }

            return productOptionEntities;
        }
        #endregion Methods
    }
}
