﻿// <copyright file="IFilterToTypeConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    public interface IFilterToTypeConverter<TFilter>
    {
        FilterConversionResult<TFilter> Parse(string filter);
    }
}
