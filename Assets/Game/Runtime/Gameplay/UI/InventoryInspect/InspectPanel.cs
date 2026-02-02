using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InspectPanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image centerImage;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void Show(InspectData d)
    {
        if (d == null) return;

        if (centerImage != null) centerImage.sprite = d.sprite;
        if (descriptionText != null) descriptionText.text = d.description ?? "";

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Hide();
    }
}
