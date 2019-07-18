using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TeamZ.Assets.Code.Game.Navigation;

namespace TeamZ.Tests.Navigation
{
    public class NavigationServiceAlgoTest
    {
        [Test]
        public void DirectPath()
        {
            var navigationService = new NavigationService();

            var waypoint1 = new Waypoint();
            var waypoint2 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint1}};
            var waypoint3 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint2}};
            var waypoint4 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint3}};
            var expectedPath = new[] {waypoint4, waypoint3, waypoint2, waypoint1};

            var result = navigationService.CalculatePathFromWaypoints(new[] {waypoint4}, new[] {waypoint1});

            Assert.True(expectedPath.SequenceEqual(result));
        }

        [Test]
        public void MultiplePathsSelectsFirstOne()
        {
            var navigationService = new NavigationService();

            var waypoint1 = new Waypoint();
            var waypoint2 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint1}};
            var waypoint3 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint2}};
            var waypoint4 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint2}};
            var waypoint5 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint3, waypoint4}};

            var expectedPath = new[] {waypoint5, waypoint3, waypoint2, waypoint1};

            var result = navigationService.CalculatePathFromWaypoints(new[] {waypoint5}, new[] {waypoint1});

            Assert.True(expectedPath.SequenceEqual(result));
        }
        
        [Test]
        public void MultiplePathsSelectsShortestOne()
        {
            var navigationService = new NavigationService();

            var waypoint1 = new Waypoint();
            var waypoint2 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint1}};
            var waypoint3 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint1}};
            var waypoint4 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint2}};
            var waypoint5 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint3, waypoint4}};

            var expectedPath = new[] {waypoint5, waypoint3, waypoint1};

            var result = navigationService.CalculatePathFromWaypoints(new[] {waypoint5}, new[] {waypoint1});

            Assert.True(expectedPath.SequenceEqual(result));
        }

        [Test]
        public void NoPath()
        {
            var navigationService = new NavigationService();

            var waypoint1 = new Waypoint();
            var waypoint2 = new Waypoint() {Waypoints = new List<Waypoint>() { }};
            var waypoint3 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint2}};
            var waypoint4 = new Waypoint() {Waypoints = new List<Waypoint>() {waypoint3}};
            var expectedPath = new Waypoint[0];

            var result = navigationService.CalculatePathFromWaypoints(new[] {waypoint4}, new[] {waypoint1});

            Assert.True(expectedPath.SequenceEqual(result), "There is no way from waypoint4 to waypoint1.");
        }
    }
}