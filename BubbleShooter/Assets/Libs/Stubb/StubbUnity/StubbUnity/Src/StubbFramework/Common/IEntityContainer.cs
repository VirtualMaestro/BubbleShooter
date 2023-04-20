using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Common
{
    public interface IEntityContainer
    {
        void OnInitialize();
        bool HasEntity { get; }
        ref EcsEntity GetEntity();
        void SetEntity(ref EcsEntity entity);
    }
}