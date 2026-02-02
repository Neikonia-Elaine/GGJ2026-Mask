using UnityEngine;
public class MonitorPanel_clawMachine : UIPanel
{
    public override void OnOpen(object data = null)
    {
        base.OnOpen(data);
        Debug.Log("打开ClawMachine监视器面板");
    }
}