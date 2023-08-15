using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class ShipMovement : MonoBehaviour
{
    //Declarations
    [Header("Input Commands")]
    [SerializeField] private float _yCommand;
    [SerializeField] private float _xCommand;
    [SerializeField] private float _turnCommand;

    [Header("Movement Speeds")]
    [SerializeField] private float _forwardsSpeed;
    [SerializeField] private float _reverseSpeed;
    [SerializeField] private float _strafeSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _maxVelocity = 100;

    [Header("Overrides")]
    [SerializeField] private bool _isMovementDisabled = false;

    [Header("Debug Utils")]
    [SerializeField] private bool _isDebugActive = false;
    [SerializeField] private float _currentVelocityMagnitude;

    //references
    private Rigidbody2D _shipRB;



    //Monobehaviors
    private void Awake()
    {
        _shipRB = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        RotateShip();

        if (_isDebugActive)
        {
            TrackVelocity();
        }
    }

    private void FixedUpdate()
    {
        ApplyMovementViaPhysics();
        ClampVelocity();
    }



    //Internal Utils
    private float ClampInput(float input)
    {
        return Mathf.Clamp(input, -1f, 1f);
    }

    private void ApplyMovementViaPhysics()
    {
        if (_shipRB == null)
        {
            STKDebugLogger.LogError($"{gameObject.name}'s ShipMovement script has no Rigidbody2D component to move");
            return;
        }

        if (!IsMovementDisabled())
        {
            //determine y force based on current y command direction
            float yForce = 0;

            if (_yCommand > 0)
                 yForce = _yCommand * _forwardsSpeed;
            else if (_yCommand < 0)
                yForce = _yCommand * _reverseSpeed;


            //build and apply the move vector
            Vector2 moveVector = new Vector2(_xCommand * _strafeSpeed, yForce);
            _shipRB.AddRelativeForce(moveVector * Time.deltaTime);
        }
    }

    private void ClampVelocity()
    {
        if (_shipRB == null)
        {
            STKDebugLogger.LogError($"{gameObject.name}'s ShipMovement script has no Rigidbody2D component to modify");
            return;
        }

        else
        {
            if (_shipRB.velocity.magnitude > _maxVelocity)
                _shipRB.velocity = Vector2.ClampMagnitude(_shipRB.velocity, _maxVelocity);
        }
    }

    private void RotateShip()
    {
        if (!IsMovementDisabled())
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;

            currentRotation.z -= _turnCommand * _turnSpeed * Time.deltaTime;

            Quaternion currentQuaternion = Quaternion.Euler(currentRotation);

            transform.rotation = currentQuaternion;
        }
    }


    //Getters, Setters, & Commands
    public float GetCurrentThrustCommandValue()
    {
        return _yCommand;
    }

    public float GetCurrentStrafeCommandValue()
    {
        return _xCommand;
    }

    public float GetCurrentTurnCommandValue()
    {
        return _turnCommand;
    }



    public void SetThrustCommand(float newValue)
    {
        _yCommand = ClampInput(newValue);
    }

    public void SetStrafeCommand(float newValue)
    {
        _xCommand = ClampInput(newValue);
    }

    public void SetTurnCommand(float newValue)
    {
        _turnCommand = ClampInput(newValue);
    }



    public float GetMaxVelocity()
    {
        return _maxVelocity;
    }

    public void SetMaxVelocity(float newMax)
    {
        _maxVelocity = newMax;
    }



    public float GetForwardsSpeed()
    {
        return _forwardsSpeed;
    }

    public float GetReverseSpeed()
    {
        return _reverseSpeed;
    }

    public float GetStrafeSpeed()
    {
        return _strafeSpeed;
    }

    public float GetTurnSpeed()
    {
        return _turnSpeed;
    }



    public void SetForwardsSpeed(float newValue)
    {
        _forwardsSpeed = newValue;
    }

    public void SetReverseSpeed(float newValue)
    {
        _reverseSpeed = newValue;
    }

    public void SetStrafeSpeed(float newValue)
    {
        _strafeSpeed = newValue;
    }

    public void SetTurnSpeed(float newValue)
    {
        _turnSpeed = newValue;
    }



    public bool IsMovementDisabled()
    {
        return _isMovementDisabled;
    }

    public void EnableMovement()
    {
        _isMovementDisabled = false;
    }

    public void DisableMovement()
    {
        _isMovementDisabled = true;
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

    private void TrackVelocity()
    {
        _currentVelocityMagnitude = _shipRB.velocity.magnitude;
    }

}
