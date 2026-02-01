using UnityEngine;
using UnityEngine.InputSystem;
using Game.Runtime.Core;

public class KeyboardManager : MonoBehaviour
{
    public InputAction maskAction { get; private set; }
    public InputAction monitorAction { get; private set; }

    private bool monitorOpen;

    private void Awake()
    {
        maskAction = InputSystem.actions.FindAction("Ability");
        monitorAction = InputSystem.actions.FindAction("Monitor");
    }

    private void OnEnable()
    {
        InputSystem.actions.FindActionMap("Mask").Enable();
    }

    private void OnDisable()
    {
        InputSystem.actions.FindActionMap("Mask").Disable();
    }

    private void Update()
    {
        HandleMask();
        HandleMonitor();
    }

    private void HandleMask()
    {
        if (maskAction == null) return;
        if (!maskAction.WasPressedThisFrame()) return;

        MaskManager.Instance.ToggleMask();
        Debug.Log("Toggled Mask");
    }

    private void HandleMonitor()
    {
        if (monitorAction == null) return;
        if (!monitorAction.WasPressedThisFrame()) return;

        monitorOpen = !monitorOpen;
        
        // 对应场景打开对应监视器
        if (monitorOpen)
        {
            if (GameManager.Instance.CurrentPhase == GamePhase.FoodTruck)
                UIManager.Instance.Open<MonitorPanel_foodTruck>();
            else
                UIManager.Instance.Open<MonitorPanel>();
        }
        else
        {
            if (GameManager.Instance.CurrentPhase == GamePhase.FoodTruck)
                UIManager.Instance.Close<MonitorPanel_foodTruck>();
            else
                UIManager.Instance.Close<MonitorPanel>();
        }
        Debug.Log("Toggled Monitor");
    }
}
