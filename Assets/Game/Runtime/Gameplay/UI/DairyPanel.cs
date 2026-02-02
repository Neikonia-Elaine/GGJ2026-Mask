using UnityEngine.EventSystems;
using Game.Runtime.Core;

public class DairyPanel : UIPanel, IPointerClickHandler
{
    public override void OnOpen(object data = null)
    {
        base.OnOpen();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.Close<DairyPanel>();
    }

}