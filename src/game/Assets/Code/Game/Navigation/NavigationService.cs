using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.InternalUtil;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Video;

namespace TeamZ.Assets.Code.Game.Navigation
{
    public class Waypoint
    {
        public Vector3 Position { get; set; }
        public List<Waypoint> Waypoints { get; set; }

        public BoxCollider2D Collider { get; set; }
    }

    public class NavigationService : IDisposable
    {
        private GameObject gameObject;
        private BoxCollider2D boxCollider;
        private Rigidbody2D rigibody2d;
        private NavigationEventProvider eventProvider;
        private OrderedMatrix<Waypoint> orderedMatrix;

        public Dictionary<BoxCollider2D, Waypoint[]> Planes { get; }
            = new Dictionary<BoxCollider2D, Waypoint[]>();

        public void Activate()
        {
            this.gameObject = new GameObject("~NavigationService");

            var camera = GameObject.FindObjectOfType<DirectionalCamera>();
            this.gameObject.transform.SetParent(camera.transform, false);

            this.rigibody2d = this.gameObject.AddComponent<Rigidbody2D>();
            this.rigibody2d.isKinematic = false;
            this.rigibody2d.gravityScale = 0;
            this.rigibody2d.mass = 0;

            this.boxCollider = this.gameObject.AddComponent<BoxCollider2D>();
            this.boxCollider.isTrigger = true;
            this.boxCollider.offset = Vector2.zero;
            this.boxCollider.size = new Vector2(100, 100);

            this.eventProvider = this.gameObject.AddComponent<NavigationEventProvider>();

            // Add caching
            this.eventProvider.Enter.Subscribe(o => this.AddNewEntry(o));
            this.eventProvider.Exit.Subscribe(o => this.RemoveEntry(o));

            this.orderedMatrix = new OrderedMatrix<Waypoint>(o => o.Position.x, o => o.Position.y);
        }

        private void RemoveEntry(BoxCollider2D collider)
        {
            if (!this.Planes.TryGetValue(collider, out var waypoints))
            {
                return;
            }

            foreach (var waypoint in waypoints)
            {
                this.orderedMatrix.Remove(waypoint);
                foreach (var connectedWaypoint in waypoint.Waypoints)
                {
                    connectedWaypoint.Waypoints.Remove(waypoint);
                }
            }
        }

        private void AddNewEntry(BoxCollider2D collider)
        {
            var (left, right) = this.CalculateWaypointsPositions(collider);

            var nearestToLeft = this.orderedMatrix.GetNearestInRadius(left, 5)
                .Where(o => o.Collider != collider)
                .ToList();

            var nearestToRight = this.orderedMatrix.GetNearestInRadius(right, 5)
                .Where(o => o.Collider != collider)
                .ToList();


            var leftWaypoint = new Waypoint
            {
                Position = left,
                Waypoints = nearestToLeft,
                Collider = collider
            };

            foreach (var waypoint in nearestToLeft)
            {
                waypoint.Waypoints.Add(leftWaypoint);
            }

            var rightWaypoint = new Waypoint
            {
                Position = right,
                Waypoints = nearestToRight,
                Collider = collider
            };

            foreach (var waypoint in nearestToRight)
            {
                waypoint.Waypoints.Add(rightWaypoint);
            }

            this.orderedMatrix.Add(leftWaypoint);
            this.orderedMatrix.Add(rightWaypoint);
            this.Planes.Add(collider, new[] {leftWaypoint, rightWaypoint});
        }

        private (Vector2 Left, Vector2 Right) CalculateWaypointsPositions(BoxCollider2D collider)
        {
            var bounds = collider.bounds;
            var center = bounds.center;
            var extents = bounds.extents;
            var left = new Vector2(center.x - extents.x * 0.9f, center.y + extents.y * 1.1f);
            var right = new Vector2(center.x + extents.x * 0.9f, center.y + extents.y * 1.1f);

            return (left, right);
        }

        public IEnumerable<Vector3> CalculatePath(Vector3 start, Vector3 end)
        {
            if (!this.Planes?.Any() ?? false)
            {
                yield break;
            }
            
            var startHit = Physics2D.Raycast(start, Vector3.down, 2, this.eventProvider.ImportantLayersMask);
            var endHit = Physics2D.Raycast(end, Vector3.down, 2, this.eventProvider.ImportantLayersMask);

            if (startHit.collider is BoxCollider2D startCollider && this.Planes.ContainsKey(startCollider) &&
                endHit.collider is BoxCollider2D endCollider && this.Planes.ContainsKey(endCollider))
            {
                var path = this.CalculatePathFromColliders(startCollider, endCollider);

                yield return start;
                foreach (var point in path)
                {
                    yield return point;
                }

                yield return end;
            }

            yield break;
        }

        private IEnumerable<Vector3> CalculatePathFromColliders(BoxCollider2D startCollider, BoxCollider2D endCollider)
        {
            if (startCollider == endCollider)
            {
                yield break;
            }

            if (!this.Planes.TryGetValue(startCollider, out var startWaypoints))
            {
                yield break;
            }

            if (!this.Planes.TryGetValue(endCollider, out var endWaypoints))
            {
                yield break;
            }

            this.CalculatePathFromWaypoints(startWaypoints, endWaypoints);
        }

        public Waypoint[] CalculatePathFromWaypoints(Waypoint[] startWaypoints, Waypoint[] endWaypoints)
        {
            var visitedWaypoints = new HashSet<Waypoint>();
            var paths = new LinkedList<Waypoint>[startWaypoints.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                paths[i] = new LinkedList<Waypoint>();
                paths[i].AddLast(startWaypoints[i]);
            }

            while (startWaypoints.Any())
            {
                foreach (var path in paths)
                {
                    var waypoint = path.Last();
                    visitedWaypoints.Add(waypoint);

                    if (endWaypoints.Contains(waypoint))
                    {
                        return path.ToArray();
                    }
                }

                var nextStartWaypoints = startWaypoints
                    .SelectMany((o, i) => o.Waypoints.Select(oo => (Path: paths[i], Waypoint: oo)))
                    .Where(o => !visitedWaypoints.Contains(o.Waypoint))
                    .ToArray();

                for (int i = 0; i < nextStartWaypoints.Length; i++)
                {
                    var next = nextStartWaypoints[i];
                    next.Path = new LinkedList<Waypoint>(next.Path);
                    next.Path.AddLast(next.Waypoint);
                    
                    nextStartWaypoints[i] = next;
                }

                startWaypoints = nextStartWaypoints.Select(o => o.Waypoint).ToArray();
                paths = nextStartWaypoints.Select(o => o.Path).ToArray();
            }

            return new Waypoint[0];
        }

        public void Dispose()
        {
            GameObject.Destroy(this.gameObject);
            this.Planes.Clear();
            this.orderedMatrix?.Clear();
        }
    }
}