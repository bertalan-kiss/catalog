using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IValidator<Category> categoryValidator;
    private readonly IMessageSenderService messageSenderService;
    private readonly ILogger<CategoryService> logger;

    public CategoryService(ICategoryRepository categoryRepository, IValidator<Category> categoryValidator, IMessageSenderService messageSenderService, ILogger<CategoryService> logger)
    {
        this.categoryRepository = categoryRepository;
        this.categoryValidator = categoryValidator;
        this.messageSenderService = messageSenderService;
        this.logger = logger;
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

        await PublishUpdateMessage(category);
    }

    private async Task PublishUpdateMessage(Category category)
    {
        try
        {
            await messageSenderService.SendAsync(new Domain.Messages.CategoryUpdatedMessage
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl,
                ParentCategoryId = category.Parent?.Id
            });
        }
        catch (MessageSendoutFailedException ex)
        {
            logger.Log(LogLevel.Error, ex, ex?.Message);
        }
    }
}

