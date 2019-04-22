using System.Collections.Generic;
using DeepCore;
using UnityEngine;

public class EffectClip : MonoBehaviour
{

    [SerializeField]public RectTransform m_rectTrans;//遮挡容器，即ScrollView

    HashMap<Material,Shader> m_materialList = new HashMap<Material,Shader>();//存放需要修改Shader的Material

    
    void Start()
    {

        //获取所有需要修改shader的material，并替换shader
        var particleSystems = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0, j = particleSystems.Length; i < j; i++)
        {
            var ps = particleSystems[i];
            var mat = ps.GetComponent<Renderer>().material;
            var sharder =  Shader.Find(mat.shader.name + "_ui");
            if (sharder)
            {
                m_materialList.Add(mat,mat.shader);
                mat.shader = sharder;
            }
        }
        var trailRendererSystem = GetComponentsInChildren<TrailRenderer>();

        for (int i = 0, j = trailRendererSystem.Length; i < j; i++)
        {
            var ps = trailRendererSystem[i];
            var mat = ps.GetComponent<Renderer>().material;
            
            var sharder = Shader.Find(mat.shader.name + "_ui");
            if (sharder)
            {
                m_materialList.Add(mat,mat.shader);
                mat.shader = sharder;
            }
        }

        var renders = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0, j = renders.Length; i < j; i++)
        {
            var ps = renders[i];
            var mat = ps.material;
            
            var sharder = Shader.Find(mat.shader.name + "_ui");
            if (sharder)
            {
                m_materialList.Add(mat,mat.shader);
                mat.shader = sharder;
            }
        }
        //给shader的容器坐标变量_Area赋值
        Vector3[] v = new Vector3[4];
        m_rectTrans.GetWorldCorners(v);
        //计算容器在世界坐标的Vector4，xz为左右边界的值，yw为下上边界值
        var area = new Vector4(v[0].x, v[0].y, v[2].x, v[2].y);

        foreach (var entry in m_materialList)
        {
            entry.Key.SetVector("_Area", area);
        }
    }

    public void ResetMaterials()
    {
        foreach (var entry in m_materialList)
        {
            entry.Key.shader = entry.Value;
        }
        m_materialList.Clear();
    }
    
    
    private void OnDestroy()
    {
        ResetMaterials();
    }
}