using FluentValidation;
using RA.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA.Models.Validators
{
    
    public class RestaurantQueryValidators : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };
        private string[] allowedSortByColumnNames =
        {nameof(Restaurant.Name),nameof(Restaurant.Category),nameof(Restaurant.Description)};
        public RestaurantQueryValidators()
        {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"Page size must in [{string.Join(",", allowedPageSizes)}]");
                }
            });
            RuleFor(r => r.SortBy).Must(
                value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
            
            
        }

    }
    
}
