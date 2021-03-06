﻿// <copyright file="ILinkGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Links
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    public interface ILinkGenerator<in TEntity>
    {
        IEnumerable<NamedLink> GenerateLinks(IUrlHelper urlHelper, TEntity entity);
    }
}
