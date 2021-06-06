using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Common
{
    public interface IEntityContainer : IDispose
    {
        void Initialize();
        bool HasEntity { get; }
        ref EcsEntity GetEntity();
        void SetEntity(ref EcsEntity entity);
    }
}