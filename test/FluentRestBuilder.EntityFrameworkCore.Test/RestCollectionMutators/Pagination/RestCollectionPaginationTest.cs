﻿// <copyright file="RestCollectionPaginationTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.RestCollectionMutators.Pagination
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.RestCollectionMutators.Pagination;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.Test.Common.Mocks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Primitives;
    using Xunit;

    public class RestCollectionPaginationTest : ScopedDbContextTestBase
    {
        public RestCollectionPaginationTest()
        {
            this.CreateEntities();
        }

        [Fact]
        public async Task TestPagination()
        {
            const int page = 2;
            const int entriesPerPage = 3;
            var mutator = new RestCollectionPagination<Entity>(
                this.CreateAccessor(page, entriesPerPage),
                new QueryArgumentKeys());
            var result = await mutator.Apply(this.Context.Entities).ToListAsync();
            Assert.Equal(entriesPerPage, result.Count);
            const int firstIndex = entriesPerPage * (page - 1);
            Assert.True(
                result.Select(e => e.Id)
                    .SequenceEqual(Enumerable.Range(firstIndex + 1, entriesPerPage)));
        }

        [Fact]
        public async Task TestExceedingPagination()
        {
            const int page = 2;
            const int entriesPerPage = 6;
            const int expectedEntities = 10 - entriesPerPage;
            var mutator = new RestCollectionPagination<Entity>(
                this.CreateAccessor(page, entriesPerPage),
                new QueryArgumentKeys());
            var result = await mutator.Apply(this.Context.Entities).ToListAsync();
            Assert.Equal(expectedEntities, result.Count);
            const int firstIndex = entriesPerPage * (page - 1);
            Assert.True(
                result.Select(e => e.Id)
                    .SequenceEqual(Enumerable.Range(firstIndex + 1, expectedEntities)));
        }

        private IHttpContextAccessor CreateAccessor(int page, int entriesPerPage)
        {
            var keys = new QueryArgumentKeys();
            return new HttpContextAccessor
            {
                HttpContext = new DefaultHttpContext
                {
                    Request =
                    {
                        Query = new QueryCollection(new Dictionary<string, StringValues>
                        {
                            { keys.Page, new StringValues(page.ToString()) },
                            { keys.EntriesPerPage, new StringValues(entriesPerPage.ToString()) }
                        })
                    }
                }
            };
        }
    }
}