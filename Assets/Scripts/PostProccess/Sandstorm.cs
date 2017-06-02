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
    [Range(1.0f, 10.0f)]
    public float noiseScale;
    public Color sandstormColor;
	[Range(-1.0f, 1.0f)]
	public float windStrengthX;
	[Range(-1.0f, 1.0f)]
	public float windStrengthY;
	[Range(0.01f, 2.0f)]
	public float stormSpeed;

    public Shader sandstormShader = null;
    private Material sandstormMaterial = null;
	private Vector2 stormDirection;
	private float time = 0.0f;


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
		time += Time.deltaTime*stormSpeed;
		//time = time % stormCycleDuration;

		stormDirection.Set(windStrengthX, windStrengthY);
		stormDirection.Normalize ();
		stormDirection = stormDirection*time;

        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }


        //sandstormMaterial.SetFloat("flipY", (flipY ? 1.0f : -1.0f));
        sandstormMaterial.SetFloat("vignetteRadius", vignetteRadius);
        sandstormMaterial.SetFloat("ns", noiseScale);
        sandstormMaterial.SetColor("sc", sandstormColor);
		sandstormMaterial.SetVector("displace", stormDirection);
        Graphics.Blit(source, destination, sandstormMaterial);
    }
    

}
