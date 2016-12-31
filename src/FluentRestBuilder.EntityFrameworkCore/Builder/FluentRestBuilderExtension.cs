﻿// <copyright file="FluentRestBuilderExtension.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore.Common;
    using EntityFrameworkCore.MetaModel;
    using EntityFrameworkCore.Pipes;
    using EntityFrameworkCore.Pipes.Deletion;
    using EntityFrameworkCore.Pipes.Insertion;
    using EntityFrameworkCore.Pipes.QueryableSource;
    using EntityFrameworkCore.Pipes.Update;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;

    public static class FluentRestBuilderExtension
    {
        public static IFluentRestBuilder AddEntityFrameworkCorePipes<TContext>(
            this IFluentRestBuilder builder)
            where TContext : DbContext
        {
            RegisterEntityFrameworkRelatedServices<TContext>(builder.Services);
            RegisterPipes(builder.Services);
            RegisterTransformations(builder.Services);

            return builder;
        }

        private static void RegisterEntityFrameworkRelatedServices<TContext>(
            IServiceCollection collection)
            where TContext : DbContext
        {
            collection.TryAddScoped<IQueryableFactory, ContextQueryableFactory<TContext>>();
            collection.TryAddScoped(typeof(IQueryableFactory<>), typeof(QueryableFactory<>));
            collection.TryAddSingleton(typeof(IModelContainer<>), typeof(ModelContainer<>));
        }

        private static void RegisterPipes(IServiceCollection collection)
        {
            collection.TryAddScoped(
                typeof(IEntityInsertionPipeFactory<>), typeof(EntityInsertionPipeFactory<>));
            collection.TryAddScoped(
                typeof(IEntityUpdatePipeFactory<>), typeof(EntityUpdatePipeFactory<>));
            collection.TryAddScoped(
                typeof(IEntityDeletionPipeFactory<>), typeof(EntityDeletionPipeFactory<>));
            collection.TryAddScoped(
                typeof(IQueryableSourcePipeFactory<,>),
                typeof(QueryableSourcePipeFactory<,>));
        }

        private static void RegisterTransformations(IServiceCollection collection)
        {
            collection.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(AsyncQueryableTransformer<>));
        }
    }
}
