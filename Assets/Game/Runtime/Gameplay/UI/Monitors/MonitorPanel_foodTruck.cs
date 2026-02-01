using UnityEngine;
public class MonitorPanel_foodTruck : UIPanel
{
    public override void OnOpen(object data = null)
    {
        base.OnOpen(data);
        Debug.Log("打开FoodTruck监视器面板");
    }
}