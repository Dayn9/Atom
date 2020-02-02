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
    public NativeArray<float3> m_inData;
    public NativeArray<float3> m_outData;

    EntityQuery m_ParticleQuery;

    [BurstCompile]
    struct CohesionSystemJob : IJobForEachWithEntity<Translation, Velocity, Particle>
    {
        public float3 origin;
        [ReadOnly] public NativeArray<float3> inData;
        [WriteOnly] public NativeArray<float3> outData;

        public void Execute(
            Entity entity, 
            int index,
            ref Translation translation,
            ref Velocity velocity,
            ref Particle particle)
        {
            //get the displacement to origin
            Vector3 diffOrigin = origin - translation.Value;
            float3 cohesionForce = Vector3.ClampMagnitude(diffOrigin.normalized * particle.CohesionSpeed, diffOrigin.magnitude);

            outData[index] = translation.Value;

            velocity.Value += cohesionForce;
        }
    }

    [BurstCompile]
    struct SeperationSystemJob : IJobForEach<Translation, Velocity, Particle>
    {
        [ReadOnly] public NativeArray<float3> outData;

        public void Execute(
            ref Translation translation,
            ref Velocity velocity,
            ref Particle particle)
        {
            float3 seperationForce = new float3();
            for(int i=0;i<outData.Length;i++) 
            {
                //don't seperate from self
                if (!translation.Value.Equals(outData[i]))
                {
                    //find the distance between particles
                    Vector3 diffOther = translation.Value - outData[i];

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

            velocity.Value += seperationForce;
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
        int particleCount = m_ParticleQuery.CalculateEntityCount();
        
        m_inData = new NativeArray<float3>(particleCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
        m_outData = new NativeArray<float3>(particleCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);

        var cohesionJob = new CohesionSystemJob
        {
            origin = new float3(0, 0, 0),
            inData = m_inData,
            outData = m_outData
        };

        JobHandle collisionJobHandle = cohesionJob.Schedule(this, inputDependencies);
        collisionJobHandle.Complete();

        var seperationJob = new SeperationSystemJob
        {
            outData = m_outData
        };

        JobHandle seperationJobHandle = seperationJob.Schedule(this, collisionJobHandle);
        seperationJobHandle.Complete();

        var nucleusJob = new NucleusSystemJob
        {
            DeltaTime = Time.deltaTime,
            Drag = 0.01f
        };

        JobHandle nucleusJobHandle = nucleusJob.Schedule(this, seperationJobHandle);

        m_inData.Dispose();
        m_outData.Dispose();

        return nucleusJobHandle;
    }


    protected override void OnCreate()
    {
        m_ParticleQuery = GetEntityQuery(new EntityQueryDesc
        {
            All = new[] { ComponentType.ReadOnly<Velocity>(), ComponentType.ReadOnly<Particle>() }
        });
       
       
        RequireForUpdate(m_ParticleQuery);
    }
}