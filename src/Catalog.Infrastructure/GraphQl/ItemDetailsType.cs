using Catalog.Domain.Entities;
using GraphQL.Types;

namespace Catalog.Infrastructure.GraphQl
{
    public class ItemDetailsType : ObjectGraphType<ItemDetails>
    {
        public ItemDetailsType()
        {
            Field(x => x.Id);
            Field(x => x.Identifier);
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.ImageUrl, nullable: true);
            Field(x => x.CategoryId, nullable: true);
            Field(x => x.Amount);
            Field(x => x.Price);
        }
    }
}

