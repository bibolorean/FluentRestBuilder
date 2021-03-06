// <copyright file="CollectionMappingPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Pipes.CollectionMapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Builder;
    using FluentRestBuilder.Mocks;
    using FluentRestBuilder.Mocks.EntityFramework;
    using FluentRestBuilder.Pipes;
    using HypertextApplicationLanguage.Links;
    using HypertextApplicationLanguage.Pipes.CollectionMapping;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class CollectionMappingPipeTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public CollectionMappingPipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterSource()
                .RegisterCollectionMappingPipe()
                .Services
                .AddScoped<IRestCollectionLinkGenerator>(p => new MockLinkGenerator())
                .AddScoped(p => this.database.Create())
                .BuildServiceProvider();
            this.controller = new MockController(provider);
        }

        public void Dispose()
        {
            this.controller.Dispose();
        }

        [Fact]
        public async Task TestCollectionTransformation()
        {
            this.database.CreateEnumeratedEntities(10);
            var result = await this.controller.FromSource(this.database.Create().Entities)
                .MapToRestCollection(e => e.Name)
                .ToObjectResultOrDefault();
            Assert.NotNull(result);
            Assert.Contains("items", result.Embedded.Keys);
            Assert.IsAssignableFrom<IEnumerable<string>>(result.Embedded["items"]);
        }

        private class MockLinkGenerator : IRestCollectionLinkGenerator
        {
            public IEnumerable<NamedLink> GenerateLinks(PaginationMetaInfo paginationMetaInfo) =>
                Enumerable.Empty<NamedLink>();
        }
    }
}
