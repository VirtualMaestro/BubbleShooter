using StubbUnity.StubbFramework.Common;
using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.StubbFramework.Scenes
{
    /// <summary>
    /// Interface for the SceneController which should be implemented in engine specific class.
    /// It will contain original Scene specific engine instance .
    /// </summary>
    public interface ISceneController : IEntityContainer
    {
        IAssetName SceneName { get; }

        /// <summary>
        /// Check if a scene is main.
        /// </summary>
        bool IsMain { get; }

        /// <summary>
        /// Set a scene as main.
        /// </summary>
        void SetAsMain();

        /// <summary>
        /// For internal use. For showing scene use World.ActivateSceneByName() or World.ActivateScene().
        /// </summary>
        void ShowContent();

        /// <summary>
        /// For internal use. For hiding scene use World.DeactivateSceneByName() or World.DeactivateScene().
        /// </summary>
        void HideContent();

        /// <summary>
        /// Determines if scene is active.
        /// </summary>
        bool IsContentActive { get; }
    }
}