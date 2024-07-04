using FluentValidation;
using StoryTrails.Communication.Request;

namespace StoryTrails.Application.Validators
{
    public class BookValidator : AbstractValidator<BooksJsonRequest>
    {

        public BookValidator()
        {
            RuleFor(book => book.BookName).NotEmpty().WithMessage("Book name is required ");
            RuleFor(book => book.PagesAmount).GreaterThan(0).WithMessage("Book pages amount must be bigger than 0");
            RuleFor(book => book.Collection).NotEmpty().WithMessage("Book collection is required");
        }
    }
}
