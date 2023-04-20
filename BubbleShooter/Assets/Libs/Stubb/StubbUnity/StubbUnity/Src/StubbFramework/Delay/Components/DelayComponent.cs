namespace StubbUnity.StubbFramework.Delay.Components
{
    /// <summary>
    /// Once added this component will be destroyed after some period of time which is set in its fields (Frames or Milliseconds).
    /// E.g. for internal use it is used for delaying of the destroying entity. Let's say it is needed to destroy entity after 5 seconds:
    ///  <br/><c>var entity = World.NewEntity();
    ///  entity.Set &lt;DestroyEntityComponent&gt;();
    ///  entity.Set &lt;DelayComponent&gt;().Milliseconds = 5000;
    /// </c>
    /// </summary>
    public struct DelayComponent
    {
        public int Frames;
        public long Milliseconds;
    }
}