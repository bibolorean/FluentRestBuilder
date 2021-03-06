﻿// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;

    public static partial class Integration
    {
        public static IFluentRestBuilder AddCachingPipes(
            this IFluentRestBuilder builder)
        {
            new FluentRestBuilderCore(builder.Services)
                .RegisterDistributedCacheInputStoragePipe()
                .RegisterDistributedCacheInputRemovalPipe()
                .RegisterDistributedCacheInputBridgePipe()
                .RegisterMemoryCacheInputStoragePipe()
                .RegisterMemoryCacheInputRemovalPipe()
                .RegisterMemoryCacheInputBridgePipe();
            return builder;
        }
    }
}
