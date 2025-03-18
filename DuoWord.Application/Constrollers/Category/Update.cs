using DuoWord.Presentation.Common;
using DuoWord.Presentation.DTO;
using DuoWord.Presentation.DTO;
using FastEndpoints;

namespace DuoWord.Presentation.Constrollers.Category;

public class Update : Endpoint<UpdateCategory_RequestDTO, Result<Category_ResponseDTO>>
{
    public override void Configure()
    {
        Put("/api/Category");
        AllowAnonymous();

    }
    public override async Task HandleAsync(UpdateCategory_RequestDTO request, CancellationToken ct)
    {
        await SendAsync(Result<Category_ResponseDTO>.Success(new()
        {
            Id = Guid.NewGuid(),
            Name = "",
            State= request.State
        }));

    }
}
