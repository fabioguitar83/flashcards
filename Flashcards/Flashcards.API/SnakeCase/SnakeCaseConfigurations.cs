using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;

namespace Flashcards.API.SnakeCase
{

    //CONTRATOS COM SNAKE CASE FILTER
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((character, index) =>
                    index > 0 && char.IsUpper(character)
                        ? "_" + character
                        : character.ToString()))
                .ToLower();
        }
    }

    public class SnakecasingParameOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
            else
            {
                foreach (var item in operation.Parameters)
                {
                    item.Name = item.Name.ToSnakeCase();
                }
            }
        }
    }

    //QUERYS COM SNAKE CASE PROVIDER

    public class SnakeCaseQueryValueProviderFactory : IValueProviderFactory
    {
        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var valueProvider = new SnakeCaseQueryValueProvider(
                BindingSource.Query,
                context.ActionContext.HttpContext.Request.Query,
                CultureInfo.CurrentCulture);

            context.ValueProviders.Add(valueProvider);

            return Task.CompletedTask;
        }
    }
    public class SnakeCaseQueryValueProvider : QueryStringValueProvider, IValueProvider
    {
        public SnakeCaseQueryValueProvider(
            BindingSource bindingSource,
            IQueryCollection values,
            CultureInfo culture)
            : base(bindingSource, values, culture)
        {
        }

        public override bool ContainsPrefix(string prefix)
        {
            return base.ContainsPrefix(prefix.ToSnakeCase());
        }

        public override ValueProviderResult GetValue(string key)
        {
            return base.GetValue(key.ToSnakeCase());
        }
    }
}
