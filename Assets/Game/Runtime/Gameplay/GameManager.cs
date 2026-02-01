using System;
using Game.Runtime.Core;
using Game.Runtime.Core.Attributes;
using UnityEngine;
using EventHandler = Game.Runtime.Core.EventHandler;


public class GameManager : Singleton<GameManager>
{
    public Player player;
    [SceneName] public string titleScene;
    [SceneName] public string openingScene;
    [SceneName] public string clawMachineScene;
    [SceneName] public string foodTruckScene;
    [SceneName] public string monitorRoomScene;

    [SceneName] public string boxingScene;
    public GamePhase CurrentPhase { get; private set; }

    private string lastGameScene;
    private bool useSpwan = false;

    public void SetGamePhase(GamePhase newPhase)
    {
        CurrentPhase = newPhase;
        EventHandler.CallGamePhaseChangedEvent(newPhase);
    }

    public bool IsGameplay => CurrentPhase == GamePhase.Gameplay;

    private void Start()
    {
        player.gameObject.SetActive(false);
        GameTitle(); //从标题界面开始
    }

    /// <summary>
    /// 开始新游戏
    /// </summary>
    public void StartNewGame()
    {
        SetGamePhase(GamePhase.Gameplay);
        useSpwan = true;
        TransitionManager.Instance.TransitionTo(openingScene);
        UIManager.Instance.openGameUI();
    }

    /// <summary>
    /// 游戏开始界面
    /// </summary>
    public void GameTitle()
    {
        SetGamePhase(GamePhase.GameTitle);
        TransitionManager.Instance.Transition(string.Empty, titleScene);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartClawMachineGame()
    {
        SetGamePhase(GamePhase.ClawMachineGame);
        lastGameScene = TransitionManager.Instance.currentSceneName;
        TransitionManager.Instance.TransitionTo(clawMachineScene);
    }

    public void ExitClawMachineGame()
    {
        SetGamePhase(GamePhase.Gameplay);
        TransitionManager.Instance.TransitionTo(lastGameScene);
    }

    public void OpenFoodTruckScene()
    {
        lastGameScene = TransitionManager.Instance.currentSceneName;
        SetGamePhase(GamePhase.FoodTruck);
        TransitionManager.Instance.TransitionTo(foodTruckScene);
    }

    public void OpenMonitorRoomScene()
    {
        lastGameScene = TransitionManager.Instance.currentSceneName;
        useSpwan = true;
        TransitionManager.Instance.TransitionTo(monitorRoomScene);
    }

    public void ExitMonitorRoomScene()
    {
        useSpwan = true;
        TransitionManager.Instance.TransitionTo(lastGameScene);
    }

    public void ExitFoodTruckScene()
    {
        SetGamePhase(GamePhase.Gameplay);
        TransitionManager.Instance.TransitionTo(lastGameScene);
    }

    public void StartBoxingScene()
    {
        SetGamePhase(GamePhase.FoodTruck);
        lastGameScene = TransitionManager.Instance.currentSceneName;
        TransitionManager.Instance.TransitionTo(boxingScene);
    }

    public void ExitBoxingScene()
    {
        SetGamePhase(GamePhase.Gameplay);
        TransitionManager.Instance.TransitionTo(lastGameScene);
    }

    private void AfterSceneLoad(string toSceneName)
    {
        var spwan = GameObject.FindWithTag("Spwan");
        if (spwan && useSpwan)
        {
            player.transform.position = spwan.transform.position;
            useSpwan = false;
        }

        player.gameObject.SetActive(CurrentPhase is GamePhase.Gameplay or GamePhase.MonitorRoom);
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += AfterSceneLoad;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= AfterSceneLoad;
    }
}