using UniRx;
using UnityEngine;

namespace TeamZ.Assets.Code.Game.Navigation
{
    public class NavigationEventProvider : MonoBehaviour
    {
        public int ImportantLayers { get; private set; }
        public int ImportantLayersMask { get; private set; }

        public Subject<BoxCollider2D> Enter { get; set; } = new Subject<BoxCollider2D>();
        public Subject<BoxCollider2D> Exit { get; set; } = new Subject<BoxCollider2D>();

        private void Start()
        {
            this.ImportantLayers = LayerMask.NameToLayer("Ground");
            this.ImportantLayersMask = LayerMask.GetMask("Ground");
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.layer == this.ImportantLayers &&
                collider is BoxCollider2D boxCollider)
            {
                this.Enter.OnNext(boxCollider);
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.layer == this.ImportantLayers &&
                collider is BoxCollider2D boxCollider)
            {
                this.Exit.OnNext(boxCollider);
            }
        }

        private void OnDestroy()
        {
            this.Enter.OnCompleted();
            this.Exit.OnCompleted();
        }
    }
}

