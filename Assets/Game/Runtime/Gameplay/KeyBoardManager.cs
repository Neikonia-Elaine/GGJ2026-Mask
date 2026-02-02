using UnityEngine;
using UnityEngine.InputSystem;
using Game.Runtime.Core;

public class KeyboardManager : MonoBehaviour
{
    public InputAction maskAction { get; private set; }
    public InputAction monitorAction { get; private set; }
    public InputAction exitAction { get; private set; }

    private bool monitorOpen;

    private void Awake()
    {
        maskAction = InputSystem.actions.FindAction("Ability");
        monitorAction = InputSystem.actions.FindAction("Monitor");
        exitAction = InputSystem.actions.FindAction("Exit");
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
        HandleExit();
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
            else if (GameManager.Instance.CurrentPhase == GamePhase.Boxing)
                UIManager.Instance.Open<MonitorPanel_boxing>();
            else if (GameManager.Instance.CurrentPhase == GamePhase.ClawMachineGame)
                UIManager.Instance.Open<MonitorPanel_clawMachine>();
            else
                UIManager.Instance.Open<MonitorPanel>();
        }
        else
        {
            if (GameManager.Instance.CurrentPhase == GamePhase.FoodTruck)
                UIManager.Instance.Close<MonitorPanel_foodTruck>();
            else if (GameManager.Instance.CurrentPhase == GamePhase.Boxing)
                UIManager.Instance.Close<MonitorPanel_boxing>();
            else if (GameManager.Instance.CurrentPhase == GamePhase.ClawMachineGame)
                UIManager.Instance.Close<MonitorPanel_clawMachine>();
            else
                UIManager.Instance.Close<MonitorPanel>();
        }
        Debug.Log("Toggled Monitor");
    }

    public void OnMonitorClicked()
    {
        monitorOpen = !monitorOpen;

        bool isFoodTruck = GameManager.Instance != null
                        && GameManager.Instance.CurrentPhase == GamePhase.FoodTruck;
        bool isBoxing = GameManager.Instance != null
                        && GameManager.Instance.CurrentPhase == GamePhase.Boxing;
        bool isClawMachine = GameManager.Instance != null
                        && GameManager.Instance.CurrentPhase == GamePhase.ClawMachineGame;
        if (monitorOpen)
        {
            if (isFoodTruck) UIManager.Instance.Open<MonitorPanel_foodTruck>();
            else if (isBoxing) UIManager.Instance.Open<MonitorPanel_boxing>();
            else if (isClawMachine) UIManager.Instance.Open<MonitorPanel_clawMachine>();
            else UIManager.Instance.Open<MonitorPanel>();
        }
        else
        {
            if (isFoodTruck) UIManager.Instance.Close<MonitorPanel_foodTruck>();
            else if (isBoxing) UIManager.Instance.Close<MonitorPanel_boxing>();
            else if (isClawMachine) UIManager.Instance.Close<MonitorPanel_clawMachine>();
            else UIManager.Instance.Close<MonitorPanel>();
        }

        Debug.Log("Toggled Monitor (UI)");
    }

    private void HandleExit()
    {
        if (exitAction == null) return;
        if (!exitAction.WasPressedThisFrame()) return;
        if (GameManager.Instance.CurrentPhase == GamePhase.FoodTruck)
        {
            GameManager.Instance.ExitFoodTruckScene();
        }
        if (GameManager.Instance.CurrentPhase == GamePhase.Boxing)
        {
            GameManager.Instance.ExitBoxingScene();
        }
        if (GameManager.Instance.CurrentPhase == GamePhase.ClawMachineGame)
        {
            GameManager.Instance.ExitClawMachineGame();
        }
    }
}
