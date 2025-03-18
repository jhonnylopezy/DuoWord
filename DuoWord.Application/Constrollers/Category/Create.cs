using DuoWord.Presentation.Common;
using DuoWord.Presentation.DTO;
using FastEndpoints;

namespace DuoWord.Presentation.Constrollers.Category;

public class Create : Endpoint<Create_RequestDTO, Result<Category_ResponseDTO>>
{
    public override void Configure()
    {
        Post("/api/Category");
        AllowAnonymous();

    }
    public override async Task HandleAsync(Create_RequestDTO request,CancellationToken ct)
    {
        await SendAsync(Result<Category_ResponseDTO>.Success(new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            State="Active"
        }));

    }
}
