﻿// <copyright file="FilterToIntegerConverterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.FilterByClientRequest.Converters
{
    using System.Globalization;
    using FluentRestBuilder.Pipes.FilterByClientRequest.Converters;
    using Mocks;
    using Xunit;

    public class FilterToIntegerConverterTest
    {
        private readonly FilterToIntegerConverter converter;

        public FilterToIntegerConverterTest()
        {
            this.converter = new FilterToIntegerConverter(new CultureInfoConversionPriority());
            new CultureInfo("fr-FR").AssignAsCurrentUiCulture();
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("-1", -1)]
        public void TestConversion(string filter, int expectedResult)
        {
            var result = this.converter.Parse(filter);
            Assert.True(result.Success);
            Assert.Equal(expectedResult, result.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("true")]
        public void TestFailure(string filter)
        {
            var result = this.converter.Parse(filter);
            Assert.False(result.Success);
        }
    }
}
