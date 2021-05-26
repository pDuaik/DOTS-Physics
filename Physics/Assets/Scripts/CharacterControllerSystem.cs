using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class CharacterControllerSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = UnityEngine.Time.deltaTime;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var jobHandle = Entities
            .WithName("CharacterControllerSystem")
            .ForEach((ref PhysicsVelocity physics, ref CharacterData player, ref Rotation rotation) =>
            {
                physics.Linear += vertical * deltaTime * player.speed * math.forward(rotation.Value);
                physics.Angular += new float3(0, horizontal * deltaTime, 0);

            }).Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
