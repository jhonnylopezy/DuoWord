

using DuoWord.Presentation.DTO;
using DuoWord.SharedKernel.Common;
using FastEndpoints;

namespace DuoWord.Presentation.Constrollers.Category;

public class GetList : EndpointWithoutRequest<Result<Category_ResponseDTO>>
{
    public override void Configure()
    {
        Get("/api/Category/getList");
        AllowAnonymous();

    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(Result<Category_ResponseDTO>.Success(new()
        {
            Id= Guid.NewGuid(),
            Name = "nombre",
            State="Active"
        }));

    }
}
