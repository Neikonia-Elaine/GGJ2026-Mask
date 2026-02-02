using UnityEngine;
using Game.Runtime.Core;
public class MonitorRoomManager : MonoBehaviour
{

    private void Awake()
    {
        if (CollectionProgressTracker.Instance.IsAllCollected()){
            UIManager.Instance.Open<EndGamePuzzlePanel>();
        }

    }

    private void Start()
    {

    }

    private void Update()
    {

    }

}