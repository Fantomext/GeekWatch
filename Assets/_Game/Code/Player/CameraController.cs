using Cinemachine;
using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour 
{
    [SerializeField] private GameObject _player;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _sensitivity;
    private float _tempSense;

    private float defaultSense = 150f;
    private static string senceKey = "senceVolume";
    public bool isFollow;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isFollow = true;
        CheckSense();
        SetSensitivity();
        _camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = _player.transform.eulerAngles.y;
    }

    public void SenseZero()
    {
        _tempSense = _sensitivity;
        _sensitivity = 0f;
        SetSensitivity();
    }

    public void SenseNormal()
    {
        _sensitivity = _tempSense;
        SetSensitivity();
    }

    private void CheckSense()
    {
        if (PlayerPrefs.HasKey(senceKey))
        {
            _sensitivity = PlayerPrefs.GetFloat(senceKey);
        }
        else
        {
            _sensitivity = defaultSense;
            PlayerPrefs.SetFloat(senceKey, _sensitivity);
        }
        SetSensitivity();
    }
   

    private void Update()
    {
        if(isFollow)
            _player.transform.eulerAngles =
                new Vector3(_player.transform.eulerAngles.x, transform.eulerAngles.y, _player.transform.eulerAngles.z);

        //SetSensitivity();
    }

    public void ChangeRotation(Transform target)
    {
        Vector3 targetDirection = target.forward;
        Vector3 forward = _player.transform.forward;

        float angle = Vector3.SignedAngle(targetDirection, forward, Vector3.up) * -1;
        _camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value += angle;
        Debug.Log(angle);
    }

    public void SetSensitivity()
    {
        _camera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _sensitivity;
        _camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = _sensitivity;
    }



}
