﻿using Client.Source.Gameplay.Components;
using Leopotam.Ecs;
using StubbUnity.Unity.View;

namespace Client.Source.Gameplay.Mono
{
    public class BoostIconsHolderMonoLink : EcsViewLink
    {
        public override void OnInitialize()
        {
            GetEntity().Get<BoostIconsHolderComponent>().BoostIconsHolder = this;
        }
    }
}