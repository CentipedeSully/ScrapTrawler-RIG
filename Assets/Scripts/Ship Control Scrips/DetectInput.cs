using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInput : MonoBehaviour
{
    //Declarations
    [Header("Inputs")]
    [SerializeField] private float _thrustInput;
    [SerializeField] private float _strafeInput;
    [SerializeField] private float _turnInput;

    [SerializeField] private float _zoomInput;

    [Header("References")]
    [SerializeField] private ShipMovement _movementRef;
    [SerializeField] private LookAheadFocus _lookAheadRef;
    [SerializeField] private CameraController _cameraControllerRef;

    //Monobehaviors
    private void Awake()
    {
        InitializeReferences();
    }

    private void Update()
    {
        ListenForInputs();
        CommunicateInputsToMovementReference();
        CommunicateInputsToLookAheadRef();
        CommunicateInputsToCamerwaController();
    }




    //Internal Utils
    private void InitializeReferences()
    {
        if (_movementRef == null)
            _movementRef = GetComponent<ShipMovement>();
    }

    private void ListenForThrustInput()
    {
        _thrustInput = Input.GetAxis("Vertical");
    }

    private void ListenForStrafeInput()
    {
        bool leftStrafe = Input.GetKey(KeyCode.Q);
        bool rightStrafe = Input.GetKey(KeyCode.E);


        //If either (both are pressed) or (both are released), then input is zero
        if ((leftStrafe == rightStrafe))
            _strafeInput = 0;

        else if (leftStrafe)
            _strafeInput = -1;

        else if (rightStrafe)
            _strafeInput = 1;

    }

    private void ListenForTurnInput()
    {
        _turnInput = Input.GetAxis("Horizontal");
    }


    private void ListenForZoomInput()
    {
        _zoomInput = Input.mouseScrollDelta.y;
    }


    private void ListenForInputs()
    {
        ListenForStrafeInput();
        ListenForThrustInput();
        ListenForTurnInput();
        ListenForZoomInput();
    }


    private void CommunicateInputsToMovementReference()
    {
        if (_movementRef != null)
        {
            _movementRef.SetStrafeCommand(_strafeInput);
            _movementRef.SetThrustCommand(_thrustInput);
            _movementRef.SetTurnCommand(_turnInput);
        }
    }

    private void CommunicateInputsToLookAheadRef()
    {
        if (_lookAheadRef != null)
            _lookAheadRef.SetInput(new Vector2(_strafeInput, _thrustInput));
    }

    private void CommunicateInputsToCamerwaController()
    {
        if (_cameraControllerRef != null)
            _cameraControllerRef.SetZoomInput(_zoomInput);
    }


    //Getters, Setters, Commands
    public float GetThrustInput()
    {
        return _thrustInput;
    }

    public float GetTurnInput()
    {
        return _turnInput;
    }

    public float GetStrafeInput()
    {
        return _strafeInput;
    }




}
