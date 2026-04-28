using UnityEngine;

public class SpawnLegacy : MonoBehaviour
{
    public GameObject Prefab;

    public uint Num;

    private Unity.Mathematics.Random m_Random = new(2);

    void Start()
    {

        for (int i = 0; i < Num; i++)
        {

            var obj = Instantiate(Prefab);
            obj.transform.localPosition = new(m_Random.NextFloat(-3, 3), 0, m_Random.NextFloat(-6, 6));
            var anim = obj.GetComponent<Animation>();
            anim.wrapMode = WrapMode.Loop;
            var n = m_Random.NextInt(0, anim.GetClipCount());
            foreach (AnimationState state in anim)
            {
                if (n == 0)
                {
                    anim.Play(state.name);
                    break;
                }
                n--;
            }
        }
    }
}