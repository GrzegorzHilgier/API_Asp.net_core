using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Data.SqlClient;

namespace InfrastructureSP
{
    public class ItemRepositorySP : IItemRepository
    {
        private readonly SqlConnection _sqlConnection;

        public ItemRepositorySP(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }
        public IUnitOfWork UnitOfWork { get; }
        public long Count { get; }

        public async Task<IEnumerable<Item>> GetAsync()
        {
            var result = await _sqlConnection.QueryAsync<Item>("GetAllItems", commandType: CommandType.StoredProcedure);
            return result.AsList();
        }

        public async Task<IEnumerable<Item>> GetAsync(int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> GetAsync(Guid id)
        {
            return await _sqlConnection.ExecuteScalarAsync<Item>("GetAllItems", new {Id = id.ToString()},
                commandType: CommandType.StoredProcedure);
        }

        public Item Add(Item item)
        {
            var result = _sqlConnection.ExecuteScalar<Item>
                ("InsertItem", item, commandType:CommandType.StoredProcedure);
            return result;
        }

        public Item Update(Item item)
        {
            var result = _sqlConnection.ExecuteScalar<Item>
            ("UpdateItem", item, commandType:
                CommandType.StoredProcedure);
            return result;
        }

        public Item Delete(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
