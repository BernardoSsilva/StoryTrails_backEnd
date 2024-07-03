using FluentValidation;
using StoryTrails.Comunication.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTrails.Application.Validators
{
    internal class CollectionValidator: AbstractValidator<CollectionJsonRequest>
    {
        public CollectionValidator()
        {
            RuleFor(collection => collection.CollectionObjective).GreaterThan(0).WithMessage("Collection objective must be bigger than 0");
            RuleFor(Collection => Collection.CollectionName).NotEmpty().WithMessage("Collection name is required");
            RuleFor(Collection => Collection.UserId).NotEmpty().WithMessage("User id is required");
        }
    }
}
