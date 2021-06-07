using Client.Source.Gameplay.Components;
using Client.Source.Gameplay.Events;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Source.Gameplay.Systems
{
    public class ShowDestroyParticleSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyParticleComponent> _destroyParticleFilter;
        private EcsFilter<ShowDestroyParticleEvent> _showDestroyParticleFilter;

        public void Run()
        {
            if (_showDestroyParticleFilter.IsEmpty() || _destroyParticleFilter.IsEmpty()) return;
            
            var particleSystem = _destroyParticleFilter.Single().DestroyParticle.GetComponent<ParticleSystem>();

            foreach (var idx in _showDestroyParticleFilter)
            {
                var particleParams = new ParticleSystem.EmitParams
                {
                    position = _showDestroyParticleFilter.Get1(idx).Position,
                    applyShapeToPosition = true
                };
                
                particleSystem.Emit(particleParams, 10);
            }
        }
    }
}