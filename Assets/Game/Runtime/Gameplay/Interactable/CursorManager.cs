using Game.Runtime.Core;
using Game.Runtime.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// 鼠标点击管理器
/// </summary>
public class CursorManager : MonoBehaviour
{
    [SerializeField] protected LayerMask interactLayer;
    [SerializeField] private InputActionReference pointAction;
    [SerializeField] private InputActionReference clickAction;

    private Camera mainCam;
    private Collider2D currentHover;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        pointAction.action.Enable();
        clickAction.action.Enable();
    }

    private void OnDisable()
    {
        pointAction.action.Disable();
        clickAction.action.Disable();
    }

    private void Update()
    {
        // if (GameManager.Instance.CurrentPhase != GamePhase.Gameplay) return;
        if (EventSystem.current && EventSystem.current.IsPointerOverGameObject()) return;

        var screenPos = pointAction.action.ReadValue<Vector2>();
        Vector2 worldPos = mainCam.ScreenToWorldPoint(screenPos);

        currentHover = Physics2D.OverlapPoint(worldPos, interactLayer);
        // if (currentHover != null) 

        if (clickAction.action.WasPressedThisFrame())
        {
            AudioManager.Instance.PlaySFX(AudioName.Click);
            var interactable = currentHover?.GetComponent<Interactable>();
            if (GameManager.Instance.player.gameObject.activeInHierarchy)
                GameManager.Instance.player.NavigationToPosition(
                    worldPos,
                    () => OnNavigationCompleted(interactable)
                );
            else
                interactable?.Interact();
        }
    }

    private void OnNavigationCompleted(Interactable interactable)
    {
        interactable?.Interact();
    }
}