using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Logging;
using StubbUnity.StubbFramework.Physics.Components;
using StubbUnity.StubbFramework.View;

namespace StubbUnity.StubbFramework.Extensions
{
    public static class CollisionWorldExtension
    {
        private static readonly Dictionary<int, bool> CollisionTable = new Dictionary<int, bool>();
        private static readonly Dictionary<int, bool> RegisterCollisionTable = new Dictionary<int, bool>();

        public static void DispatchTriggerEnter(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var triggerEnter = ref entity.Get<TriggerEnterComponent>();
            triggerEnter.ObjectA = objA;
            triggerEnter.ObjectB = objB;
            triggerEnter.Info = collisionInfo;
        }

        public static void DispatchTriggerEnter2D(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var triggerEnter = ref entity.Get<TriggerEnter2DComponent>();
            triggerEnter.ObjectA = objA;
            triggerEnter.ObjectB = objB;
            triggerEnter.Info = collisionInfo;
        }

        public static void DispatchTriggerStay(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var triggerStay = ref entity.Get<TriggerStayComponent>();
            triggerStay.ObjectA = objA;
            triggerStay.ObjectB = objB;
            triggerStay.Info = collisionInfo;
        }

        public static void DispatchTriggerStay2D(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var triggerStay = ref entity.Get<TriggerStay2DComponent>();
            triggerStay.ObjectA = objA;
            triggerStay.ObjectB = objB;
            triggerStay.Info = collisionInfo;
        }

        public static void DispatchTriggerExit(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var triggerExit = ref entity.Get<TriggerExitComponent>();
            triggerExit.ObjectA = objA;
            triggerExit.ObjectB = objB;
            triggerExit.Info = collisionInfo;
        }

        public static void DispatchTriggerExit2D(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var triggerExit = ref entity.Get<TriggerExit2DComponent>();
            triggerExit.ObjectA = objA;
            triggerExit.ObjectB = objB;
            triggerExit.Info = collisionInfo;
        }

        public static void DispatchCollisionEnter(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var collisionEnter = ref entity.Get<CollisionEnterComponent>();
            collisionEnter.ObjectA = objA;
            collisionEnter.ObjectB = objB;
            collisionEnter.Info = collisionInfo;
        }

        public static void DispatchCollisionEnter2D(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var collisionEnter = ref entity.Get<CollisionEnter2DComponent>();
            collisionEnter.ObjectA = objA;
            collisionEnter.ObjectB = objB;
            collisionEnter.Info = collisionInfo;
        }

        public static void DispatchCollisionStay(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var collisionStay = ref entity.Get<CollisionStayComponent>();
            collisionStay.ObjectA = objA;
            collisionStay.ObjectB = objB;
            collisionStay.Info = collisionInfo;
        }

        public static void DispatchCollisionStay2D(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var collisionStay = ref entity.Get<CollisionStay2DComponent>();
            collisionStay.ObjectA = objA;
            collisionStay.ObjectB = objB;
            collisionStay.Info = collisionInfo;
        }

        public static void DispatchCollisionExit(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var collisionExit = ref entity.Get<CollisionExitComponent>();
            collisionExit.ObjectA = objA;
            collisionExit.ObjectB = objB;
            collisionExit.Info = collisionInfo;
        }

        public static void DispatchCollisionExit2D(this EcsWorld world, IEcsViewLink objA, IEcsViewLink objB,
            object collisionInfo)
        {
            if (CanDispatch(objA.TypeId, objB.TypeId, out int result, out int hash) == false) return;

            RegisterCollision(ref objA, ref objB, in result, in hash);

            var entity = world.NewEntity();
            entity.Get<CleanupCollisionComponent>();

            ref var collisionExit = ref entity.Get<CollisionExit2DComponent>();
            collisionExit.ObjectA = objA;
            collisionExit.ObjectB = objB;
            collisionExit.Info = collisionInfo;
        }

        /// <summary>
        /// Add two uniques ids (ints) as collision pair.
        /// Ids should be > 0.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="typeIdA"></param>
        /// <param name="typeIdB"></param>
        /// <param name="shift"></param>
        public static void AddCollisionPair(this EcsWorld world, int typeIdA, int typeIdB, int shift = 8)
        {
            _VerifyCollisionPair(world, typeIdA, typeIdB, shift);

            CollisionTable[_GetHash(typeIdA, typeIdB, shift)] = true;
        }

        /// <summary>
        /// Check if given collision pair exist.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="typeIdA"></param>
        /// <param name="typeIdB"></param>
        /// <param name="shift"></param>
        /// <returns>
        /// -1 - no collision pair;
        ///  0 - collision pair exists in given order;
        ///  1 - collision pair exists in reverse order;
        /// </returns>
        public static int HasCollisionPair(this EcsWorld world, int typeIdA, int typeIdB, int shift = 8)
        {
            if (typeIdA <= 0 || typeIdB <= 0) return -1;
            if (CollisionTable.ContainsKey(_GetHash(typeIdA, typeIdB, shift))) return 0;
            if (CollisionTable.ContainsKey(_GetHash(typeIdB, typeIdA, shift))) return 1;

            return -1;
        }

        public static void EndPhysicsFrame(this EcsWorld world)
        {
            RegisterCollisionTable.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void RegisterCollision(ref IEcsViewLink objA, ref IEcsViewLink objB, in int result, in int hash)
        {
            if (result == 1)
            {
                var tmp = objA;
                objA = objB;
                objB = tmp;
            }

            RegisterCollisionTable[hash] = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CanDispatch(int typeIdA, int typeIdB, out int result, out int hashResult, int shift = 8)
        {
            result = -1;
            hashResult = -1;

            if (typeIdA <= 0 || typeIdB <= 0) return false;

            var hash = _GetHash(typeIdA, typeIdB, shift);
            if (CollisionTable.ContainsKey(hash))
            {
                result = 0;
                hashResult = hash;
                return RegisterCollisionTable.ContainsKey(hash) == false;
            }

            var reverseHash = _GetHash(typeIdB, typeIdA, shift);
            if (CollisionTable.ContainsKey(reverseHash))
            {
                result = 1;
                hashResult = reverseHash;
                return RegisterCollisionTable.ContainsKey(reverseHash) == false;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int _GetHash(int byte1, int byte2, int shift)
        {
            return byte1 | byte2 << shift;
        }

        [Conditional("DEBUG")]
        private static void _VerifyCollisionPair(EcsWorld world, int typeIdA, int typeIdB, int shift)
        {
            if (typeIdA <= 0 || typeIdB <= 0)
            {
                log.Error($"Wrong collision pair: {typeIdA}:{typeIdB} - collision type should be > 0.");
            }

            if (HasCollisionPair(world, typeIdA, typeIdB, shift) >= 0)
            {
                log.Warn($"Collision pair {typeIdA} : {typeIdB} is already added!");
            }
        }
    }
}