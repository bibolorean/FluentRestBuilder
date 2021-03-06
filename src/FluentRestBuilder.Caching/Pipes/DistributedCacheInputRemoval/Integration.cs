﻿// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using Caching.Pipes.DistributedCacheInputRemoval;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterDistributedCacheInputRemovalPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IDistributedCacheInputRemovalPipeFactory<>),
                typeof(DistributedCacheInputRemovalPipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> RemoveFromDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, string> keyFactory) =>
            pipe.GetRequiredService<IDistributedCacheInputRemovalPipeFactory<TInput>>()
                .Create(keyFactory, pipe);

        public static OutputPipe<TInput> RemoveFromDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, string key) =>
            pipe.RemoveFromDistributedCache(i => key);
    }
}
