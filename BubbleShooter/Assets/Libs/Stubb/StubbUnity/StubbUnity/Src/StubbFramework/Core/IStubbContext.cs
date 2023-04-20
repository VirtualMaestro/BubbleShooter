using System;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common;

namespace StubbUnity.StubbFramework.Core
{
    public interface IStubbContext : IDestroy
    {
        EcsWorld World { get; }

        /// <summary>
        /// Injects data globally (for root system and all child systems).
        /// </summary>
        void Inject(object data, Type overridenType = null);
        
        void Init();
        void Run();

        EcsFeature HeadFeature { get; set; }
        EcsFeature MainFeature { get; set; }
        EcsFeature TailFeature { get; set; }
    }
}