using System;
using System.Collections.Generic;
using System.Linq;
using TeamZ.Code.Helpers;
using UnityEngine;

namespace TeamZ.Assets.Code.Game.Navigation
{
    // @TODO: Figure out proper name
    public class OrderedMatrix<TValue>
    {
        private List<OrderedMatrixFirstLevelItem> matrix;

        public Func<TValue, float> FirstValueGetter { get; }
        public Func<TValue, float> SecondLevelGetter { get; }

        private class OrderedMatrixFirstLevelItem
        {
            public float X { get; set; }
            public List<OrderedMatrixSecondLevelItem<TValue>> Items { get; set; }
        }

        private class OrderedMatrixSecondLevelItem<T>
        {
            public float Y { get; set; }
            public T Item { get; set; }
        }

        public OrderedMatrix(Func<TValue, float> firstValueGetter, Func<TValue, float> secondLevelGetter)
        {
            this.matrix = new List<OrderedMatrixFirstLevelItem>();
            this.FirstValueGetter = firstValueGetter;
            this.SecondLevelGetter = secondLevelGetter;
        }

        public void Add(TValue value)
        {
            var x = this.FirstValueGetter(value);
            var y = this.SecondLevelGetter(value);

            var (xItem, empty) = this.matrix.NearestBinarySearchValue(x, o => o.X);
            if (empty || xItem.X != x)
            {
                var newXItem = new OrderedMatrixFirstLevelItem
                {
                    X = x,
                    Items = new List<OrderedMatrixSecondLevelItem<TValue>>{
                        new OrderedMatrixSecondLevelItem<TValue>{
                            Y = y,
                            Item = value
                        }
                    }
                };

                this.matrix.Add(newXItem);
                return;
            }

            xItem.Items.Add(new OrderedMatrixSecondLevelItem<TValue>
            {
                Item = value,
                Y = y
            });
        }

        public void Remove(TValue value)
        {
            var x = this.FirstValueGetter(value);
            var y = this.SecondLevelGetter(value);

            var (xItem, xEmpty) = this.matrix.ExactBinarySearchValue(x, o => o.X);
            if (xEmpty)
            {
                return;
            }

            var index = xItem.Items.ExactBinarySearch(y, o => o.Y);
            if (index == -1)
            {
                return;
            }

            xItem.Items.RemoveAt(index);
        }

        public IEnumerable<TValue> GetNearestInRadius(Vector2 point, float radius)
        {
            var x = point.x;
            var y = point.y;
            var origin = new Vector2(x, y);

            var items = this.matrix.RangedBinarySearchValues(x - radius, x + radius, o => o.X);
            if (!items.Any())
            {
                return new TValue[0];
            }

            var nearbyItems = items
                .SelectMany(o => o.Items.RangedBinarySearchValues(y - radius, y + radius, oo => oo.Y)
                    .Select(oo => (o.X, oo.Y, oo.Item))
                    .ToArray())
                .Where(o => Vector2.Distance(origin, new Vector2(o.X, o.Y)) < radius)
                .Select(o => o.Item)
                .ToArray();

            return nearbyItems;
        }
    }
}

