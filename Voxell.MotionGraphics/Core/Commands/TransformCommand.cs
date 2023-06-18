using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Voxell.MotionGraphics
{
    using static Command;
    using static Interpolation;

    public struct TransformCommandComp : IComponentData { }

    [BurstCompile]
    public static class TransformCommand
    {
        public static FunctionPointer<CommandDelegate> Translate(
            float3 a, float3 b, RefRW<LocalTransform> transform
        ) {
            [AOT.MonoPInvokeCallback(typeof(CommandDelegate))]
            void Func(float t) => transform.ValueRW.Position = math.lerp(a, b, t);
            return CreateFuncPointer(Func);
        }

        public static MotionCommand Translate(
            this RefRW<LocalTransform> transform, in float3 to, in float duration,
            in FunctionPointer<InterpolationDelegate> interpolation
        ) {
            FunctionPointer<CommandDelegate> command = Translate(transform.ValueRO.Position, to, transform);

            MotionCommand motionCommand = new MotionCommand
            {
                Command = command,
                Interpolation = interpolation,
                Start = Command.ElapsedTime,
                Duration = duration,
            };

            Command.IncrementElapsedTime(duration);
            transform.ValueRW.Position = to;

            return motionCommand;
        }

        public static FunctionPointer<CommandDelegate> Rotate(
            quaternion a, quaternion b, RefRW<LocalTransform> transform
        ) {
            void Func(float t) => transform.ValueRW.Rotation = math.slerp(a, b, t);
            return CreateFuncPointer(Func);
        }

        public static FunctionPointer<CommandDelegate> Scale(
            float a, float b, RefRW<LocalTransform> transform
        ) {
            void Func(float t) => transform.ValueRW.Scale = math.lerp(a, b, t);
            return CreateFuncPointer(Func);
        }
    }
}
