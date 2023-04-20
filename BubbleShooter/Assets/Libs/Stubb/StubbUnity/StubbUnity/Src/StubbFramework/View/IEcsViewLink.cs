using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common;

namespace StubbUnity.StubbFramework.View
{
    public interface IEcsViewLink : IEntityContainer, IDestroy
    {
        string Name { get; }
        
        int TypeId { get; set; }
        /// <summary>
        /// An instance of the World where this IEcsViewLink belongs to.
        /// </summary>
        EcsWorld World { get; }

        /// <summary>
        /// Method invoked when a game pauses (EcsWorld.PauseGame).
        /// </summary>
        void OnPause();
       
        /// <summary>
        /// Method invokes when a game resumes (EcsWorld.ResumeGame).
        /// </summary>
        void OnResume();
    }
}