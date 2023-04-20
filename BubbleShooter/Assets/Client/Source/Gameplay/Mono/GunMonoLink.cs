using Client.Source.Gameplay.Components;
using DG.Tweening;
using Leopotam.Ecs;
using StubbUnity.Unity.View;
using UnityEngine;

namespace Client.Source.Gameplay.Mono
{
    public class GunMonoLink : EcsViewLink
    {
        [SerializeField]
        private GameObject currentCoinHolder;
        [SerializeField]
        private GameObject nextCoinHolder;
        [SerializeField]
        private LineRenderer lineRenderer;

        public GameObject CurrentHolder => currentCoinHolder;
        public GameObject NextHolder => nextCoinHolder;
        public LineRenderer LineRenderer => lineRenderer;
        
        public override void OnInitialize()
        {
            GetEntity().Get<GunComponent>().Gun = this;
        }

        private void _RotateHolder()
        {
            (currentCoinHolder, nextCoinHolder) = (nextCoinHolder, currentCoinHolder);
        }

        private void OnMouseUpAsButton()
        {
            transform.DORotate(transform.rotation.eulerAngles + new Vector3(0,0, 180), 0.5f);
            _RotateHolder();
        }
    }
}