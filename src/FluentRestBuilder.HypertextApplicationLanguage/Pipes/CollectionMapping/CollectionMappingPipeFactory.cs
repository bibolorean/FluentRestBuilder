﻿// <copyright file="CollectionMappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Pipes.CollectionMapping
{
    using System;
    using System.Linq;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Storage;
    using HypertextApplicationLanguage;
    using Links;

    public class CollectionMappingPipeFactory<TInput, TOutput> :
        ICollectionMappingPipeFactory<TInput, TOutput>
    {
        private readonly IRestCollectionLinkGenerator linkGenerator;
        private readonly ILinkAggregator linkAggregator;
        private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public CollectionMappingPipeFactory(
            IRestCollectionLinkGenerator linkGenerator,
            ILinkAggregator linkAggregator,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
            IQueryableTransformer<TInput> queryableTransformer)
        {
            this.linkGenerator = linkGenerator;
            this.linkAggregator = linkAggregator;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
            this.queryableTransformer = queryableTransformer;
        }

        public OutputPipe<RestEntityCollection> Create(
                Func<TInput, TOutput> mapping, IOutputPipe<IQueryable<TInput>> parent) =>
            new CollectionMappingPipe<TInput, TOutput>(
                mapping,
                this.linkGenerator,
                this.linkAggregator,
                this.paginationMetaInfoStorage,
                this.queryableTransformer,
                parent);
    }
}