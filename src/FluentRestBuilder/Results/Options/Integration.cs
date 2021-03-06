﻿// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Results.Options;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterOptionsResultPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IOptionsResultFactory<>),
                typeof(OptionsResultFactory<>));
            builder.Services.TryAddScoped(
                typeof(IAllowedOptionsBuilder<>),
                typeof(AllowedOptionsBuilder<>));
            builder.Services.TryAddSingleton<IHttpVerbMap, HttpVerbMap>();
            return builder;
        }

        public static Task<IActionResult> ToOptionsResult<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<IAllowedOptionsBuilder<TInput>, IAllowedOptionsBuilder<TInput>> builder)
            where TInput : class
        {
            var allowedOptionsBuilder = pipe.GetService<IAllowedOptionsBuilder<TInput>>();
            return pipe.GetService<IOptionsResultFactory<TInput>>()
                .Create(input => builder(allowedOptionsBuilder).GenerateAllowedVerbs(input), pipe)
                .Execute();
        }

        public static Task<IActionResult> ToOptionsResult<TInput>(
            this IOutputPipe<TInput> pipe,
            params HttpVerb[] verbs)
            where TInput : class =>
            pipe.GetService<IOptionsResultFactory<TInput>>()
                .Create(input => verbs, pipe)
                .Execute();
    }
}
