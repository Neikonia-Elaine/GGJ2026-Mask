using UnityEngine;
using UnityEngine.UI;

public class FoodTruckPanel : UIPanel
{
    public override void OnOpen(object data = null)
    {
        base.OnOpen();
        Debug.Log("打开FoodTruckPanel");
    }
}