using FluentAssertions;
using System;
using System.Linq;

namespace ShortenerUrl.Api.UnitTest.Common
{
    public static class Assertion
    {
        public static bool Match(object actual, object expected)
        {
            actual.Should().BeEquivalentTo(expected);
            return true;
        }

        public static bool Match(object actual, object expected, string[] excludedFields = null)
        {
            actual.Should().BeEquivalentTo(expected,
                config =>
                    config
                        .Excluding(x => excludedFields.Any(y => x.Path.EndsWith(y)))
                        .RespectingRuntimeTypes());

            return true;
        }
    }
}
