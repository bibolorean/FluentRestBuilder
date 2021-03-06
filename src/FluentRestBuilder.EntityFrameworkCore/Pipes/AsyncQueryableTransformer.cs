﻿// <copyright file="AsyncQueryableTransformer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.EntityFrameworkCore;

    public class AsyncQueryableTransformer<TEntity> : IQueryableTransformer<TEntity>
    {
        public Task<List<TEntity>> ToList(IQueryable<TEntity> queryable) => queryable.ToListAsync();

        public Task<TEntity> SingleOrDefault(IQueryable<TEntity> queryable) =>
            queryable.SingleOrDefaultAsync();

        public Task<TEntity> FirstOrDefault(IQueryable<TEntity> queryable) =>
            queryable.FirstOrDefaultAsync();

        public Task<int> Count(IQueryable<TEntity> queryable) =>
            queryable.CountAsync();
    }
}
