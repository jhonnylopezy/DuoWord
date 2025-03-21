


namespace DuoWords.Application.Features.Categories.Commands.Create
{
    public record CreateCategoryCommand : IRequest<Result<Category>>
    {
        public required string Name { get; init; }
    }
}
