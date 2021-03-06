//THIS FILE IS AUTOGENERATED BY GHOSTCOMPILER. DON'T MODIFY OR ALTER.
using AOT;
using Unity.Burst;
using Unity.Networking.Transport;
using Unity.Entities;
using Unity.Collections;
using Unity.NetCode;
using Unity.Transforms;
using Unity.Mathematics;


namespace Assembly_CSharp.Generated
{
    public struct PlayerInputSerializer : ICommandDataSerializer<PlayerInput>
    {
        public void Serialize(ref DataStreamWriter writer, in PlayerInput data)
        {
            writer.WriteInt((int) data.horizontal);
            writer.WriteInt((int) data.vertical);
        }

        public void Deserialize(ref DataStreamReader reader, ref PlayerInput data)
        {
            data.horizontal = (int) reader.ReadInt();
            data.vertical = (int) reader.ReadInt();
        }

        public void Serialize(ref DataStreamWriter writer, in PlayerInput data, in PlayerInput baseline, NetworkCompressionModel compressionModel)
        {
            writer.WritePackedIntDelta((int) data.horizontal, (int) baseline.horizontal, compressionModel);
            writer.WritePackedIntDelta((int) data.vertical, (int) baseline.vertical, compressionModel);
        }

        public void Deserialize(ref DataStreamReader reader, ref PlayerInput data, in PlayerInput baseline, NetworkCompressionModel compressionModel)
        {
            data.horizontal = (int) reader.ReadPackedIntDelta((int) baseline.horizontal, compressionModel);
            data.vertical = (int) reader.ReadPackedIntDelta((int) baseline.vertical, compressionModel);
        }
    }
    public class PlayerInputSendCommandSystem : CommandSendSystem<PlayerInputSerializer, PlayerInput>
    {
        [BurstCompile]
        struct SendJob : IJobEntityBatch
        {
            public SendJobData data;
            public void Execute(ArchetypeChunk chunk, int orderIndex)
            {
                data.Execute(chunk, orderIndex);
            }
        }
        protected override void OnUpdate()
        {
            var sendJob = new SendJob{data = InitJobData()};
            ScheduleJobData(sendJob);
        }
    }
    public class PlayerInputReceiveCommandSystem : CommandReceiveSystem<PlayerInputSerializer, PlayerInput>
    {
        [BurstCompile]
        struct ReceiveJob : IJobEntityBatch
        {
            public ReceiveJobData data;
            public void Execute(ArchetypeChunk chunk, int orderIndex)
            {
                data.Execute(chunk, orderIndex);
            }
        }
        protected override void OnUpdate()
        {
            var recvJob = new ReceiveJob{data = InitJobData()};
            ScheduleJobData(recvJob);
        }
    }
}
