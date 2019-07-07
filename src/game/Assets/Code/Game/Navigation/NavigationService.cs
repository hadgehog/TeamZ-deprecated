using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UniRx;
using UnityEngine;

namespace TeamZ.Assets.Code.Game.Navigation
{

    public class NavigationService : IDisposable
    {
        private GameObject gameObject;
        private BoxCollider2D boxCollider;
        private Rigidbody2D rigibody2d;
        private NavigationEventProvider eventProvider;

        public Dictionary<BoxCollider2D, Vector3[]> Planes { get; }
            = new Dictionary<BoxCollider2D, Vector3[]>();

        public void Activate()
        {
            this.gameObject = new GameObject("~NavigationService");

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
            this.eventProvider.Enter.Subscribe(o => this.Planes.Add(o, new Vector3[0]));
            this.eventProvider.Exit.Subscribe(o => this.Planes.Remove(o));
        }

        public IEnumerable<Vector3> CalculatePath(Vector3 start, Vector3 end)
        {
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
        }

        public void Dispose()
        {
            GameObject.Destroy(this.gameObject);
            this.Planes.Clear();
        }
    }
}

