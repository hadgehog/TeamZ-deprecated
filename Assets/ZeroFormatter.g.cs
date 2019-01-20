#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;
    using global::ZeroFormatter.Comparers;

    public static partial class ZeroFormatterInitializer
    {
        static bool registered = false;

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            if(registered) return;
            registered = true;
            // Enums
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::CharacterControllerScript.Direction>.Register(new ZeroFormatter.DynamicObjectSegments.CharacterControllerScript_DirectionFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Comparers.ZeroFormatterEqualityComparer<global::CharacterControllerScript.Direction>.Register(new ZeroFormatter.DynamicObjectSegments.CharacterControllerScript_DirectionEqualityComparer());
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::CharacterControllerScript.Direction?>.Register(new ZeroFormatter.DynamicObjectSegments.NullableCharacterControllerScript_DirectionFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Comparers.ZeroFormatterEqualityComparer<global::CharacterControllerScript.Direction?>.Register(new NullableEqualityComparer<global::CharacterControllerScript.Direction>());
            
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::CharacterControllerScript.FightMode>.Register(new ZeroFormatter.DynamicObjectSegments.CharacterControllerScript_FightModeFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Comparers.ZeroFormatterEqualityComparer<global::CharacterControllerScript.FightMode>.Register(new ZeroFormatter.DynamicObjectSegments.CharacterControllerScript_FightModeEqualityComparer());
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::CharacterControllerScript.FightMode?>.Register(new ZeroFormatter.DynamicObjectSegments.NullableCharacterControllerScript_FightModeFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Comparers.ZeroFormatterEqualityComparer<global::CharacterControllerScript.FightMode?>.Register(new NullableEqualityComparer<global::CharacterControllerScript.FightMode>());
            
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::DoorScript.DoorState>.Register(new ZeroFormatter.DynamicObjectSegments.DoorScript_DoorStateFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Comparers.ZeroFormatterEqualityComparer<global::DoorScript.DoorState>.Register(new ZeroFormatter.DynamicObjectSegments.DoorScript_DoorStateEqualityComparer());
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::DoorScript.DoorState?>.Register(new ZeroFormatter.DynamicObjectSegments.NullableDoorScript_DoorStateFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Comparers.ZeroFormatterEqualityComparer<global::DoorScript.DoorState?>.Register(new NullableEqualityComparer<global::DoorScript.DoorState>());
            
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::GameSaving.States.MonoBehaviourStateKind>.Register(new ZeroFormatter.DynamicObjectSegments.GameSaving.States.MonoBehaviourStateKindFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Comparers.ZeroFormatterEqualityComparer<global::GameSaving.States.MonoBehaviourStateKind>.Register(new ZeroFormatter.DynamicObjectSegments.GameSaving.States.MonoBehaviourStateKindEqualityComparer());
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::GameSaving.States.MonoBehaviourStateKind?>.Register(new ZeroFormatter.DynamicObjectSegments.GameSaving.States.NullableMonoBehaviourStateKindFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Comparers.ZeroFormatterEqualityComparer<global::GameSaving.States.MonoBehaviourStateKind?>.Register(new NullableEqualityComparer<global::GameSaving.States.MonoBehaviourStateKind>());
            
            // Objects
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::GameSaving.States.CameraState>.Register(new ZeroFormatter.DynamicObjectSegments.GameSaving.States.CameraStateFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::GameSaving.States.EntityState>.Register(new ZeroFormatter.DynamicObjectSegments.GameSaving.States.EntityStateFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::GameSaving.States.GameObjectState>.Register(new ZeroFormatter.DynamicObjectSegments.GameSaving.States.GameObjectStateFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::GameSaving.States.GameState>.Register(new ZeroFormatter.DynamicObjectSegments.GameSaving.States.GameStateFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::TeamZ.Assets.GameSaving.States.CharacterControllerState>.Register(new ZeroFormatter.DynamicObjectSegments.TeamZ.Assets.GameSaving.States.CharacterControllerStateFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::GameSaving.States.Charaters.LizardState>.Register(new ZeroFormatter.DynamicObjectSegments.GameSaving.States.Charaters.LizardStateFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            // Structs
            {
                var structFormatter = new ZeroFormatter.DynamicObjectSegments.UnityEngine.Vector3Formatter<ZeroFormatter.Formatters.DefaultResolver>();
                ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::UnityEngine.Vector3>.Register(structFormatter);
                ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::UnityEngine.Vector3?>.Register(new global::ZeroFormatter.Formatters.NullableStructFormatter<ZeroFormatter.Formatters.DefaultResolver, global::UnityEngine.Vector3>(structFormatter));
            }
            {
                var structFormatter = new ZeroFormatter.DynamicObjectSegments.UnityEngine.QuaternionFormatter<ZeroFormatter.Formatters.DefaultResolver>();
                ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::UnityEngine.Quaternion>.Register(structFormatter);
                ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::UnityEngine.Quaternion?>.Register(new global::ZeroFormatter.Formatters.NullableStructFormatter<ZeroFormatter.Formatters.DefaultResolver, global::UnityEngine.Quaternion>(structFormatter));
            }
            {
                var structFormatter = new ZeroFormatter.DynamicObjectSegments.UnityEngine.Vector2Formatter<ZeroFormatter.Formatters.DefaultResolver>();
                ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::UnityEngine.Vector2>.Register(structFormatter);
                ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::UnityEngine.Vector2?>.Register(new global::ZeroFormatter.Formatters.NullableStructFormatter<ZeroFormatter.Formatters.DefaultResolver, global::UnityEngine.Vector2>(structFormatter));
            }
            // Unions
            {
                var unionFormatter = new ZeroFormatter.DynamicObjectSegments.GameSaving.States.MonoBehaviourStateFormatter<ZeroFormatter.Formatters.DefaultResolver>();
                ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::GameSaving.States.MonoBehaviourState>.Register(unionFormatter);
            }
            // Generics
            ZeroFormatter.Formatters.Formatter.RegisterCollection<ZeroFormatter.Formatters.DefaultResolver, global::System.Guid, global::System.Collections.Generic.HashSet<global::System.Guid>>();
            ZeroFormatter.Formatters.Formatter.RegisterEnumerable<ZeroFormatter.Formatters.DefaultResolver, global::GameSaving.States.GameObjectState>();
            ZeroFormatter.Formatters.Formatter.RegisterEnumerable<ZeroFormatter.Formatters.DefaultResolver, global::GameSaving.States.MonoBehaviourState>();
        }
    }
}
#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.DynamicObjectSegments.GameSaving.States
{
    using global::System;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;

    public class CameraStateFormatter<TTypeResolver> : Formatter<TTypeResolver, global::GameSaving.States.CameraState>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::GameSaving.States.CameraState value)
        {
            var segment = value as IZeroFormatterSegment;
            if (segment != null)
            {
                return segment.Serialize(ref bytes, offset);
            }
            else if (value == null)
            {
                BinaryUtil.WriteInt32(ref bytes, offset, -1);
                return 4;
            }
            else
            {
                var startOffset = offset;

                offset += (8 + 4 * (1 + 1));
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::System.Guid>(ref bytes, startOffset, offset, 0, value.PlayerId);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::UnityEngine.Vector3>(ref bytes, startOffset, offset, 1, value.Position);

                return ObjectSegmentHelper.WriteSize(ref bytes, startOffset, offset, 1);
            }
        }

        public override global::GameSaving.States.CameraState Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = BinaryUtil.ReadInt32(ref bytes, offset);
            if (byteSize == -1)
            {
                byteSize = 4;
                return null;
            }
            return new CameraStateObjectSegment<TTypeResolver>(tracker, new ArraySegment<byte>(bytes, offset, byteSize));
        }
    }

    public class CameraStateObjectSegment<TTypeResolver> : global::GameSaving.States.CameraState, IZeroFormatterSegment
        where TTypeResolver : ITypeResolver, new()
    {
        static readonly int[] __elementSizes = new int[]{ 16, 0 };

        readonly ArraySegment<byte> __originalBytes;
        readonly global::ZeroFormatter.DirtyTracker __tracker;
        readonly int __binaryLastIndex;
        readonly byte[] __extraFixedBytes;

        CacheSegment<TTypeResolver, global::UnityEngine.Vector3> _Position;

        // 0
        public override global::System.Guid PlayerId
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<TTypeResolver, global::System.Guid>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<TTypeResolver, global::System.Guid>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 1
        public override global::UnityEngine.Vector3 Position
        {
            get
            {
                return _Position.Value;
            }
            set
            {
                _Position.Value = value;
            }
        }


        public CameraStateObjectSegment(global::ZeroFormatter.DirtyTracker dirtyTracker, ArraySegment<byte> originalBytes)
        {
            var __array = originalBytes.Array;

            this.__originalBytes = originalBytes;
            this.__tracker = dirtyTracker = dirtyTracker.CreateChild();
            this.__binaryLastIndex = BinaryUtil.ReadInt32(ref __array, originalBytes.Offset + 4);

            this.__extraFixedBytes = ObjectSegmentHelper.CreateExtraFixedBytes(this.__binaryLastIndex, 1, __elementSizes);

            _Position = new CacheSegment<TTypeResolver, global::UnityEngine.Vector3>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 1, __binaryLastIndex, __tracker));
        }

        public bool CanDirectCopy()
        {
            return !__tracker.IsDirty;
        }

        public ArraySegment<byte> GetBufferReference()
        {
            return __originalBytes;
        }

        public int Serialize(ref byte[] targetBytes, int offset)
        {
            if (__extraFixedBytes != null || __tracker.IsDirty)
            {
                var startOffset = offset;
                offset += (8 + 4 * (1 + 1));

                offset += ObjectSegmentHelper.SerializeFixedLength<TTypeResolver, global::System.Guid>(ref targetBytes, startOffset, offset, 0, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeCacheSegment<TTypeResolver, global::UnityEngine.Vector3>(ref targetBytes, startOffset, offset, 1, ref _Position);

                return ObjectSegmentHelper.WriteSize(ref targetBytes, startOffset, offset, 1);
            }
            else
            {
                return ObjectSegmentHelper.DirectCopyAll(__originalBytes, ref targetBytes, offset);
            }
        }
    }

    public class EntityStateFormatter<TTypeResolver> : Formatter<TTypeResolver, global::GameSaving.States.EntityState>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::GameSaving.States.EntityState value)
        {
            var segment = value as IZeroFormatterSegment;
            if (segment != null)
            {
                return segment.Serialize(ref bytes, offset);
            }
            else if (value == null)
            {
                BinaryUtil.WriteInt32(ref bytes, offset, -1);
                return 4;
            }
            else
            {
                var startOffset = offset;

                offset += (8 + 4 * (5 + 1));
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::System.Guid>(ref bytes, startOffset, offset, 0, value.Id);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, string>(ref bytes, startOffset, offset, 1, value.AssetGuid);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::UnityEngine.Vector3>(ref bytes, startOffset, offset, 2, value.Scale);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::UnityEngine.Quaternion>(ref bytes, startOffset, offset, 3, value.Rotation);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::UnityEngine.Vector3>(ref bytes, startOffset, offset, 4, value.Position);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::System.Guid>(ref bytes, startOffset, offset, 5, value.LevelId);

                return ObjectSegmentHelper.WriteSize(ref bytes, startOffset, offset, 5);
            }
        }

        public override global::GameSaving.States.EntityState Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = BinaryUtil.ReadInt32(ref bytes, offset);
            if (byteSize == -1)
            {
                byteSize = 4;
                return null;
            }
            return new EntityStateObjectSegment<TTypeResolver>(tracker, new ArraySegment<byte>(bytes, offset, byteSize));
        }
    }

    public class EntityStateObjectSegment<TTypeResolver> : global::GameSaving.States.EntityState, IZeroFormatterSegment
        where TTypeResolver : ITypeResolver, new()
    {
        static readonly int[] __elementSizes = new int[]{ 16, 0, 0, 0, 0, 16 };

        readonly ArraySegment<byte> __originalBytes;
        readonly global::ZeroFormatter.DirtyTracker __tracker;
        readonly int __binaryLastIndex;
        readonly byte[] __extraFixedBytes;

        CacheSegment<TTypeResolver, string> _AssetGuid;
        CacheSegment<TTypeResolver, global::UnityEngine.Vector3> _Scale;
        CacheSegment<TTypeResolver, global::UnityEngine.Quaternion> _Rotation;
        CacheSegment<TTypeResolver, global::UnityEngine.Vector3> _Position;

        // 0
        public override global::System.Guid Id
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<TTypeResolver, global::System.Guid>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<TTypeResolver, global::System.Guid>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 1
        public override string AssetGuid
        {
            get
            {
                return _AssetGuid.Value;
            }
            set
            {
                _AssetGuid.Value = value;
            }
        }

        // 2
        public override global::UnityEngine.Vector3 Scale
        {
            get
            {
                return _Scale.Value;
            }
            set
            {
                _Scale.Value = value;
            }
        }

        // 3
        public override global::UnityEngine.Quaternion Rotation
        {
            get
            {
                return _Rotation.Value;
            }
            set
            {
                _Rotation.Value = value;
            }
        }

        // 4
        public override global::UnityEngine.Vector3 Position
        {
            get
            {
                return _Position.Value;
            }
            set
            {
                _Position.Value = value;
            }
        }

        // 5
        public override global::System.Guid LevelId
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<TTypeResolver, global::System.Guid>(__originalBytes, 5, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<TTypeResolver, global::System.Guid>(__originalBytes, 5, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }


        public EntityStateObjectSegment(global::ZeroFormatter.DirtyTracker dirtyTracker, ArraySegment<byte> originalBytes)
        {
            var __array = originalBytes.Array;

            this.__originalBytes = originalBytes;
            this.__tracker = dirtyTracker = dirtyTracker.CreateChild();
            this.__binaryLastIndex = BinaryUtil.ReadInt32(ref __array, originalBytes.Offset + 4);

            this.__extraFixedBytes = ObjectSegmentHelper.CreateExtraFixedBytes(this.__binaryLastIndex, 5, __elementSizes);

            _AssetGuid = new CacheSegment<TTypeResolver, string>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 1, __binaryLastIndex, __tracker));
            _Scale = new CacheSegment<TTypeResolver, global::UnityEngine.Vector3>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 2, __binaryLastIndex, __tracker));
            _Rotation = new CacheSegment<TTypeResolver, global::UnityEngine.Quaternion>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 3, __binaryLastIndex, __tracker));
            _Position = new CacheSegment<TTypeResolver, global::UnityEngine.Vector3>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 4, __binaryLastIndex, __tracker));
        }

        public bool CanDirectCopy()
        {
            return !__tracker.IsDirty;
        }

        public ArraySegment<byte> GetBufferReference()
        {
            return __originalBytes;
        }

        public int Serialize(ref byte[] targetBytes, int offset)
        {
            if (__extraFixedBytes != null || __tracker.IsDirty)
            {
                var startOffset = offset;
                offset += (8 + 4 * (5 + 1));

                offset += ObjectSegmentHelper.SerializeFixedLength<TTypeResolver, global::System.Guid>(ref targetBytes, startOffset, offset, 0, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeCacheSegment<TTypeResolver, string>(ref targetBytes, startOffset, offset, 1, ref _AssetGuid);
                offset += ObjectSegmentHelper.SerializeCacheSegment<TTypeResolver, global::UnityEngine.Vector3>(ref targetBytes, startOffset, offset, 2, ref _Scale);
                offset += ObjectSegmentHelper.SerializeCacheSegment<TTypeResolver, global::UnityEngine.Quaternion>(ref targetBytes, startOffset, offset, 3, ref _Rotation);
                offset += ObjectSegmentHelper.SerializeCacheSegment<TTypeResolver, global::UnityEngine.Vector3>(ref targetBytes, startOffset, offset, 4, ref _Position);
                offset += ObjectSegmentHelper.SerializeFixedLength<TTypeResolver, global::System.Guid>(ref targetBytes, startOffset, offset, 5, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);

                return ObjectSegmentHelper.WriteSize(ref targetBytes, startOffset, offset, 5);
            }
            else
            {
                return ObjectSegmentHelper.DirectCopyAll(__originalBytes, ref targetBytes, offset);
            }
        }
    }

    public class GameObjectStateFormatter<TTypeResolver> : Formatter<TTypeResolver, global::GameSaving.States.GameObjectState>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::GameSaving.States.GameObjectState value)
        {
            var segment = value as IZeroFormatterSegment;
            if (segment != null)
            {
                return segment.Serialize(ref bytes, offset);
            }
            else if (value == null)
            {
                BinaryUtil.WriteInt32(ref bytes, offset, -1);
                return 4;
            }
            else
            {
                var startOffset = offset;

                offset += (8 + 4 * (1 + 1));
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::GameSaving.States.EntityState>(ref bytes, startOffset, offset, 0, value.Entity);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::System.Collections.Generic.IEnumerable<global::GameSaving.States.MonoBehaviourState>>(ref bytes, startOffset, offset, 1, value.MonoBehaviousStates);

                return ObjectSegmentHelper.WriteSize(ref bytes, startOffset, offset, 1);
            }
        }

        public override global::GameSaving.States.GameObjectState Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = BinaryUtil.ReadInt32(ref bytes, offset);
            if (byteSize == -1)
            {
                byteSize = 4;
                return null;
            }
            return new GameObjectStateObjectSegment<TTypeResolver>(tracker, new ArraySegment<byte>(bytes, offset, byteSize));
        }
    }

    public class GameObjectStateObjectSegment<TTypeResolver> : global::GameSaving.States.GameObjectState, IZeroFormatterSegment
        where TTypeResolver : ITypeResolver, new()
    {
        static readonly int[] __elementSizes = new int[]{ 0, 0 };

        readonly ArraySegment<byte> __originalBytes;
        readonly global::ZeroFormatter.DirtyTracker __tracker;
        readonly int __binaryLastIndex;
        readonly byte[] __extraFixedBytes;

        global::GameSaving.States.EntityState _Entity;
        CacheSegment<TTypeResolver, global::System.Collections.Generic.IEnumerable<global::GameSaving.States.MonoBehaviourState>> _MonoBehaviousStates;

        // 0
        public override global::GameSaving.States.EntityState Entity
        {
            get
            {
                return _Entity;
            }
            set
            {
                __tracker.Dirty();
                _Entity = value;
            }
        }

        // 1
        public override global::System.Collections.Generic.IEnumerable<global::GameSaving.States.MonoBehaviourState> MonoBehaviousStates
        {
            get
            {
                return _MonoBehaviousStates.Value;
            }
            set
            {
                _MonoBehaviousStates.Value = value;
            }
        }


        public GameObjectStateObjectSegment(global::ZeroFormatter.DirtyTracker dirtyTracker, ArraySegment<byte> originalBytes)
        {
            var __array = originalBytes.Array;

            this.__originalBytes = originalBytes;
            this.__tracker = dirtyTracker = dirtyTracker.CreateChild();
            this.__binaryLastIndex = BinaryUtil.ReadInt32(ref __array, originalBytes.Offset + 4);

            this.__extraFixedBytes = ObjectSegmentHelper.CreateExtraFixedBytes(this.__binaryLastIndex, 1, __elementSizes);

            _Entity = ObjectSegmentHelper.DeserializeSegment<TTypeResolver, global::GameSaving.States.EntityState>(originalBytes, 0, __binaryLastIndex, __tracker);
            _MonoBehaviousStates = new CacheSegment<TTypeResolver, global::System.Collections.Generic.IEnumerable<global::GameSaving.States.MonoBehaviourState>>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 1, __binaryLastIndex, __tracker));
        }

        public bool CanDirectCopy()
        {
            return !__tracker.IsDirty;
        }

        public ArraySegment<byte> GetBufferReference()
        {
            return __originalBytes;
        }

        public int Serialize(ref byte[] targetBytes, int offset)
        {
            if (__extraFixedBytes != null || __tracker.IsDirty)
            {
                var startOffset = offset;
                offset += (8 + 4 * (1 + 1));

                offset += ObjectSegmentHelper.SerializeSegment<TTypeResolver, global::GameSaving.States.EntityState>(ref targetBytes, startOffset, offset, 0, _Entity);
                offset += ObjectSegmentHelper.SerializeCacheSegment<TTypeResolver, global::System.Collections.Generic.IEnumerable<global::GameSaving.States.MonoBehaviourState>>(ref targetBytes, startOffset, offset, 1, ref _MonoBehaviousStates);

                return ObjectSegmentHelper.WriteSize(ref targetBytes, startOffset, offset, 1);
            }
            else
            {
                return ObjectSegmentHelper.DirectCopyAll(__originalBytes, ref targetBytes, offset);
            }
        }
    }

    public class GameStateFormatter<TTypeResolver> : Formatter<TTypeResolver, global::GameSaving.States.GameState>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::GameSaving.States.GameState value)
        {
            var segment = value as IZeroFormatterSegment;
            if (segment != null)
            {
                return segment.Serialize(ref bytes, offset);
            }
            else if (value == null)
            {
                BinaryUtil.WriteInt32(ref bytes, offset, -1);
                return 4;
            }
            else
            {
                var startOffset = offset;

                offset += (8 + 4 * (2 + 1));
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::System.Guid>(ref bytes, startOffset, offset, 0, value.LevelId);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::System.Collections.Generic.IEnumerable<global::GameSaving.States.GameObjectState>>(ref bytes, startOffset, offset, 1, value.GameObjectsStates);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::System.Collections.Generic.HashSet<global::System.Guid>>(ref bytes, startOffset, offset, 2, value.VisitedLevels);

                return ObjectSegmentHelper.WriteSize(ref bytes, startOffset, offset, 2);
            }
        }

        public override global::GameSaving.States.GameState Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = BinaryUtil.ReadInt32(ref bytes, offset);
            if (byteSize == -1)
            {
                byteSize = 4;
                return null;
            }
            return new GameStateObjectSegment<TTypeResolver>(tracker, new ArraySegment<byte>(bytes, offset, byteSize));
        }
    }

    public class GameStateObjectSegment<TTypeResolver> : global::GameSaving.States.GameState, IZeroFormatterSegment
        where TTypeResolver : ITypeResolver, new()
    {
        static readonly int[] __elementSizes = new int[]{ 16, 0, 0 };

        readonly ArraySegment<byte> __originalBytes;
        readonly global::ZeroFormatter.DirtyTracker __tracker;
        readonly int __binaryLastIndex;
        readonly byte[] __extraFixedBytes;

        CacheSegment<TTypeResolver, global::System.Collections.Generic.IEnumerable<global::GameSaving.States.GameObjectState>> _GameObjectsStates;
        CacheSegment<TTypeResolver, global::System.Collections.Generic.HashSet<global::System.Guid>> _VisitedLevels;

        // 0
        public override global::System.Guid LevelId
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<TTypeResolver, global::System.Guid>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<TTypeResolver, global::System.Guid>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 1
        public override global::System.Collections.Generic.IEnumerable<global::GameSaving.States.GameObjectState> GameObjectsStates
        {
            get
            {
                return _GameObjectsStates.Value;
            }
            set
            {
                _GameObjectsStates.Value = value;
            }
        }

        // 2
        public override global::System.Collections.Generic.HashSet<global::System.Guid> VisitedLevels
        {
            get
            {
                return _VisitedLevels.Value;
            }
            set
            {
                _VisitedLevels.Value = value;
            }
        }


        public GameStateObjectSegment(global::ZeroFormatter.DirtyTracker dirtyTracker, ArraySegment<byte> originalBytes)
        {
            var __array = originalBytes.Array;

            this.__originalBytes = originalBytes;
            this.__tracker = dirtyTracker = dirtyTracker.CreateChild();
            this.__binaryLastIndex = BinaryUtil.ReadInt32(ref __array, originalBytes.Offset + 4);

            this.__extraFixedBytes = ObjectSegmentHelper.CreateExtraFixedBytes(this.__binaryLastIndex, 2, __elementSizes);

            _GameObjectsStates = new CacheSegment<TTypeResolver, global::System.Collections.Generic.IEnumerable<global::GameSaving.States.GameObjectState>>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 1, __binaryLastIndex, __tracker));
            _VisitedLevels = new CacheSegment<TTypeResolver, global::System.Collections.Generic.HashSet<global::System.Guid>>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 2, __binaryLastIndex, __tracker));
        }

        public bool CanDirectCopy()
        {
            return !__tracker.IsDirty;
        }

        public ArraySegment<byte> GetBufferReference()
        {
            return __originalBytes;
        }

        public int Serialize(ref byte[] targetBytes, int offset)
        {
            if (__extraFixedBytes != null || __tracker.IsDirty)
            {
                var startOffset = offset;
                offset += (8 + 4 * (2 + 1));

                offset += ObjectSegmentHelper.SerializeFixedLength<TTypeResolver, global::System.Guid>(ref targetBytes, startOffset, offset, 0, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeCacheSegment<TTypeResolver, global::System.Collections.Generic.IEnumerable<global::GameSaving.States.GameObjectState>>(ref targetBytes, startOffset, offset, 1, ref _GameObjectsStates);
                offset += ObjectSegmentHelper.SerializeCacheSegment<TTypeResolver, global::System.Collections.Generic.HashSet<global::System.Guid>>(ref targetBytes, startOffset, offset, 2, ref _VisitedLevels);

                return ObjectSegmentHelper.WriteSize(ref targetBytes, startOffset, offset, 2);
            }
            else
            {
                return ObjectSegmentHelper.DirectCopyAll(__originalBytes, ref targetBytes, offset);
            }
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.DynamicObjectSegments.TeamZ.Assets.GameSaving.States
{
    using global::System;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;

    public class CharacterControllerStateFormatter<TTypeResolver> : Formatter<TTypeResolver, global::TeamZ.Assets.GameSaving.States.CharacterControllerState>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::TeamZ.Assets.GameSaving.States.CharacterControllerState value)
        {
            var segment = value as IZeroFormatterSegment;
            if (segment != null)
            {
                return segment.Serialize(ref bytes, offset);
            }
            else if (value == null)
            {
                BinaryUtil.WriteInt32(ref bytes, offset, -1);
                return 4;
            }
            else
            {
                var startOffset = offset;

                offset += (8 + 4 * (0 + 1));
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::CharacterControllerScript.Direction>(ref bytes, startOffset, offset, 0, value.CurrentDirection);

                return ObjectSegmentHelper.WriteSize(ref bytes, startOffset, offset, 0);
            }
        }

        public override global::TeamZ.Assets.GameSaving.States.CharacterControllerState Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = BinaryUtil.ReadInt32(ref bytes, offset);
            if (byteSize == -1)
            {
                byteSize = 4;
                return null;
            }
            return new CharacterControllerStateObjectSegment<TTypeResolver>(tracker, new ArraySegment<byte>(bytes, offset, byteSize));
        }
    }

    public class CharacterControllerStateObjectSegment<TTypeResolver> : global::TeamZ.Assets.GameSaving.States.CharacterControllerState, IZeroFormatterSegment
        where TTypeResolver : ITypeResolver, new()
    {
        static readonly int[] __elementSizes = new int[]{ 4 };

        readonly ArraySegment<byte> __originalBytes;
        readonly global::ZeroFormatter.DirtyTracker __tracker;
        readonly int __binaryLastIndex;
        readonly byte[] __extraFixedBytes;


        // 0
        public override global::CharacterControllerScript.Direction CurrentDirection
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<TTypeResolver, global::CharacterControllerScript.Direction>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<TTypeResolver, global::CharacterControllerScript.Direction>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }


        public CharacterControllerStateObjectSegment(global::ZeroFormatter.DirtyTracker dirtyTracker, ArraySegment<byte> originalBytes)
        {
            var __array = originalBytes.Array;

            this.__originalBytes = originalBytes;
            this.__tracker = dirtyTracker = dirtyTracker.CreateChild();
            this.__binaryLastIndex = BinaryUtil.ReadInt32(ref __array, originalBytes.Offset + 4);

            this.__extraFixedBytes = ObjectSegmentHelper.CreateExtraFixedBytes(this.__binaryLastIndex, 0, __elementSizes);

        }

        public bool CanDirectCopy()
        {
            return !__tracker.IsDirty;
        }

        public ArraySegment<byte> GetBufferReference()
        {
            return __originalBytes;
        }

        public int Serialize(ref byte[] targetBytes, int offset)
        {
            if (__extraFixedBytes != null || __tracker.IsDirty)
            {
                var startOffset = offset;
                offset += (8 + 4 * (0 + 1));

                offset += ObjectSegmentHelper.SerializeFixedLength<TTypeResolver, global::CharacterControllerScript.Direction>(ref targetBytes, startOffset, offset, 0, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);

                return ObjectSegmentHelper.WriteSize(ref targetBytes, startOffset, offset, 0);
            }
            else
            {
                return ObjectSegmentHelper.DirectCopyAll(__originalBytes, ref targetBytes, offset);
            }
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.DynamicObjectSegments.GameSaving.States.Charaters
{
    using global::System;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;

    public class LizardStateFormatter<TTypeResolver> : Formatter<TTypeResolver, global::GameSaving.States.Charaters.LizardState>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::GameSaving.States.Charaters.LizardState value)
        {
            var segment = value as IZeroFormatterSegment;
            if (segment != null)
            {
                return segment.Serialize(ref bytes, offset);
            }
            else if (value == null)
            {
                BinaryUtil.WriteInt32(ref bytes, offset, -1);
                return 4;
            }
            else
            {
                var startOffset = offset;

                offset += (8 + 4 * (3 + 1));
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, int>(ref bytes, startOffset, offset, 0, value.Health);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, int>(ref bytes, startOffset, offset, 1, value.Armor);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, int>(ref bytes, startOffset, offset, 2, value.PunchDamage);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, int>(ref bytes, startOffset, offset, 3, value.KickDamage);

                return ObjectSegmentHelper.WriteSize(ref bytes, startOffset, offset, 3);
            }
        }

        public override global::GameSaving.States.Charaters.LizardState Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = BinaryUtil.ReadInt32(ref bytes, offset);
            if (byteSize == -1)
            {
                byteSize = 4;
                return null;
            }
            return new LizardStateObjectSegment<TTypeResolver>(tracker, new ArraySegment<byte>(bytes, offset, byteSize));
        }
    }

    public class LizardStateObjectSegment<TTypeResolver> : global::GameSaving.States.Charaters.LizardState, IZeroFormatterSegment
        where TTypeResolver : ITypeResolver, new()
    {
        static readonly int[] __elementSizes = new int[]{ 4, 4, 4, 4 };

        readonly ArraySegment<byte> __originalBytes;
        readonly global::ZeroFormatter.DirtyTracker __tracker;
        readonly int __binaryLastIndex;
        readonly byte[] __extraFixedBytes;


        // 0
        public override int Health
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<TTypeResolver, int>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<TTypeResolver, int>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 1
        public override int Armor
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<TTypeResolver, int>(__originalBytes, 1, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<TTypeResolver, int>(__originalBytes, 1, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 2
        public override int PunchDamage
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<TTypeResolver, int>(__originalBytes, 2, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<TTypeResolver, int>(__originalBytes, 2, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 3
        public override int KickDamage
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<TTypeResolver, int>(__originalBytes, 3, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<TTypeResolver, int>(__originalBytes, 3, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }


        public LizardStateObjectSegment(global::ZeroFormatter.DirtyTracker dirtyTracker, ArraySegment<byte> originalBytes)
        {
            var __array = originalBytes.Array;

            this.__originalBytes = originalBytes;
            this.__tracker = dirtyTracker = dirtyTracker.CreateChild();
            this.__binaryLastIndex = BinaryUtil.ReadInt32(ref __array, originalBytes.Offset + 4);

            this.__extraFixedBytes = ObjectSegmentHelper.CreateExtraFixedBytes(this.__binaryLastIndex, 3, __elementSizes);

        }

        public bool CanDirectCopy()
        {
            return !__tracker.IsDirty;
        }

        public ArraySegment<byte> GetBufferReference()
        {
            return __originalBytes;
        }

        public int Serialize(ref byte[] targetBytes, int offset)
        {
            if (__extraFixedBytes != null || __tracker.IsDirty)
            {
                var startOffset = offset;
                offset += (8 + 4 * (3 + 1));

                offset += ObjectSegmentHelper.SerializeFixedLength<TTypeResolver, int>(ref targetBytes, startOffset, offset, 0, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeFixedLength<TTypeResolver, int>(ref targetBytes, startOffset, offset, 1, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeFixedLength<TTypeResolver, int>(ref targetBytes, startOffset, offset, 2, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeFixedLength<TTypeResolver, int>(ref targetBytes, startOffset, offset, 3, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);

                return ObjectSegmentHelper.WriteSize(ref targetBytes, startOffset, offset, 3);
            }
            else
            {
                return ObjectSegmentHelper.DirectCopyAll(__originalBytes, ref targetBytes, offset);
            }
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.DynamicObjectSegments.UnityEngine
{
    using global::System;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;

    public class Vector3Formatter<TTypeResolver> : Formatter<TTypeResolver, global::UnityEngine.Vector3>
        where TTypeResolver : ITypeResolver, new()
    {
        readonly Formatter<TTypeResolver, float> formatter0;
        readonly Formatter<TTypeResolver, float> formatter1;
        readonly Formatter<TTypeResolver, float> formatter2;
        
        public override bool NoUseDirtyTracker
        {
            get
            {
                return formatter0.NoUseDirtyTracker
                    && formatter1.NoUseDirtyTracker
                    && formatter2.NoUseDirtyTracker
                ;
            }
        }

        public Vector3Formatter()
        {
            formatter0 = Formatter<TTypeResolver, float>.Default;
            formatter1 = Formatter<TTypeResolver, float>.Default;
            formatter2 = Formatter<TTypeResolver, float>.Default;
            
        }

        public override int? GetLength()
        {
            return 12;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::UnityEngine.Vector3 value)
        {
            BinaryUtil.EnsureCapacity(ref bytes, offset, 12);
            var startOffset = offset;
            offset += formatter0.Serialize(ref bytes, offset, value.x);
            offset += formatter1.Serialize(ref bytes, offset, value.y);
            offset += formatter2.Serialize(ref bytes, offset, value.z);
            return offset - startOffset;
        }

        public override global::UnityEngine.Vector3 Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 0;
            int size;
            var item0 = formatter0.Deserialize(ref bytes, offset, tracker, out size);
            offset += size;
            byteSize += size;
            var item1 = formatter1.Deserialize(ref bytes, offset, tracker, out size);
            offset += size;
            byteSize += size;
            var item2 = formatter2.Deserialize(ref bytes, offset, tracker, out size);
            offset += size;
            byteSize += size;
            
            return new global::UnityEngine.Vector3(item0, item1, item2);
        }
    }

    public class QuaternionFormatter<TTypeResolver> : Formatter<TTypeResolver, global::UnityEngine.Quaternion>
        where TTypeResolver : ITypeResolver, new()
    {
        readonly Formatter<TTypeResolver, float> formatter0;
        readonly Formatter<TTypeResolver, float> formatter1;
        readonly Formatter<TTypeResolver, float> formatter2;
        readonly Formatter<TTypeResolver, float> formatter3;
        
        public override bool NoUseDirtyTracker
        {
            get
            {
                return formatter0.NoUseDirtyTracker
                    && formatter1.NoUseDirtyTracker
                    && formatter2.NoUseDirtyTracker
                    && formatter3.NoUseDirtyTracker
                ;
            }
        }

        public QuaternionFormatter()
        {
            formatter0 = Formatter<TTypeResolver, float>.Default;
            formatter1 = Formatter<TTypeResolver, float>.Default;
            formatter2 = Formatter<TTypeResolver, float>.Default;
            formatter3 = Formatter<TTypeResolver, float>.Default;
            
        }

        public override int? GetLength()
        {
            return 16;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::UnityEngine.Quaternion value)
        {
            BinaryUtil.EnsureCapacity(ref bytes, offset, 16);
            var startOffset = offset;
            offset += formatter0.Serialize(ref bytes, offset, value.x);
            offset += formatter1.Serialize(ref bytes, offset, value.y);
            offset += formatter2.Serialize(ref bytes, offset, value.z);
            offset += formatter3.Serialize(ref bytes, offset, value.w);
            return offset - startOffset;
        }

        public override global::UnityEngine.Quaternion Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 0;
            int size;
            var item0 = formatter0.Deserialize(ref bytes, offset, tracker, out size);
            offset += size;
            byteSize += size;
            var item1 = formatter1.Deserialize(ref bytes, offset, tracker, out size);
            offset += size;
            byteSize += size;
            var item2 = formatter2.Deserialize(ref bytes, offset, tracker, out size);
            offset += size;
            byteSize += size;
            var item3 = formatter3.Deserialize(ref bytes, offset, tracker, out size);
            offset += size;
            byteSize += size;
            
            return new global::UnityEngine.Quaternion(item0, item1, item2, item3);
        }
    }

    public class Vector2Formatter<TTypeResolver> : Formatter<TTypeResolver, global::UnityEngine.Vector2>
        where TTypeResolver : ITypeResolver, new()
    {
        readonly Formatter<TTypeResolver, float> formatter0;
        readonly Formatter<TTypeResolver, float> formatter1;
        
        public override bool NoUseDirtyTracker
        {
            get
            {
                return formatter0.NoUseDirtyTracker
                    && formatter1.NoUseDirtyTracker
                ;
            }
        }

        public Vector2Formatter()
        {
            formatter0 = Formatter<TTypeResolver, float>.Default;
            formatter1 = Formatter<TTypeResolver, float>.Default;
            
        }

        public override int? GetLength()
        {
            return 8;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::UnityEngine.Vector2 value)
        {
            BinaryUtil.EnsureCapacity(ref bytes, offset, 8);
            var startOffset = offset;
            offset += formatter0.Serialize(ref bytes, offset, value.x);
            offset += formatter1.Serialize(ref bytes, offset, value.y);
            return offset - startOffset;
        }

        public override global::UnityEngine.Vector2 Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 0;
            int size;
            var item0 = formatter0.Deserialize(ref bytes, offset, tracker, out size);
            offset += size;
            byteSize += size;
            var item1 = formatter1.Deserialize(ref bytes, offset, tracker, out size);
            offset += size;
            byteSize += size;
            
            return new global::UnityEngine.Vector2(item0, item1);
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.DynamicObjectSegments.GameSaving.States
{
    using global::System;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;

    public class MonoBehaviourStateFormatter<TTypeResolver> : Formatter<TTypeResolver, global::GameSaving.States.MonoBehaviourState>
        where TTypeResolver : ITypeResolver, new()
    {
        readonly global::System.Collections.Generic.IEqualityComparer<global::GameSaving.States.MonoBehaviourStateKind> comparer;
        readonly global::GameSaving.States.MonoBehaviourStateKind[] unionKeys;
        
        public MonoBehaviourStateFormatter()
        {
            comparer = global::ZeroFormatter.Comparers.ZeroFormatterEqualityComparer<global::GameSaving.States.MonoBehaviourStateKind>.Default;
            unionKeys = new global::GameSaving.States.MonoBehaviourStateKind[4];
            unionKeys[0] = new global::GameSaving.States.EntityState().Type;
            unionKeys[1] = new global::GameSaving.States.CameraState().Type;
            unionKeys[2] = new global::GameSaving.States.Charaters.LizardState().Type;
            unionKeys[3] = new global::TeamZ.Assets.GameSaving.States.CharacterControllerState().Type;
            
        }

        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::GameSaving.States.MonoBehaviourState value)
        {
            if (value == null)
            {
                return BinaryUtil.WriteInt32(ref bytes, offset, -1);
            }

            var startOffset = offset;

            offset += 4;
            offset += Formatter<TTypeResolver, global::GameSaving.States.MonoBehaviourStateKind>.Default.Serialize(ref bytes, offset, value.Type);

            if (value is global::GameSaving.States.EntityState)
            {
                offset += Formatter<TTypeResolver, global::GameSaving.States.EntityState>.Default.Serialize(ref bytes, offset, (global::GameSaving.States.EntityState)value);
            }
            else if (value is global::GameSaving.States.CameraState)
            {
                offset += Formatter<TTypeResolver, global::GameSaving.States.CameraState>.Default.Serialize(ref bytes, offset, (global::GameSaving.States.CameraState)value);
            }
            else if (value is global::GameSaving.States.Charaters.LizardState)
            {
                offset += Formatter<TTypeResolver, global::GameSaving.States.Charaters.LizardState>.Default.Serialize(ref bytes, offset, (global::GameSaving.States.Charaters.LizardState)value);
            }
            else if (value is global::TeamZ.Assets.GameSaving.States.CharacterControllerState)
            {
                offset += Formatter<TTypeResolver, global::TeamZ.Assets.GameSaving.States.CharacterControllerState>.Default.Serialize(ref bytes, offset, (global::TeamZ.Assets.GameSaving.States.CharacterControllerState)value);
            }
            
            else
            {
                throw new Exception("Unknown subtype of Union:" + value.GetType().FullName);
            }
        
            var writeSize = offset - startOffset;
            BinaryUtil.WriteInt32(ref bytes, startOffset, writeSize);
            return writeSize;
        }

        public override global::GameSaving.States.MonoBehaviourState Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            if ((byteSize = BinaryUtil.ReadInt32(ref bytes, offset)) == -1)
            {
                byteSize = 4;
                return null;
            }
        
            offset += 4;
            int size;
            var unionKey = Formatter<TTypeResolver, global::GameSaving.States.MonoBehaviourStateKind>.Default.Deserialize(ref bytes, offset, tracker, out size);
            offset += size;

            global::GameSaving.States.MonoBehaviourState result;
            if (comparer.Equals(unionKey, unionKeys[0]))
            {
                result = Formatter<TTypeResolver, global::GameSaving.States.EntityState>.Default.Deserialize(ref bytes, offset, tracker, out size);
            }
            else if (comparer.Equals(unionKey, unionKeys[1]))
            {
                result = Formatter<TTypeResolver, global::GameSaving.States.CameraState>.Default.Deserialize(ref bytes, offset, tracker, out size);
            }
            else if (comparer.Equals(unionKey, unionKeys[2]))
            {
                result = Formatter<TTypeResolver, global::GameSaving.States.Charaters.LizardState>.Default.Deserialize(ref bytes, offset, tracker, out size);
            }
            else if (comparer.Equals(unionKey, unionKeys[3]))
            {
                result = Formatter<TTypeResolver, global::TeamZ.Assets.GameSaving.States.CharacterControllerState>.Default.Deserialize(ref bytes, offset, tracker, out size);
            }
            else
            {
                throw new Exception("Unknown unionKey type of Union: " + unionKey.ToString());
            }

            return result;
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.DynamicObjectSegments
{
    using global::System;
    using global::System.Collections.Generic;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;


    public class CharacterControllerScript_DirectionFormatter<TTypeResolver> : Formatter<TTypeResolver, global::CharacterControllerScript.Direction>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return 4;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::CharacterControllerScript.Direction value)
        {
            return BinaryUtil.WriteInt32(ref bytes, offset, (Int32)value);
        }

        public override global::CharacterControllerScript.Direction Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 4;
            return (global::CharacterControllerScript.Direction)BinaryUtil.ReadInt32(ref bytes, offset);
        }
    }


    public class NullableCharacterControllerScript_DirectionFormatter<TTypeResolver> : Formatter<TTypeResolver, global::CharacterControllerScript.Direction?>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return 5;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::CharacterControllerScript.Direction? value)
        {
            BinaryUtil.WriteBoolean(ref bytes, offset, value.HasValue);
            if (value.HasValue)
            {
                BinaryUtil.WriteInt32(ref bytes, offset + 1, (Int32)value.Value);
            }
            else
            {
                BinaryUtil.EnsureCapacity(ref bytes, offset, offset + 5);
            }

            return 5;
        }

        public override global::CharacterControllerScript.Direction? Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 5;
            var hasValue = BinaryUtil.ReadBoolean(ref bytes, offset);
            if (!hasValue) return null;

            return (global::CharacterControllerScript.Direction)BinaryUtil.ReadInt32(ref bytes, offset + 1);
        }
    }



    public class CharacterControllerScript_DirectionEqualityComparer : IEqualityComparer<global::CharacterControllerScript.Direction>
    {
        public bool Equals(global::CharacterControllerScript.Direction x, global::CharacterControllerScript.Direction y)
        {
            return (Int32)x == (Int32)y;
        }

        public int GetHashCode(global::CharacterControllerScript.Direction x)
        {
            return (int)x;
        }
    }



    public class CharacterControllerScript_FightModeFormatter<TTypeResolver> : Formatter<TTypeResolver, global::CharacterControllerScript.FightMode>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return 4;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::CharacterControllerScript.FightMode value)
        {
            return BinaryUtil.WriteInt32(ref bytes, offset, (Int32)value);
        }

        public override global::CharacterControllerScript.FightMode Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 4;
            return (global::CharacterControllerScript.FightMode)BinaryUtil.ReadInt32(ref bytes, offset);
        }
    }


    public class NullableCharacterControllerScript_FightModeFormatter<TTypeResolver> : Formatter<TTypeResolver, global::CharacterControllerScript.FightMode?>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return 5;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::CharacterControllerScript.FightMode? value)
        {
            BinaryUtil.WriteBoolean(ref bytes, offset, value.HasValue);
            if (value.HasValue)
            {
                BinaryUtil.WriteInt32(ref bytes, offset + 1, (Int32)value.Value);
            }
            else
            {
                BinaryUtil.EnsureCapacity(ref bytes, offset, offset + 5);
            }

            return 5;
        }

        public override global::CharacterControllerScript.FightMode? Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 5;
            var hasValue = BinaryUtil.ReadBoolean(ref bytes, offset);
            if (!hasValue) return null;

            return (global::CharacterControllerScript.FightMode)BinaryUtil.ReadInt32(ref bytes, offset + 1);
        }
    }



    public class CharacterControllerScript_FightModeEqualityComparer : IEqualityComparer<global::CharacterControllerScript.FightMode>
    {
        public bool Equals(global::CharacterControllerScript.FightMode x, global::CharacterControllerScript.FightMode y)
        {
            return (Int32)x == (Int32)y;
        }

        public int GetHashCode(global::CharacterControllerScript.FightMode x)
        {
            return (int)x;
        }
    }



    public class DoorScript_DoorStateFormatter<TTypeResolver> : Formatter<TTypeResolver, global::DoorScript.DoorState>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return 4;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::DoorScript.DoorState value)
        {
            return BinaryUtil.WriteInt32(ref bytes, offset, (Int32)value);
        }

        public override global::DoorScript.DoorState Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 4;
            return (global::DoorScript.DoorState)BinaryUtil.ReadInt32(ref bytes, offset);
        }
    }


    public class NullableDoorScript_DoorStateFormatter<TTypeResolver> : Formatter<TTypeResolver, global::DoorScript.DoorState?>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return 5;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::DoorScript.DoorState? value)
        {
            BinaryUtil.WriteBoolean(ref bytes, offset, value.HasValue);
            if (value.HasValue)
            {
                BinaryUtil.WriteInt32(ref bytes, offset + 1, (Int32)value.Value);
            }
            else
            {
                BinaryUtil.EnsureCapacity(ref bytes, offset, offset + 5);
            }

            return 5;
        }

        public override global::DoorScript.DoorState? Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 5;
            var hasValue = BinaryUtil.ReadBoolean(ref bytes, offset);
            if (!hasValue) return null;

            return (global::DoorScript.DoorState)BinaryUtil.ReadInt32(ref bytes, offset + 1);
        }
    }



    public class DoorScript_DoorStateEqualityComparer : IEqualityComparer<global::DoorScript.DoorState>
    {
        public bool Equals(global::DoorScript.DoorState x, global::DoorScript.DoorState y)
        {
            return (Int32)x == (Int32)y;
        }

        public int GetHashCode(global::DoorScript.DoorState x)
        {
            return (int)x;
        }
    }



}
#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.DynamicObjectSegments.GameSaving.States
{
    using global::System;
    using global::System.Collections.Generic;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;


    public class MonoBehaviourStateKindFormatter<TTypeResolver> : Formatter<TTypeResolver, global::GameSaving.States.MonoBehaviourStateKind>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return 4;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::GameSaving.States.MonoBehaviourStateKind value)
        {
            return BinaryUtil.WriteInt32(ref bytes, offset, (Int32)value);
        }

        public override global::GameSaving.States.MonoBehaviourStateKind Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 4;
            return (global::GameSaving.States.MonoBehaviourStateKind)BinaryUtil.ReadInt32(ref bytes, offset);
        }
    }


    public class NullableMonoBehaviourStateKindFormatter<TTypeResolver> : Formatter<TTypeResolver, global::GameSaving.States.MonoBehaviourStateKind?>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return 5;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::GameSaving.States.MonoBehaviourStateKind? value)
        {
            BinaryUtil.WriteBoolean(ref bytes, offset, value.HasValue);
            if (value.HasValue)
            {
                BinaryUtil.WriteInt32(ref bytes, offset + 1, (Int32)value.Value);
            }
            else
            {
                BinaryUtil.EnsureCapacity(ref bytes, offset, offset + 5);
            }

            return 5;
        }

        public override global::GameSaving.States.MonoBehaviourStateKind? Deserialize(ref byte[] bytes, int offset, global::ZeroFormatter.DirtyTracker tracker, out int byteSize)
        {
            byteSize = 5;
            var hasValue = BinaryUtil.ReadBoolean(ref bytes, offset);
            if (!hasValue) return null;

            return (global::GameSaving.States.MonoBehaviourStateKind)BinaryUtil.ReadInt32(ref bytes, offset + 1);
        }
    }



    public class MonoBehaviourStateKindEqualityComparer : IEqualityComparer<global::GameSaving.States.MonoBehaviourStateKind>
    {
        public bool Equals(global::GameSaving.States.MonoBehaviourStateKind x, global::GameSaving.States.MonoBehaviourStateKind y)
        {
            return (Int32)x == (Int32)y;
        }

        public int GetHashCode(global::GameSaving.States.MonoBehaviourStateKind x)
        {
            return (int)x;
        }
    }



}
#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
