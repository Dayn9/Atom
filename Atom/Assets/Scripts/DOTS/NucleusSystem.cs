using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public class NucleusSystem : JobComponentSystem
{
    public static NativeList<float3> positionsBuffer;

    [BurstCompile]
    struct CohesionSystemJob : IJobForEach<Translation, Velocity, Particle>
    {
        public float3 origin;

        public void Execute(
            ref Translation translation,
            ref Velocity velocity,
            ref Particle particle)
        {
            //get the displacement to origin
            Vector3 diffOrigin = origin - translation.Value;
            float3 cohesionForce = Vector3.ClampMagnitude(diffOrigin.normalized * particle.CohesionSpeed, diffOrigin.magnitude);

            positionsBuffer.Add(translation.Value);

            velocity.Value += cohesionForce;
        }
    }

    [BurstCompile]
    struct SeperationSystemJob : IJobForEach<Translation, Velocity, Particle>
    {
        public void Execute(
            ref Translation translation,
            ref Velocity velocity,
            ref Particle particle)
        {
            float3 seperationForce = new float3();
            foreach (float3 pos in positionsBuffer)
            {
                //don't seperate from self
                if (!translation.Value.Equals(pos))
                {
                    //find the distance between particles
                    Vector3 diffOther = translation.Value - pos;

                    //rare occurance, but seperate from identical other
                    if (diffOther.sqrMagnitude < 0.01)
                    {
                        //seperationForce = Unity.Random.insideUnitSphere;
                    }
                    else
                    {
                        //calculate the amount of overlap
                        float overlap = diffOther.magnitude - (2 * particle.Radius);
                        //check if actually overlapping
                        if (overlap < 0)
                        {
                            //add force to seperate
                            seperationForce -= (float3)diffOther.normalized * overlap;
                        }
                    }
                }
            }
        }
    }


    [BurstCompile]
    //[RequireComponentTag(typeof(ProtonTag) , typeof(NeutronTag))]
    struct NucleusSystemJob : IJobForEach<Translation, Velocity, Particle>
    {
        public float DeltaTime;
        public float Drag;

        public void Execute(
            ref Translation translation,
            ref Velocity velocity,
            ref Particle particle)
        {
            velocity.Value = velocity.Value * (1 - Drag);
            translation.Value += velocity.Value * DeltaTime;
        }
    }



    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        positionsBuffer = new NativeList<float3>();

        var cohesionJob = new CohesionSystemJob
        {
            origin = new float3(0, 0, 0),
        };

        JobHandle collisionJobHandle = cohesionJob.Schedule(this, inputDependencies);

        var seperationJob = new SeperationSystemJob
        {
        };

        JobHandle seperationJobHandle = seperationJob.Schedule(this, collisionJobHandle);

        var nucleusJob = new NucleusSystemJob
        {
            DeltaTime = Time.deltaTime,
            Drag = 0.01f
        };

        JobHandle nucleusJobHandle = nucleusJob.Schedule(this, seperationJobHandle);

        return nucleusJobHandle;
    }
}