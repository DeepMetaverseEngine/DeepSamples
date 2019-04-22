using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


public enum StateMaterial
{
    NORMAL = 0,
    DISSOLOVE = 10,
    COVER = 20,
}


public class MaterialManager : MonoBehaviour
{
    Renderer render = null;

    public delegate void MaterialUpdate(MaterialUnit matunit,Renderer render, float delta);

    public class MaterialUnit
    {
        public Material[] mMat;
        public MaterialUpdate mUpdateCallBack;
        public int mTimeCount;
        public float[] OrgValue;
        public Color[] OrgCorlor;
    }

    SortedList<int, MaterialUnit> matList = new SortedList<int, MaterialUnit>();
    Dictionary<int, MaterialUnit> cacheList = new Dictionary<int, MaterialUnit>();

    void Start()
    {
        render = GetComponent<Renderer>();
        //SaveOrgMat();
    }

    void Update()
    {
        if (render == null)
        {
            return;
        }
        int matcount = matList.Count;
        try
        {
            if (matcount != 0)
            {
                var unit = matList[matList.Keys[matcount - 1]];
                if (unit.mUpdateCallBack != null)
                {
                    unit.mTimeCount += (int)(Time.deltaTime * 1000);
                    unit.mUpdateCallBack(unit, render, unit.mTimeCount);
                }
            }
        }
        catch (Exception e)
        {
            foreach (var mat in matList)
            {
                Debugger.LogError("key=" + mat.Key);
                Debugger.LogError("Value=" + mat.Value.mMat);
            }
        }
    }


    public void SaveOrgMat()
    {
        if (render == null)
        {
            render = GetComponent<Renderer>();
            if (render == null)
            {
                //GameDebug.LogError(this.gameObject.name + "'s render is null");
                return;
            }
        }
        MaterialUnit mUnit = new MaterialUnit();
        mUnit.mMat = render.materials;
        mUnit.mUpdateCallBack = null;
        mUnit.mTimeCount = 0;
        if (!matList.ContainsKey((int)StateMaterial.NORMAL))
        {
            matList.Add((int)StateMaterial.NORMAL, mUnit);
            cacheList.Add((int)StateMaterial.NORMAL, mUnit);
            int i = 0;
            int j = 0;
            mUnit.OrgCorlor = new Color[render.materials.Length];
            mUnit.OrgValue = new float[render.materials.Length];
            foreach (var mat in render.materials)
            {
                if (mat.HasProperty("_fresnel_color"))
                {
                    mUnit.OrgCorlor[i++] = mat.GetColor("_fresnel_color");
                }
                if (mat.HasProperty("_fresnel_area"))
                {
                    mUnit.OrgValue[j++] = mat.GetFloat("_fresnel_area");
                }
            }
        }
       
    }




    private static Shader DISSOLOVE_SHADER = null;
    private Shader getDissoloveShader()
    {
        if (DISSOLOVE_SHADER == null)
        {
//            Material[] matlist = matList[(int)StateMaterial.NORMAL].mMat;
//            if (matlist != null)
//            {
//                DISSOLOVE_SHADER = matlist[0].shader;
//            }
//            else
//            {
                DISSOLOVE_SHADER = GameGlobal.Instance.getShader("TL/Char/CharToon");
            //}
           
            //DISSOLOVE_SHADER = GameGlobal.Instance.getShader("TL/Char/CharToon");//Shader.Find("King/Character/ToonCharacterClip");
        }
        return DISSOLOVE_SHADER;
    }

    public void Onhit(StateMaterial sm, MaterialUpdate updateCallBack = null)
    {
        if (render == null)
        {
            return;
        }
        if (!matList.ContainsKey((int)sm))
        {
            return;
        }
        MaterialUnit mUnit = matList[(int)sm];
        mUnit.mUpdateCallBack = updateCallBack;
        mUnit.mTimeCount = 0;
       
        foreach (var mat in render.materials)
        {
            float value = 1;
            if (mat.HasProperty("_fresnel_color"))
            {
                mat.SetColor("_fresnel_color", new Color(value, value, value, value));
            }
            if (mat.HasProperty("_fresnel_area"))
            {
                mat.SetFloat("_fresnel_area", 2);
            }
            
        }
    }

    public void resetHit(StateMaterial sm)
    {
        if (render == null)
        {
            return;
        }
        
        MaterialUnit mUnit = matList[(int)sm];
        mUnit.mUpdateCallBack = null;
        mUnit.mTimeCount = 0;
        int i = 0;
        int j = 0;
        foreach (var mat in render.materials)
        {
            if (mat.HasProperty("_fresnel_color"))
            {
                mat.SetColor("_fresnel_color", mUnit.OrgCorlor[i++]);
                
            }
            if (mat.HasProperty("_fresnel_area"))
            {
                mat.SetFloat("_fresnel_area", mUnit.OrgValue[j++]);
            }
        }
        
    }

    public void AddMatState(StateMaterial sm, MaterialUpdate updateCallBack = null)
    {
        if (render == null)
        {
            return;
        }
        if (!matList.ContainsKey((int)sm))
        {
            if (!cacheList.ContainsKey((int)sm))
            {
                Material[] normalMats = matList[(int)StateMaterial.NORMAL].mMat;
                Material[] materials = new Material[normalMats.Length];
                for (int i = 0; i< normalMats.Length; ++i)
                {
                    Material mat = normalMats[i];
                    Material m = null;
                    if (sm == StateMaterial.DISSOLOVE)
                    {
                        
                        if (mat.shader != null && mat.shader.name == getDissoloveShader().name)
                        {
                            m = new Material(getDissoloveShader());
                            m.CopyPropertiesFromMaterial(mat);
//                            if (mat.HasProperty("_color_map"))
//                            {
//                                Texture colormap = mat.GetTexture("_color_map");
//                                if (colormap != null)
//                                {
//                                    m.SetTexture("_color_map", colormap);
//                                }
//                            }
//                            else
//                            {
//                                //Debuger.Log("material manager=" + gameObject.name);
//                                return;
//                            }
//                            Texture tt = Resources.Load<Texture>("EF_dis_TEX");
//                            m.SetTexture("_dis", tt);
//                            tt = Resources.Load<Texture>("EF_disNoise_TEX");
//                            m.SetTexture("_noise", tt);
                        }
                        else
                        {
                            m = mat;
                        }
                        
                    }
                    else if (sm == StateMaterial.COVER)
                    {
                        m = new Material(Shader.Find("iPhone/Back"));
                        m.SetColor("_NotVisibleColor", new Color(48 / 255.0f, 139 / 255.0f, 1f, 9 / 255.0f));
                        m.mainTexture = mat.mainTexture;
                    }
                    materials[i] = m;
                }
                MaterialUnit mUnit = new MaterialUnit();
                mUnit.mMat = materials;
                mUnit.mUpdateCallBack = updateCallBack;
                mUnit.mTimeCount = 0;
                cacheList.Add((int)sm, mUnit);
            }
            else
            {
                if (sm == StateMaterial.DISSOLOVE)
                {
                    MaterialUnit mUnit;
                    if (cacheList.TryGetValue((int)sm, out mUnit))
                    {
                        foreach (var mat in render.materials)
                        {
                            //mat.SetFloat("_clip", 0.9f);
                            mat.SetFloat("_alpha", 0.9f);
                        }
                        mUnit.mTimeCount = 0;
                        mUnit.mUpdateCallBack = updateCallBack;
                    }
                }
            }
            matList.Add((int)sm, cacheList[(int)sm]);
        }
        render.materials = matList.Values[matList.Count - 1].mMat;
    }

    public void RemoveMatState(StateMaterial sm)
    {
        if (matList.ContainsKey((int)sm))
        {
            matList.Remove((int)sm);
        }
        render.materials = matList.Values[matList.Count - 1].mMat;
    }


    public void ResetMatState()
    {
        if (render == null)
        {
            return;
        }
        Material[] newmatlist = render.materials;
        if (newmatlist != null)
        {
            int k = 0;
            int v = 0;
            MaterialUnit materialunit = null;
            if (matList.TryGetValue((int) StateMaterial.NORMAL, out materialunit) && materialunit != null)
            {
                for (int j = 0; j < newmatlist.Length; j++)
                {
                    newmatlist[j] = materialunit.mMat[j];
                    if (newmatlist[j].HasProperty("_fresnel_color"))
                    {
                        newmatlist[j].SetColor("_fresnel_color", materialunit.OrgCorlor[k++]);

                    }
                    if (newmatlist[j].HasProperty("_fresnel_area"))
                    {
                        newmatlist[j].SetFloat("_fresnel_area", materialunit.OrgValue[v++]);
                    }
                }
                render.materials = newmatlist;
            }
     
        }
        //render.material = matList[(int)StateMaterial.NORMAL].mMat;
        int[] l = new int[matList.Count - 1];
        int i = 0;
        foreach (var item in matList)
        {
            if (item.Key != (int)StateMaterial.NORMAL)
            {
                l[i] = item.Key;
                ++i;
            }
        }
        for (int j = 0; j < l.Length; j++)
        {
            matList.Remove(l[j]);
        }
    }

    void OnDestroy()
    {
        foreach (var item in cacheList)
        {
            int length = item.Value.mMat.Length;
            for (int i = 0; i < length; i++)
            {
                DeepCore.Unity3D.UnityHelper.Destroy(item.Value.mMat[i]);
            }
        }
        cacheList.Clear();
    }
}
