﻿// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Results.Options;
    using Storage;

    public static partial class Integration
    {
        public static Task<IActionResult> ToOptionsResult<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<IAllowedOptionsBuilder<TInput>, IEnumerable<HttpVerb>> builder)
            where TInput : class
        {
            var allowedOptionsBuilder = pipe.GetService<IAllowedOptionsBuilder<TInput>>();
            var httpContextStorage = pipe.GetService<IScopedStorage<HttpContext>>();
            IPipe resultPipe = new OptionsResultPipe<TInput>(
                input => builder(allowedOptionsBuilder),
                httpContextStorage,
                pipe);
            return resultPipe.Execute();
        }

        public static Task<IActionResult> ToOptionsResult<TInput>(
            this IOutputPipe<TInput> pipe,
            params HttpVerb[] verbs)
            where TInput : class
        {
            var httpContextStorage = pipe.GetService<IScopedStorage<HttpContext>>();
            IPipe resultPipe = new OptionsResultPipe<TInput>(
                input => verbs, httpContextStorage, pipe);
            return resultPipe.Execute();
        }
    }
}
