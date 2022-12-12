﻿using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace Flashcards.Infrastructure.Mappers
{
    public static class RegisterMappings
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ClassEntityMap());
                config.ForDommel();
            });
        }
    }
}
