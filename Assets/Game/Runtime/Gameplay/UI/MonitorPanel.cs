using UnityEngine;
using UnityEngine.UI;

public class MonitorPanel : UIPanel
{
    [SerializeField] private RawImage contentImage;

    public override void OnOpen(object data = null)
    {
        if (contentImage == null) return;
        if (data is RenderTexture rt) contentImage.texture = rt;
    }
}
