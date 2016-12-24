﻿// <copyright file="RestCollectionOrderByTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.RestCollectionMutators.OrderBy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.RestCollectionMutators.OrderBy;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.Test.Common.Mocks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Primitives;
    using Xunit;

    public class RestCollectionOrderByTest : ScopedDbContextTestBase
    {
        public RestCollectionOrderByTest()
        {
            this.CreateEntities();
        }

        [Fact]
        public async Task TestAscending()
        {
            var mutator = new RestCollectionOrderBy<Entity>(
                this.CreateHttpContextAccessor(new[] { nameof(Entity.Name) }),
                this.Factory,
                new QueryArgumentKeys());
            var entities = await mutator.Apply(this.Context.Entities).ToListAsync();
            Assert.True(entities.SequenceEqual(entities.OrderBy(e => e.Name)));
        }

        [Fact]
        public async Task TestDescending()
        {
            var mutator = new RestCollectionOrderBy<Entity>(
                this.CreateHttpContextAccessor(new[] { $"!{nameof(Entity.Name)}" }),
                this.Factory,
                new QueryArgumentKeys());
            var entities = await mutator.Apply(this.Context.Entities).ToListAsync();
            Assert.True(entities.SequenceEqual(entities.OrderByDescending(e => e.Name)));
        }

        private IHttpContextAccessor CreateHttpContextAccessor(string[] searchables)
        {
            return new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext
                {
                    Request =
                    {
                        Query = new QueryCollection(new Dictionary<string, StringValues>
                        {
                            { new QueryArgumentKeys().OrderBy, new StringValues(searchables) }
                        })
                    }
                }
            };
        }
    }
}