using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSwitcher : MonoBehaviour
{
    [Header("References")]
    public SkinnedMeshRenderer faceRenderer;

    [Header("Config")]
    public Material desiredFaceMaterial;

    [Header("Debug")]
    public bool active = true;
    public bool logChanges = true;
    Material lastMaterialSet;

    void Awake()
    {
        desiredFaceMaterial = null;
        lastMaterialSet = null;
    }

    void LateUpdate()
    {
        if (!active)
            return;

        //if (faceRenderer.materials[1] == null)
        //    return;

        if (desiredFaceMaterial == null)
            return;

        if (desiredFaceMaterial != lastMaterialSet)
        {
            //Debug.LogFormat("[FS] Changing face: {0} to {1}", faceRenderer.materials[1].name, desiredFaceMaterial.name);
            lastMaterialSet = desiredFaceMaterial;// = lastMaterialSet;

            Material[] mats = faceRenderer.materials;
            Material mat = desiredFaceMaterial;
            mats[1] = mat;
            faceRenderer.materials = mats;
        }

        //if (faceRenderer.sharedMaterials[1] != desiredFaceMaterial)
        //{
        //    Material[] mats = faceRenderer.sharedMaterials;
        //    Material mat = desiredFaceMaterial;
        //    mats[1] = mat;
        //    faceRenderer.sharedMaterials = mats;
        //}
    }
}
