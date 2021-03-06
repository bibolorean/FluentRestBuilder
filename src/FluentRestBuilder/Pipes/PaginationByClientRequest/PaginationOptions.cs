﻿// <copyright file="PaginationOptions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.PaginationByClientRequest
{
    public class PaginationOptions
    {
        public int DefaultEntriesPerPage { get; set; } = 10;

        public int MaxEntriesPerPage { get; set; } = 100;
    }
}
