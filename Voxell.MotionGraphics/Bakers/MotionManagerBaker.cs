using Unity.Entities;

namespace Voxell.MotionGraphics
{
    public sealed class MotionManagerBaker : Baker<MotionManagerAuthoring>
    {
        public override void Bake(MotionManagerAuthoring authoring)
        {
            if (authoring.so_MotionConfig == null) return;

            Entity entity = this.GetEntity(TransformUsageFlags.None);

            this.AddComponent<MotionConfigComp>(
                entity, new MotionConfigComp
                {
                    MotionConfig = authoring.so_MotionConfig.Data,
                }
            );
        }
    }
}
