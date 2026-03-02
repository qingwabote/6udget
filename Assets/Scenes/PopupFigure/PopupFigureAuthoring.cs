using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct PopupFigure : IComponentData
{
    public Entity Prefab;
}

public class PopupFigureAuthoring : MonoBehaviour
{
    public GameObject Prefab;

    class PopupFigureBaker : Baker<PopupFigureAuthoring>
    {
        public override void Bake(PopupFigureAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new PopupFigure
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public partial struct FigureSpawn : ISystem
{
    private Unity.Mathematics.Random m_Random;

    public void OnCreate(ref SystemState state)
    {
        m_Random = new Unity.Mathematics.Random(1);
    }

    public void OnUpdate(ref SystemState state)
    {
        if (!SystemAPI.HasSingleton<PopupFigure>())
        {
            return;
        }

        var popupFigureEntity = SystemAPI.GetSingletonEntity<PopupFigure>();
        var popupFigure = state.EntityManager.GetComponentData<PopupFigure>(popupFigureEntity);
        var entity = state.EntityManager.Instantiate(popupFigure.Prefab);

        SystemAPI.GetComponentRW<Figure>(entity).ValueRW.Value = m_Random.NextInt(0, 99);
        SystemAPI.GetComponentRW<FigurePopupState>(entity).ValueRW.Origin = new float3(m_Random.NextFloat(-170, 170), m_Random.NextFloat(-426, 380), 0);
    }
}
