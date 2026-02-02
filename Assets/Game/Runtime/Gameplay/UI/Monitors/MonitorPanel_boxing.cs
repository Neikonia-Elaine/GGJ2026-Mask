using UnityEngine;
public class MonitorPanel_boxing : UIPanel
{
    public override void OnOpen(object data = null)
    {
        base.OnOpen(data);
        Debug.Log("打开Boxing监视器面板");
    }
}