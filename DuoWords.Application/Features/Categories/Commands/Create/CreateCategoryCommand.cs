using DuoWord.Domain.Entities;
using DuoWord.Domain.Interfaces;
using DuoWord.SharedKernel.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoWords.Application.Features.Categories.Commands.Create
{
    public record CreateCategoryCommand : IRequest<Result<Category>>
    {
        public string Name { get; init; }
    }
}
