using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderPropertySetter : MonoBehaviour
{
    [Header("References")]
    public UnityEngine.UI.Image imageComponent;

    [Header("Shader fields")]
    public string shaderPropertyName; // Look in the docs for the property name. Don't forget to ENABLE the desired effect on the material.
    public float shaderPropertyValue;

    //Private
    private float shaderPropertyValue_current = float.MinValue;

    void Awake()
    {
        imageComponent.material = Instantiate<Material>(imageComponent.material);
    }

    void Update()
    {
        if (shaderPropertyValue_current != shaderPropertyValue)
        {
            shaderPropertyValue_current = shaderPropertyValue;
            imageComponent.material.SetFloat(shaderPropertyName, shaderPropertyValue);
        }
    }
}
