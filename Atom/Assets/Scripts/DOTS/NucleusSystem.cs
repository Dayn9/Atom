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
    private EndSimulationEntityCommandBufferSystem m_EndSimECBSystem;

    private EntityQuery m_ParticleQuery;

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

            velocity.Value += cohesionForce;
        }
    }

    [BurstCompile]
    struct SeperationSystemJob : IJobForEach<Translation, Velocity, Particle>
    {
        public float DeltaTime;

        public void Execute(
            ref Translation translation,
            ref Velocity velocity,
            ref Particle particle)
        {
            
            foreach(Particle part in particles)
            {

            }
        }
    }


    [BurstCompile]
    //[RequireComponentTag(typeof(ProtonTag) , typeof(NeutronTag))]
    struct NucleusSystemJob : IJobForEach<Translation, Velocity, Particle>
    {
        public float DeltaTime;

        public void Execute(
            ref Translation translation, 
            ref Velocity velocity,
            ref Particle particle)
        {


            translation.Value += velocity.Value * DeltaTime;
        }
    }

    
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {

        var job = new NucleusSystemJob {
            DeltaTime = Time.deltaTime
        };


        var particleCount = m_ParticleQuery.CalculateEntityCount();
        if (particleCount > 0)
        {
            var hashGrid = GetSingleton<BrickHashGrid>();
            var seperation = new SeperationSystemJob
            {
                Ecb = m_EndSimECBSystem.CreateCommandBuffer().ToConcurrent(),

                BrickGrid = hashGrid,

                BrickTranslationRO = GetComponentDataFromEntity<Position2D>(true),
                BrickRectangleBoundsRO = GetComponentDataFromEntity<RectangleBounds>(true)
            };

            collideBrickHandle = brickJob.Schedule(this, paddleHandle);
            m_EndSimECBSystem.AddJobHandleForProducer(collideBrickHandle);
        }

        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }

    protected override void OnCreate()
    {
        m_EndSimECBSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

        m_ParticleQuery = GetEntityQuery(new EntityQueryDesc
        {
            All = new[] { ComponentType.ReadOnly<Velocity>(), ComponentType.ReadOnly<Particle>() }
        });
    }
}