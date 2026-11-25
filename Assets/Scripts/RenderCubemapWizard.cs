using UnityEngine;
using UnityEditor;
using System.Collections;

//Copied from Unity docs RenderToCubemap
public class RenderCubemapWizard : ScriptableWizard
{
    public Transform renderFromPosition;
    public Cubemap cubemap;
    public float fieldOfView = 90;

    void OnWizardUpdate()
    {
        string helpString = "Select transform to render from and cubemap to render into";
        bool isValid = (renderFromPosition != null) && (cubemap != null);
    }

    void OnWizardCreate()
    {
        // create temporary camera for rendering
        GameObject go = new GameObject("CubemapCamera");
        Camera cam = go.AddComponent<Camera>();

        cam.fieldOfView = fieldOfView;
        
        // place it on the object
        go.transform.position = renderFromPosition.position;
        go.transform.rotation = Quaternion.identity;
        // render into cubemap
        cam.RenderToCubemap(cubemap);

        // destroy temporary camera
        DestroyImmediate(go);
    }

    [MenuItem("GameObject/Render into Cubemap")]
    static void RenderCubemap()
    {
        ScriptableWizard.DisplayWizard<RenderCubemapWizard>(
            "Render cubemap", "Render!");
    }
}