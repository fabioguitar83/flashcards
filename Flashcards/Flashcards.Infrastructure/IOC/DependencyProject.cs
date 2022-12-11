using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Infrastructure.Configuration;
using Flashcards.Infrastructure.Database;
using Flashcards.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Flashcards.Infrastructure.IOC
{
    public static class DependencyProject
    {
        public static void AddDependenciesProject(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddTransient<MySqlConnection>(_ => new MySqlConnection(config.GetConnectionString("Flashcards_DB")));

            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
            
            serviceCollection.AddTransient<IClassRepository, ClassRepository>();
            serviceCollection.AddTransient<ILessonRepository, LessonRepository>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<IFlashcardRepository, FlashcardRepository>();

            serviceCollection.AddScoped(cfg => cfg.GetService<IOptions<AppSettings>>().Value);

        }
    }
}
