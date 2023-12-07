using Catalog.Application.Interfaces;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.GraphQl
{
	public class ItemQuery : ObjectGraphType
    {
		public ItemQuery(IItemGraphQlService itemGraphQlService)
		{
            FieldAsync<ListGraphType<ItemDetailsType>>(Name = "items",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "categoryId" },
                    new QueryArgument<IntGraphType> { Name = "pageSize" },
                    new QueryArgument<IntGraphType> { Name = "page" }
                    ),
                resolve: async x => await itemGraphQlService.GetItemList(
                    x.GetArgument<int>("categoryId"),
                    x.GetArgument<int>("pageSize"),
                    x.GetArgument<int>("page")
                    ));
        }
	}

    public class ItemDetailsSchema : Schema
    {
        public ItemDetailsSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<ItemQuery>();
        }
    }
}

