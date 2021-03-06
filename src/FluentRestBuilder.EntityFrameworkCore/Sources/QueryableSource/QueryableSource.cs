// <copyright file="QueryableSource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Sources;
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;

    public class QueryableSource<TEntity> : SourceBase<IQueryable<TEntity>>
        where TEntity : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public QueryableSource(
            IScopedStorage<DbContext> contextStorage,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.contextStorage = contextStorage;
        }

        protected override Task<IQueryable<TEntity>> GetOutput()
        {
            IQueryable<TEntity> queryable = this.contextStorage.Value.Set<TEntity>();
            return Task.FromResult(queryable);
        }
    }
}
