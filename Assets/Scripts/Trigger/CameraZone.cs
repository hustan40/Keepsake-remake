using Cinemachine;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _zoneCamera;
    [SerializeField] private int _priorityOnEnter = 20;
    private int _defaultPriority;

    private void Start()
    {
        _defaultPriority = _zoneCamera.Priority;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _zoneCamera.Priority = _priorityOnEnter;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _zoneCamera.Priority = _defaultPriority;
    }
}