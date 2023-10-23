using System.Data;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Dapper;

namespace Catalog.Infrastructure.DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<int> Add(Category category)
        {
            var sql = @"
                INSERT INTO Category
                (
                    Name,
                    ImageUrl,
                    ParentId
                )
                OUTPUT INSERTED.Id
                VALUES
                (
                    @Name,
                    @ImageUrl,
                    @ParentId
                )";

            var parameters = new
            {
                Name = category.Name,
                ImageUrl = category.ImageUrl,
                ParentId = category.Parent?.Id
            };

            return await connection.QuerySingleAsync<int>(sql, parameters);
        }

        public async Task Delete(int id)
        {
            var sql = @"DELETE FROM Category WHERE Id = @Id";
            var parameters = new { Id = id };

            var rowsAffected = await connection.ExecuteAsync(sql, parameters);

            if (rowsAffected == 0)
                throw new CategoryNotFoundException($"Category not found with id: {id}");
        }

        public async Task<Category> Get(int id)
        {
            var sql = @"WITH cte AS
                (
                    SELECT     Id, Name, ImageUrl, ParentId
                    FROM       dbo.Category
                    WHERE      Id = @Id
                    UNION ALL
                    SELECT     c.Id, c.Name, c.ImageUrl, c.ParentId
                    FROM       dbo.Category c
                    INNER JOIN cte
                            ON cte.ParentId = c.Id
                )
                SELECT * FROM cte";

            var parameters = new { Id = id };

            var items = await connection.QueryAsync(sql, parameters);
            var mainItem = items.SingleOrDefault(i => i.Id == id);

            if (mainItem == null)
                throw new CategoryNotFoundException($"Category not found with id: {id}");

            return new Category
            {
                Id = mainItem.Id,
                Name = mainItem.Name,
                ImageUrl = mainItem.ImageUrl,
                Parent = mainItem.ParentId != null ? GetParentCategory(mainItem.ParentId, items) : null
            };
        }

        public async Task<IEnumerable<Category>> List()
        {
            var sql = "SELECT * FROM Category";

            var items = await connection.QueryAsync(sql);

            var result = new List<Category>();

            foreach (var item in items)
            {
                result.Add(new Category
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImageUrl = item.ImageUrl,
                    Parent = item.ParentId != null ? GetParentCategory(item.ParentId, items) : null
                });
            }

            return result;
        }

        public async Task Update(Category category)
        {
            var sql = @"UPDATE
                    Category
                SET
                    Name = @Name,
                    ImageUrl = @ImageUrl,
                    ParentId = @ParentId
                WHERE
                    Id = @Id";

            var parameters = new { Name = category.Name, ImageUrl = category.ImageUrl, ParentId = category.Parent?.Id, Id = category.Id };
            var rowsAffected = await connection.ExecuteAsync(sql, parameters);

            if (rowsAffected == 0)
                throw new CategoryNotFoundException($"Category not found with id: {category.Id}");
        }

        private Category GetParentCategory(int? id, IEnumerable<dynamic> items)
        {
            var parentItem = items.SingleOrDefault(i => i.Id == id);

            if (parentItem == null)
                throw new CategoryNotFoundException($"Parent category not found with id: {id}");

            return new Category
            {
                Id = parentItem.Id,
                Name = parentItem.Name,
                ImageUrl = parentItem.ImageUrl,
                Parent = parentItem.ParentId != null ? GetParentCategory(parentItem.ParentId, items) : null
            };
        }
    }
}

