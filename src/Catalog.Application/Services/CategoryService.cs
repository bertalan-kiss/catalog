using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using FluentValidation;

namespace Catalog.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IValidator<Category> categoryValidator;

    public CategoryService(ICategoryRepository categoryRepository, IValidator<Category> categoryValidator)
    {
        this.categoryRepository = categoryRepository;
        this.categoryValidator = categoryValidator;
    }

    public async Task<int> Add(Category category)
    {
        await categoryValidator.ValidateAndThrowAsync(category);

        return await categoryRepository.Add(category);
    }

    public async Task Delete(int id)
    {
        await categoryRepository.Delete(id);
    }

    public async Task<Category> Get(int id)
    {
        return await categoryRepository.Get(id);
    }

    public async Task<IEnumerable<Category>> List()
    {
        return await categoryRepository.List();
    }

    public async Task Update(Category category)
    {
        await categoryValidator.ValidateAndThrowAsync(category);

        await categoryRepository.Update(category);
    }
}

