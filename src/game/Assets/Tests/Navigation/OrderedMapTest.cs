using System.Linq;
using NUnit.Framework;
using TeamZ.Assets.Code.Game.Navigation;
using UnityEngine;

namespace TeamZ.Tests.Navigation
{
    public class OrderedMatrixTest
    {
        [Test]
        public void AddOneWaypoint()
        {
            var matrix = new OrderedMatrix<Waypoint>(o => o.Position.x, o => o.Position.y);
            var waypoint = new Waypoint
            {
                Position = new Vector3(10, 10)
            };

            matrix.Add(waypoint);

            var nearest = matrix.GetNearestInRadius(waypoint.Position, 10);

            Assert.True(nearest.Count() == 1, "There is only one waypoint in searching area.");
            Assert.True(nearest.Contains(waypoint));
        }

        [Test]
        public void AddTwoNearbyWaypoints()
        {
            var matrix = new OrderedMatrix<Waypoint>(o => o.Position.x, o => o.Position.y);
            var waypoint1 = new Waypoint
            {
                Position = new Vector3(10, 10)
            };

            var waypoint2 = new Waypoint
            {
                Position = new Vector3(9, 10)
            };

            matrix.Add(waypoint1);
            matrix.Add(waypoint2);

            var nearest = matrix.GetNearestInRadius(waypoint1.Position, 5);

            Assert.True(nearest.Count() == 2, "There are only two waypoints in searching area.");
            Assert.True(nearest.Contains(waypoint1));
            Assert.True(nearest.Contains(waypoint2));
        }

        [Test]
        public void AddTwoNotNearbyWaypoints()
        {
            var matrix = new OrderedMatrix<Waypoint>(o => o.Position.x, o => o.Position.y);
            var waypoint1 = new Waypoint
            {
                Position = new Vector3(10, 10)
            };

            var waypoint2 = new Waypoint
            {
                Position = new Vector3(-9, 10)
            };

            matrix.Add(waypoint1);
            matrix.Add(waypoint2);

            var nearest = matrix.GetNearestInRadius(waypoint1.Position, 5);

            Assert.True(nearest.Count() == 1, "There are two waypoints on the field, but only one in searching area.");
            Assert.True(nearest.Contains(waypoint1));
        }

         [Test]
        public void AddTwoWaypointsSearchInWrongPlace()
        {
            var matrix = new OrderedMatrix<Waypoint>(o => o.Position.x, o => o.Position.y);
            var waypoint1 = new Waypoint
            {
                Position = new Vector3(10, 10)
            };

            var waypoint2 = new Waypoint
            {
                Position = new Vector3(-9, 10)
            };

            matrix.Add(waypoint1);
            matrix.Add(waypoint2);

            var nearest = matrix.GetNearestInRadius(Vector2.zero, 5);

            Assert.True(nearest.Count() == 0, "There are two waypoints on the field, but none in searching area.");
        }
    }
}
