using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Darude/Sandstorm")]
public class Sandstorm : UnityStandardAssets.ImageEffects.PostEffectsBase
{

    public bool flipY;
    [Range(-1.0f, 1.0f)]
    public float vignetteRadius;
    [Range(1.0f, 512.0f)]
    public float noiseScale;
    public Color sandstormColor;

    public Shader sandstormShader = null;
    private Material sandstormMaterial = null;


    public override bool CheckResources()
    {
        CheckSupport(false);
        sandstormMaterial = CheckShaderAndCreateMaterial(sandstormShader, sandstormMaterial);

        if (!isSupported)
            ReportAutoDisable();
        return isSupported;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }


        //sandstormMaterial.SetFloat("flipY", (flipY ? 1.0f : -1.0f));
        sandstormMaterial.SetFloat("vignetteRadius", vignetteRadius);
        sandstormMaterial.SetFloat("ns", noiseScale);
        sandstormMaterial.SetColor("sc", sandstormColor);
        Graphics.Blit(source, destination, sandstormMaterial);
    }
    

}
