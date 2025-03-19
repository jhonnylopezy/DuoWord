

using DuoWord.Presentation.DTO;
using DuoWord.SharedKernel.Common;
using FastEndpoints;

namespace DuoWord.Presentation.Constrollers.Category;

public class Delete : Endpoint<CategoryById_RequestDTO,Result<string>>
{
    public override void Configure()
    {
        Delete("/api/Category/{Id}");
        AllowAnonymous();

    }
    public override async Task HandleAsync(CategoryById_RequestDTO request, CancellationToken ct)
    {
        await SendAsync(Result<string>.Success("exito"));
    }
}
