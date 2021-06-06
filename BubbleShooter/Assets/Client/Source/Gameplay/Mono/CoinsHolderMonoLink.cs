using Client.Source.Gameplay.Components;
using Leopotam.Ecs;
using StubbUnity.Unity.View;

namespace Client.Source.Gameplay.Mono
{
    public class CoinsHolderMonoLink : EcsViewLink
    {
        public override void Initialize()
        {
            GetEntity().Get<CoinsHolderComponent>().CoinsHolder = this;
        }
    }
}