using Flashcards.Domain.Interfaces;
using Flashcards.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Flashcards.Infrastructure.IOC
{
    public static class DependencyProject
    {
        public static void AddDependenciesProject(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<DBSession>();
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
