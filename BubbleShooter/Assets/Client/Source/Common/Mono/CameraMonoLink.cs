using Client.Source.Common.Components;
using Leopotam.Ecs;
using StubbUnity.Unity.View;
using UnityEngine;

namespace Client.Source.Common.Mono
{
    public class CameraMonoLink : EcsViewLink
    {
        public override void Initialize()
        {
            GetEntity().Get<CameraComponent>().Camera = GetComponent<Camera>();
        }
    }
}