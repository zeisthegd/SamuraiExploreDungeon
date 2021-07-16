using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCameraFollow : MonoBehaviour
{
    [SerializeField]VoidEventChannelSO OnPlayerInstantiated;
    CinemachineVirtualCameraBase cineCamBase;

    void Start()
    {
        cineCamBase = GetComponent<CinemachineVirtualCameraBase>();
        OnPlayerInstantiated.OnEventRaised += SetLookAtAndFollow;
    }

    void SetLookAtAndFollow()
    {
        Player player = FindObjectOfType<Player>();
        cineCamBase.Follow = player.transform;
        cineCamBase.LookAt = player.transform;

    }
}
