using UnityEngine;
using System.Collections;

public class FogAmbientColorSetting : MonoBehaviour 
{
    public bool fog = false;
    public Color fogColor = Color.black;
    public FogMode fogMode = FogMode.Linear;
    public float fogDensity = 0.01f;
    public float linerFogStart = 0f;
    public float linerFogEnd = 100f;
    public Color ambientLight = Color.white;
    public Color ambientEquatorLight = Color.white;
    public Color ambientGroundLight = Color.white;
    public Material material;

    public void Reset() 
    {
        RenderSettings.fog = fog;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogMode = fogMode;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.fogStartDistance = linerFogStart;
        RenderSettings.fogEndDistance = linerFogEnd;
		RenderSettings.ambientLight = ambientLight;
        RenderSettings.ambientEquatorColor = ambientEquatorLight;
        RenderSettings.ambientGroundColor = ambientGroundLight;
		RenderSettings.skybox = material;
        RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Custom;
        RenderSettings.customReflection = null;
        RenderSettings.reflectionBounces = 1;
        RenderSettings.reflectionIntensity = 1f;
        RenderSettings.subtractiveShadowColor = new Color(0, 0, 0, 0);
    }
}
