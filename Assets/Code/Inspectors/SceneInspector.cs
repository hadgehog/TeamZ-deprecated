using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEditor;

namespace Inspectors
{
    [Serializable]
    public class SceneReactiveProperty : ReactiveProperty<SceneAsset>
    {
        public SceneReactiveProperty()
        {
        }

        public SceneReactiveProperty(SceneAsset initialValue)
            : base(initialValue)
        {
        }
    }

    [CustomPropertyDrawer(typeof(SceneReactiveProperty))]
    public class ExtendInspectorDisplayDrawer : InspectorDisplayDrawer
    {
    }
}
