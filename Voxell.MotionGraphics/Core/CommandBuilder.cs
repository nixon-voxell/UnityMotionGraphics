using Unity.Collections;
using Unity.Entities;

namespace Voxell.MotionGraphics
{
    public struct CommandBuilder// : System.IDisposable
    {
        private Entity m_RootEntity;
        private DynamicBuffer<EntityElemComp> m_db_Entities;

        public CommandBuilder(ref SystemState state, FixedString64Bytes name)
        {
            EntityManager manager = state.EntityManager;
            this.m_RootEntity = manager.CreateEntity();
            manager.SetName(this.m_RootEntity, name);

            this.m_db_Entities = manager.AddBuffer<EntityElemComp>(this.m_RootEntity);

            manager.AddComponentData<TimelineComp>(
                this.m_RootEntity, new TimelineComp
                {
                    Playing = false,
                    CurrTime = 0.0f,
                    PrevTime = 0.0f,
                }
            );
        }

        public void AddEntity(Entity entity)
        {
            this.m_db_Entities.Add((EntityElemComp)entity);
        }
    }
}
