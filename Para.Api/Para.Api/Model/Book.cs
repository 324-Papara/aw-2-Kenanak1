using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Para.Api;

namespace Para.Api;

public class Book
{

    [DisplayName("Book id")]
    public int Id { get; set; }



    [DisplayName("Book name")]
    public string Name { get; set; }



    [DisplayName("Book author info")]
    public string Author { get; set; }



    [DisplayName("Book page count")]
    [PageCount]
    public int PageCount { get; set; }


    [DisplayName("Book year")]
    public int Year { get; set; }

}
public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Book id is required.")
            .GreaterThan(0).WithMessage("Book id must be greater than 0.")
            .LessThanOrEqualTo(10000).WithMessage("Book id must be less than or equal to 10000.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Book name is required.")
            .MinimumLength(5).WithMessage("Book name must be at least 5 characters.")
            .MaximumLength(50).WithMessage("Book name cannot exceed 50 characters.");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Book author info is required.")
            .MinimumLength(5).WithMessage("Book author info must be at least 5 characters.")
            .MaximumLength(50).WithMessage("Book author info cannot exceed 50 characters.");

        RuleFor(x => x.PageCount)
            .NotEmpty().WithMessage("Book page count is required.")
            .InclusiveBetween(50, 400).WithMessage("Book page count must be between 50 and 400.");

        RuleFor(x => x.Year)
            .NotEmpty().WithMessage("Book year is required.")
            .InclusiveBetween(1900, 2024).WithMessage("Book year must be between 1900 and 2024.");
    }
}