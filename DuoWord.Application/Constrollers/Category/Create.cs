using DuoWord.SharedKernel.Common;
using DuoWord.Presentation.DTO;
using FastEndpoints;
using MediatR;
using DuoWords.Application.Features.Categories.Commands.Create;

namespace DuoWord.Presentation.Constrollers.Category;

public class Create : Endpoint<Create_RequestDTO, Result<Category_ResponseDTO>>
{
    private IMediator _mediator;
    public Create(IMediator mediator)
    {
        this._mediator=mediator;
    }
    public override void Configure()
    {
        Post("/api/Category");
        AllowAnonymous();

    }
    public override async Task HandleAsync(Create_RequestDTO request,CancellationToken ct)
    {
       await this._mediator.Send(new CreateCategoryCommand(){ Name= request .Name});

        //await SendAsync(Result<Category_ResponseDTO>.Success(new()
        //{
        //    Id = Guid.NewGuid(),
        //    Name = request.Name,
        //    State="Active"
        //}));

    }
}
