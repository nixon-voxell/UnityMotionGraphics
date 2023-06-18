using Unity.Entities;

namespace Voxell.MotionGraphics
{
    /// <summary>Ordered sequence of entities containing ICommand components.</summary>
    [InternalBufferCapacity(0)]
    public struct EntityElemComp : IBufferElementData
    {
        public Entity Entity;

        public static explicit operator Entity(EntityElemComp entityElem) => entityElem.Entity;
        public static explicit operator EntityElemComp(Entity entity) => new EntityElemComp { Entity = entity };
    }
}
