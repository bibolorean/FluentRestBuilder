﻿// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Linq;
    using EntityFrameworkCore.Builder;
    using EntityFrameworkCore.Sources.QueryableSource;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreEntityFrameworkCore RegisterQueryableSource(
            this IFluentRestBuilderCoreEntityFrameworkCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IQueryableSourceFactory<>), typeof(QueryableSourceFactory<>));
            return builder;
        }

        public static OutputPipe<IQueryable<TEntity>> WithQueryable<TEntity>(
            this ControllerBase controller)
            where TEntity : class =>
            controller.HttpContext.RequestServices
                .GetService<IQueryableSourceFactory<TEntity>>()
                .Create(controller);
    }
}
