using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Game.Runtime.Core;

public class SimpleTextPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI text;

    [Header("Playback")]
    [SerializeField] private float autoAdvanceSeconds = 2f; // 每句停留时间
    [SerializeField] private bool clickToAdvance = true;    // 点击/空格推进
    [SerializeField] private bool autoCloseAtEnd = true;    // 播完自动关闭

    private Coroutine playCo;

    public override void OnOpen(object data = null)
    {
        base.OnOpen(data);

        // 重新打开/重复触发时，停止旧播放
        StopPlay();

        // 支持单句
        if (data is string s)
        {
            SetText(s);

            if (autoCloseAtEnd)
                playCo = StartCoroutine(AutoCloseAfter(autoAdvanceSeconds));
            return;
        }

        // 支持多句：string[]
        if (data is string[] arr)
        {
            playCo = StartCoroutine(PlayLines(arr));
            return;
        }

        // 支持多句：List<string>
        if (data is List<string> list)
        {
            playCo = StartCoroutine(PlayLines(list));
            return;
        }

        // 其他类型：清空
        SetText("");
    }

    public override void OnClose()
    {
        base.OnClose();
        StopPlay();
    }

    public void SetText(string s)
    {
        if (text != null) text.text = s ?? "";
    }

    private void StopPlay()
    {
        if (playCo != null)
        {
            StopCoroutine(playCo);
            playCo = null;
        }
    }

    private IEnumerator PlayLines(IList<string> lines)
    {
        if (lines == null || lines.Count == 0)
        {
            if (autoCloseAtEnd) UIManager.Instance.Close<SimpleTextPanel>();
            yield break;
        }

        int i = 0;
        SetText(lines[i]);

        while (true)
        {
            // 等“到时间”或“点击/空格”
            yield return WaitAdvance(autoAdvanceSeconds);

            i++;
            if (i >= lines.Count) break;

            SetText(lines[i]);
        }

        if (autoCloseAtEnd)
            UIManager.Instance.Close<SimpleTextPanel>();

        playCo = null;
    }

    private IEnumerator AutoCloseAfter(float seconds)
    {
        yield return WaitSecondsUnscaled(seconds);
        UIManager.Instance.Close<SimpleTextPanel>();
        playCo = null;
    }

    private IEnumerator WaitAdvance(float seconds)
    {
        if (!clickToAdvance)
        {
            yield return WaitSecondsUnscaled(seconds);
            yield break;
        }

        float t = 0f;
        while (t < seconds)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                yield return null; // 吃掉这一帧，防连点穿透
                yield break;
            }

            t += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private IEnumerator WaitSecondsUnscaled(float seconds)
    {
        float t = 0f;
        while (t < seconds)
        {
            t += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
