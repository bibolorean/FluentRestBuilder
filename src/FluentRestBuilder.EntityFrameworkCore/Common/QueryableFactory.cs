﻿// <copyright file="QueryableFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Common
{
    using System.Linq;

    public class QueryableFactory<TEntity> : IQueryableFactory<TEntity>
        where TEntity : class
    {
        public QueryableFactory(IQueryableFactory queryableFactory)
        {
            this.Queryable = queryableFactory.Resolve<TEntity>();
        }

        public IQueryable<TEntity> Queryable { get; }
    }
}