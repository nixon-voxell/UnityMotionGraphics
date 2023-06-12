using System.Runtime.CompilerServices;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;

namespace Voxell.MotionGFX
{
    using static Command;

    [BurstCompile]
    public static class TransformCommand
    {
        public static FunctionPointer<CommandDelegate> Translate(
            float3 a, float3 b, RefRW<LocalTransform> transform
        ) {
            // [AOT.MonoPInvokeCallback(typeof(CommandDelegate))]
            void Func(float t) => transform.ValueRW.Position = math.lerp(a, b, t);
            return CreateCommandFuncPointer(Func);
        }

        public static FunctionPointer<CommandDelegate> Rotate(
            quaternion a, quaternion b, RefRW<LocalTransform> transform
        ) {
            // [AOT.MonoPInvokeCallback(typeof(CommandDelegate))]
            void Func(float t) => transform.ValueRW.Rotation = math.slerp(a, b, t);
            return CreateCommandFuncPointer(Func);
        }

        public static FunctionPointer<CommandDelegate> Scale(
            float a, float b, RefRW<LocalTransform> transform
        ) {
            // [AOT.MonoPInvokeCallback(typeof(CommandDelegate))]
            void Func(float t) => transform.ValueRW.Scale = math.lerp(a, b, t);
            return CreateCommandFuncPointer(Func);
        }
    }

    public interface ICommand
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(float time);
    }

    public interface ICommandData<Comp, T>
    where Comp: unmanaged, IComponentData
    where T : unmanaged
    {
        public RefRW<Comp> Component { get; set; }
        public T Start { get; set; }
        public T End { get; set; }
    }

    public struct TranslationCommand : ICommand, ICommandData<LocalTransform, float3>
    {
        public RefRW<LocalTransform> Component { get; set; }
        public float3 Start { get; set; }
        public float3 End { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(float time)
        {
            this.Component.ValueRW.Position = math.lerp(this.Start, this.End, time);
        }
    }
}
