﻿using Client.Source.Gameplay.Components;
using Leopotam.Ecs;
using StubbUnity.Unity.View;

namespace Client.Source.Gameplay.Mono
{
    public class DestroyParticleMonoLink : EcsViewLink
    {
        public override void Initialize()
        {
            GetEntity().Get<DestroyParticleComponent>().DestroyParticle = this;
        }
    }
}