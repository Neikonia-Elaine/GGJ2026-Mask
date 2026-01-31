using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MonitorCameraSync : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void LateUpdate()
    {
        if (mainCamera != null)
        {
            transform.position = mainCamera.transform.position;
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}
