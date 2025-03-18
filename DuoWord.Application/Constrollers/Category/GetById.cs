

using DuoWord.Presentation.DTO;
using DuoWord.Presentation.Common;
using FastEndpoints;

namespace DuoWord.Presentation.Constrollers.Category;

public class GetById : Endpoint<CategoryById_RequestDTO,Result<Category_ResponseDTO>>
{
    public override void Configure()
    {
        Get("/api/category/{id}");
        AllowAnonymous();

    }
    public override async Task HandleAsync(CategoryById_RequestDTO request,CancellationToken ct)
    {

        await SendAsync(Result<Category_ResponseDTO>.Success(new()
        {
            Id = Guid.NewGuid(),
            Name = "Hola",
            State = "Active"
        }));

    }
}
