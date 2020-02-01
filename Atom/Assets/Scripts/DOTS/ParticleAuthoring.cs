using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Velocity : IComponentData
{
    public float3 Value;
}

public struct Particle : IComponentData
{
    public float Radius;
    public float Mass;
    public float CohesionSpeed;
}

public struct ProtonTag : IComponentData { }
public struct NeutronTag : IComponentData { }
public struct ElectronTag : IComponentData { }

public enum ParticleType { Proton, Neutron, Electron }


[DisallowMultipleComponent]
[RequiresEntityConversion]
public class ParticleAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Radius;
    public float Mass;
    public ParticleType Type;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new Velocity
        {
            Value = new float3(0, 0, 0)
        });

        dstManager.AddComponentData(entity, new Particle
        {
            Radius = Radius,
            Mass = Mass,
            CohesionSpeed = 1.2f
        });

        switch (Type)
        {
            case ParticleType.Proton:
                dstManager.AddComponentData(entity, new ProtonTag());
                break;
            case ParticleType.Neutron:
                dstManager.AddComponentData(entity, new NeutronTag());
                break;
            case ParticleType.Electron:
                dstManager.AddComponentData(entity, new ElectronTag());
                break;
        }

    }
}
