using System.Data;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Dapper;

namespace Catalog.Infrastructure.DataAccess.Repositories;

public class ItemRepository : BaseRepository, IItemRepository
{
    private readonly ICategoryRepository categoryRepository;

    public ItemRepository(ICategoryRepository categoryRepository, IDbConnection connection) : base(connection)
    {
        this.categoryRepository = categoryRepository;
    }

    public async Task<int> Add(Item item)
    {
        var sql = @"
            INSERT INTO Item
            (
                Identifier,
                Name,
                Description,
                ImageUrl,
                CategoryId,
                Price,
                Amount
            )
            OUTPUT INSERTED.Id
            VALUES
            (
                @Identifier,
                @Name,
                @Description,
                @ImageUrl,
                @CategoryId,
                @Price,
                @Amount
            )";

        var parameters = new
        {
            Identifier = item.Identifier,
            Name = item.Name,
            Description = item.Description,
            ImageUrl = item.ImageUrl,
            CategoryId = item.Category.Id,
            Price = item.Price,
            Amount = item.Amount
        };

        return await connection.QuerySingleAsync<int>(sql, parameters);
    }

    public async Task Delete(int id)
    {
        var sql = @"DELETE FROM Item WHERE Id = @Id";
        var parameters = new { Id = id };

        var rowsAffected = await connection.ExecuteAsync(sql, parameters);

        if (rowsAffected == 0)
            throw new ItemNotFoundException($"Item not found with id: {id}");
    }

    public async Task<Item> Get(int id)
    {
        var sql = "SELECT * FROM Item WHERE Id = @Id";

        var parameters = new { Id = id };
        var item = await connection.QuerySingleOrDefaultAsync(sql, parameters);

        if (item == null)
            throw new ItemNotFoundException($"Item not found with id: {id}");

        return new Item
        {
            Id = item.Id,
            Identifier = item.Identifier,
            Name = item.Name,
            Description = item.Description,
            ImageUrl = item.ImageUrl,
            Category = await categoryRepository.Get(item.CategoryId),
            Price = (float)item.Price,
            Amount = item.Amount
        };
    }

    public async Task<IEnumerable<Item>> List(int categoryId, int pageSize, int page)
    {
        var sql = @"SELECT * FROM Item
                    WHERE CategoryId = @CategoryId
                    ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        var parameters = new
        {
            CategoryId = categoryId,
            PageSize = pageSize,
            Offset = (page - 1) * pageSize
        };

        var items = await connection.QueryAsync(sql, parameters);

        var result = new List<Item>();

        foreach (var item in items)
        {
            result.Add(new Item
            {
                Id = item.Id,
                Identifier = item.Identifier,
                Name = item.Name,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                Category = await categoryRepository.Get(item.CategoryId),
                Price = (float)item.Price,
                Amount = item.Amount
            });
        }

        return result;
    }

    public async Task Update(Item item)
    {
        var sql = @"UPDATE
                    Item
                SET
                    Name = @Name,
                    Description = @Description,
                    ImageUrl = @ImageUrl,
                    CategoryId = @CategoryId,
                    Price = @Price,
                    Amount = @Amount
                WHERE
                    Id = @Id";

        var parameters = new
        {
            Name = item.Name,
            Description = item.Description,
            ImageUrl = item.ImageUrl,
            CategoryId = item.Category.Id,
            Price = item.Price,
            Amount = item.Amount,
            Id = item.Id
        };

        var rowsAffected = await connection.ExecuteAsync(sql, parameters);

        if (rowsAffected == 0)
            throw new ItemNotFoundException($"Item not found with id: {item.Id}");
    }
}

