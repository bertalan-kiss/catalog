using Catalog.Application.Interfaces;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.GraphQl
{
	public class CategoryQuery : ObjectGraphType
    {
		public CategoryQuery(ICategoryGraphQlService categoryGraphQlService)
		{
            FieldAsync<ListGraphType<CategoryDetailsType>>(Name = "categories", 
                resolve: async _ => await categoryGraphQlService.GetCategoryList());
        }
	}

    public class CategoryDetailsSchema : Schema
    {
        public CategoryDetailsSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<CategoryQuery>();
        }
    }
}

