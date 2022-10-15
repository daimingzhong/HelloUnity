using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPulse : MonoBehaviour
{
    [Header("Opacity Parameters")]
    public float maxOpacity;
    public float minOpacity;
    public float opacityStep;

    [Header("Render and Material References")]
    public MeshRenderer objectRenderer;
    public Material objectMaterial;
    private Color tempColor;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = gameObject.GetComponent<MeshRenderer>();
        objectMaterial = objectRenderer.material;

        tempColor = new Color(objectMaterial.color.r, objectMaterial.color.g,
           objectMaterial.color.b, objectMaterial.color.a);
    }

    // Update is called once per frame
    void Update()
    {
        if (tempColor.a <= minOpacity || tempColor.a >= maxOpacity)
            opacityStep *= -1.0f;

        tempColor.a += opacityStep;
        objectMaterial.SetColor("_Color", tempColor);
    }
}
