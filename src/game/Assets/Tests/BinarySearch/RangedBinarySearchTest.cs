using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TeamZ.Code.Helpers;

namespace TeamZ.Tests.BinarySearch
{
    public class RangedBinarySearchTest
    {
        private List<float> Items { get; } = new List<float> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        [Test]
        public void CheckSmallest()
        {
            var indexes = this.Items.RangedBinarySearch(1, 5, o => o);
            Assert.AreEqual((0, 4), indexes);
        }

        [Test]
        public void CheckBiggest()
        {
            var indexes = this.Items.RangedBinarySearch(8, 11, o => o);
            Assert.AreEqual((7, 10), indexes);
        }

        [Test]
        public void CheckwithMiddle()
        {
            var indexes = this.Items.RangedBinarySearch(1, 9, o => o);
            Assert.AreEqual((0, 8), indexes);
        }
    }
}
