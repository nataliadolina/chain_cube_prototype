using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using UnityEngine.Rendering;

public class CubeRendererController : MonoBehaviour
{
    private readonly int emission_property_id = Shader.PropertyToID("_EmissionColor");

    [Inject]
    private CubeSettings settings;

    private MeshRenderer meshRenderer;
    private System.Random random;
    public void Init()
    {
        random = new System.Random();
        meshRenderer = GetComponent<MeshRenderer>();

        var shader = meshRenderer.materials[0].shader;
        var emissionFeatureKeyword = new LocalKeyword(shader, "_EMISSION");
        meshRenderer.materials[0].EnableKeyword(emissionFeatureKeyword);
    }

    public void SetColor(int power)
    {
        var colorMap = settings.TwoPowerColorMap;

        if (colorMap.Count == 0)
        {
            SetNewColor(power);
            return;
        }

        if (power > colorMap.Max(x => x.Key))
        {
            SetNewColor(power);
            return;
        }

        else
        {
            SetColor(colorMap, power);
        }
        
    }

    private void SetNewColor(int power)
    {
        
        Color newColor = CalculateColorValue();
        settings.TwoPowerColorMap.Add(new GenericKeyValuePair<int, Color>(power, newColor));
        meshRenderer.material.SetColor(emission_property_id, newColor);
    }

    private void SetColor(List<GenericKeyValuePair<int, Color>> colorMap, int power)
    {
        Color color = colorMap.Where(x => x.Key == power).ToList().FirstOrDefault().Value;
        meshRenderer.material.SetColor(emission_property_id, color);
    }

    private Color CalculateColorValue()
    {
        float max = random.Next(255, 510);
        float v1 = random.Next(150, 255);
        float v2 = max - v1;
        float v3 = Mathf.Clamp(max - v1 - v2, 0, 255);
        float[] generated = { v1, v2, v3 };
        float maxGenerated = generated.Max();
        generated = generated.OrderBy(x => random.Next()).ToArray();
        return new Color(generated[0]/maxGenerated, generated[1]/maxGenerated, generated[2]/maxGenerated);
    }
}
