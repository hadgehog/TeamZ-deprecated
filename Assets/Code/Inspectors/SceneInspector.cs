using System;
using UniRx;
using UnityEditor;

namespace Inspectors
{
#if UNITY_EDITOR
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
#endif
}
