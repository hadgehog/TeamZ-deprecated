using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TeamZ.Code.Helpers;

namespace TeamZ.Tests.BinarySearch
{
    public class NearestBinarySearchTest
    {
        private List<float> Items { get; } = new List<float> { 1, 2, 3, 4, 5, 6, 7, 8, 0, 10, 11 };

        [Test]
        public void CheckSmallest()
        {
            var index = this.Items.NearestBinarySearch(1, o => o);

            Assert.AreEqual(0, index);
        }

        [Test]
        public void CheckBiggets()
        {
            var index = this.Items.NearestBinarySearch(11, o => o);

            Assert.AreEqual(10, index);
        }

        [Test]
        public void CheckMiddle()
        {
            var index = this.Items.NearestBinarySearch(6, o => o);

            Assert.AreEqual(5, index);
        }

        [Test]
        public void CheckAboveMiddle()
        {
            var index = this.Items.NearestBinarySearch(8, o => o);

            Assert.AreEqual(7, index);
        }

        [Test]
        public void CheckUnderMiddle()
        {
            var index = this.Items.NearestBinarySearch(4, o => o);

            Assert.AreEqual(3, index);
        }

        [Test]
        public void CheckBeloweSet()
        {
            var index = this.Items.NearestBinarySearch(-1, o => o);

            Assert.AreEqual(0, index);
        }

        [Test]
        public void CheckAboveSet()
        {
            var index = this.Items.NearestBinarySearch(55, o => o);

            Assert.AreEqual(10, index);
        }
    }
}
