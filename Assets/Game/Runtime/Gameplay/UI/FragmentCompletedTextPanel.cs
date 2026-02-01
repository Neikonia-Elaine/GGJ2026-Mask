using System.Collections;
using TMPro;
using UnityEngine;

public class FragmentCompletedTextPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI fragmentCompletedText;
    [SerializeField] private float autoCloseSeconds = 2f;

    private Coroutine closeCo;

    public override void OnOpen(object data = null)
    {
        base.OnOpen(data);

        if (fragmentCompletedText != null)
            fragmentCompletedText.text = data as string ?? "";

        // 重新打开/重复触发时，重置自动关闭计时
        if (closeCo != null) StopCoroutine(closeCo);
        closeCo = StartCoroutine(AutoClose());
    }

    public override void OnClose()
    {
        base.OnClose();

        if (closeCo != null)
        {
            StopCoroutine(closeCo);
            closeCo = null;
        }
    }

    private IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(autoCloseSeconds);
        Game.Runtime.Core.UIManager.Instance.Close<FragmentCompletedTextPanel>();
        closeCo = null;
    }
}
