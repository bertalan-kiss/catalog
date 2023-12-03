using Catalog.Domain.Entities;
using GraphQL.Types;

namespace Catalog.Infrastructure.GraphQl
{
    public class CategoryDetailsType : ObjectGraphType<CategoryDetails>
    {
        public CategoryDetailsType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.ImageUrl);
            //Field(x => x.ParentId);
        }
    }
}

