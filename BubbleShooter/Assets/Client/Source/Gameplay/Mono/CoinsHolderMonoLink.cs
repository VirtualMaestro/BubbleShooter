using Client.Source.Gameplay.Components;
using Leopotam.Ecs;
using StubbUnity.Unity.View;

namespace Client.Source.Gameplay.Mono
{
    public class CoinsHolderMonoLink : EcsViewLink
    {
        public override void OnInitialize()
        {
            GetEntity().Get<CoinsHolderComponent>().CoinsHolder = this;
        }
    }
}