using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using SullysToolkit;


public class CameraController : MonoSingleton<CameraController>
{
    //Declarations
    [Header("Camera Controller Settings")]
    [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;
    [SerializeField] private Transform _playspaceOrigin;
    [SerializeField] private GameObject _currentFocusObject;


    [Header("Debug Utilities & Commands")]
    [SerializeField] private bool _isDebugActive = false;



    //Monobehaviors






    //Internal Utils
    protected override void InitializeAdditionalFields()
    {
        InitializeUtils();
    }

    private void InitializeUtils()
    {
        if (_mainVirtualCamera == null)
            STKDebugLogger.LogError("No main camera set for the CameraController");

        if (_playspaceOrigin == null)
        {
            _playspaceOrigin = transform;
            STKDebugLogger.LogWarning("CameraController's playspaceOrigin set to CameraController's transform.");
        }


        if (IsCurrentFocusNull())
            SetCurrentFocus(_playspaceOrigin.gameObject);
        else SetCurrentFocus(_currentFocusObject);
    }



    //Getters, Setters, & Commands
    public bool IsCurrentFocusNull()
    {
        return _currentFocusObject == null;
    }

    public GameObject GetCurrentFocus()
    {
        return _currentFocusObject;
    }

    public void SetCurrentFocus(GameObject newFocusObject)
    {
        if (newFocusObject != null)
        {
            _currentFocusObject = newFocusObject;
            _mainVirtualCamera.Follow = newFocusObject.transform;
        }

    }



    //Debug Utils
    public bool IsDebugActive()
    {
        return _isDebugActive;
    }

    public void EnterDebug()
    {
        _isDebugActive = true;
    }

    public void ExitDebug()
    {
        _isDebugActive = false;
    }

    public void ToggleDebug()
    {
        if (IsDebugActive())
            ExitDebug();

        else EnterDebug();
    }

}
