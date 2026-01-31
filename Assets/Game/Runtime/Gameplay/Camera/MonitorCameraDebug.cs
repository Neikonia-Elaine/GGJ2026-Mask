using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MonitorCameraDebug : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void OnPostRender()
    {
        if (cam.targetTexture == null) return;

        RenderTexture.active = cam.targetTexture;
        Texture2D tex = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        tex.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;

        // 采样几个点的颜色
        Color center = tex.GetPixel(tex.width / 2, tex.height / 2);
        Color topLeft = tex.GetPixel(0, tex.height - 1);
        Color bottomRight = tex.GetPixel(tex.width - 1, 0);

        Debug.Log($"RT Center pixel: {center}");
        Debug.Log($"RT TopLeft pixel: {topLeft}");
        Debug.Log($"RT BottomRight pixel: {bottomRight}");

        Destroy(tex);
    }
}