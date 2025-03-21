

namespace DuoWords.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Result<Category>>
    {
        public ICategoryRepository _categoryRepository { get; set; }
        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categorySelected = await _categoryRepository.SelectByName(request.Name);
            if (categorySelected != null)
                return Result<Category>.Failure("The field Name already exists");

            return Result<Category>.Success(await _categoryRepository.Create(new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                State = DomainState.Active
            }));
        }
    }
}
