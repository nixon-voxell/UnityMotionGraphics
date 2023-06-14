namespace Voxell.MotionGraphics.Storage
{
    [System.Serializable]
    public struct MotionConfig : IConfig<MotionConfig>
    {
        public int FPS;

        public MotionConfig Default()
        {
            this.FPS = 60;

            return this;
        }

        public void Validate() {}
    }
}
