using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MyImageEffect : MonoBehaviour
{
    [SerializeField]
    private Shader shader;
    private Material fogMaterial;

    private void Awake()
    {
        // Create a new material with the supplied shader.
        fogMaterial = new Material(shader);
    }

    // OnRenderImage() is called when the camera has finished rendering.
    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {

        Graphics.Blit(src, dst, fogMaterial);
    }
}