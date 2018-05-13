using System;
using System.Collections.Generic;
using MediaToolkit.Util;
using Xunit;

namespace MediaToolkit.Test.Util
{
    public class ExtensionTest
    {
        public class ForEach
        {
            public ForEach()
            {
                this.CollectionUnderTest = new[] { "Foo", "Bar" };
            }

            public IEnumerable<string> CollectionUnderTest;

            [Fact]
            public void Will_Iterate_Through_EachItem_InCollection()
            {
                int expectedIterations = 2;
                int iterations = 0;

                this.CollectionUnderTest.ForEach(item => iterations++);

                Assert.Equal(expectedIterations, iterations);
            }

            [Fact]
            public void When_ActionIsNull_Throw_ArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    this.CollectionUnderTest.ForEach(null);
                });
            }
        }
    }
}
