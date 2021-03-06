﻿// <copyright file="IOrderByExpressionFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest.Expressions
{
    public interface IOrderByExpressionFactory<TEntity>
    {
        IOrderByExpression<TEntity> Create(OrderByDirection direction);
    }
}
