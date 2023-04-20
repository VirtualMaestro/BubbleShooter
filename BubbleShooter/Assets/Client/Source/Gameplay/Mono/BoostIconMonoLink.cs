using Client.Source.Gameplay.Components;
using Client.Source.Gameplay.Events;
using Leopotam.Ecs;
using StubbUnity.Unity.View;
using UnityEngine;

namespace Client.Source.Gameplay.Mono
{
    public class BoostIconMonoLink : EcsViewLink
    {
        [SerializeField] 
        private GameObject boostIconHolder;

        public GameObject BoostIconHolder => boostIconHolder;
        public bool IsEmpty => boostIconHolder.transform.childCount == 0;
        
        public override void OnInitialize()
        {
            GetEntity().Get<BoostIconComponent>().BoostIcon = this;
        }

        private void OnMouseUpAsButton()
        {
            if (IsEmpty) return;
            World.NewEntity().Get<BoostIconClickEvent>().Coin = boostIconHolder.transform.GetChild(0).gameObject;
        }
    }
}