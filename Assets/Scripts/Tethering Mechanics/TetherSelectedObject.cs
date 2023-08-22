using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class TetherSelectedObject : MonoBehaviour
{
    //Declarations
    [Header("Tether Settings")]
    [SerializeField] private LayerMask _tetherableLayerMask;
    [SerializeField] private Transform _tetherAnchor;
    [SerializeField] private Rigidbody2D _tetheredObjectRB;
    [SerializeField] private float _tetherBreakForce = 0;
    [SerializeField] private float _tetherLength;
    [SerializeField] private float _inputCooldown = .25f;
    private bool _isInputReady = true;

    private Rigidbody2D _shipRB;
    private SpringJoint2D _jointRef;
    private MouseToWorld2D _mouseRef;
    private RaycastHit2D _detectedResult;


    [Header("Debug Utilities")]
    [SerializeField] private bool _isDebugActive = false;
    [SerializeField] private float _currentDistanceFromShip = 0;

    //Monobehaviours
    private void Awake()
    {
        _mouseRef = GetComponent<MouseToWorld2D>();
        _shipRB = GetComponent<Rigidbody2D>();
        _jointRef = GetComponent<SpringJoint2D>();
        SetupSpringJoint2D();
    }

    private void Update()
    {
        ListenForMouseClick();
    }

    //Internal Utils
    private void SetupSpringJoint2D()
    {
        _jointRef.autoConfigureConnectedAnchor = false;
        _jointRef.autoConfigureDistance = false;
        _jointRef.breakForce = _tetherBreakForce;
        _jointRef.anchor = _tetherAnchor.position;
        _jointRef.distance = _tetherLength;
    }

    private void ListenForMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            _detectedResult = Physics2D.Raycast(_mouseRef.GetWorldPosition(), Vector3.forward, _tetherableLayerMask);


            //if a collider was detected && the detected collider isn't this object && we aren't already tethered
            if (_detectedResult.collider != null && _detectedResult.collider != GetComponent<CompositeCollider2D>() &&_tetheredObjectRB == null & _isInputReady)
            {
                Rigidbody2D foundObjectRB = _detectedResult.collider.GetComponent<Rigidbody2D>();
                Vector2 tetherPoint = foundObjectRB.transform.InverseTransformPoint(_mouseRef.GetWorldPosition());
                STKDebugLogger.LogStatement(_isDebugActive, $"Tether Anchor point: {tetherPoint}");
                TetherObjectToShip(foundObjectRB, tetherPoint);
                CooldownInput();
            }

            else if (_tetheredObjectRB != null && _isInputReady)
            {
                STKDebugLogger.LogStatement(_isDebugActive, "Removing Tether...");
                UntetherObject();
                CooldownInput();
            }
            else if (_isInputReady)
            {
                STKDebugLogger.LogStatement(_isDebugActive, "No Valid Collider Detected");
                CooldownInput();
            }
        }
    }

    private void CooldownInput()
    {
        _isInputReady = false;
        Invoke("ReadyInput", _inputCooldown);
    }

    private void ReadyInput()
    {
        _isInputReady = true;
    }

    //Getters, Setters, & Commands
    public void TetherObjectToShip(Rigidbody2D newSelection, Vector2 tetherPoint)
    {
        if (newSelection != null)
        {
            _tetheredObjectRB = newSelection;
            _jointRef.connectedBody = _tetheredObjectRB;
            _jointRef.connectedAnchor = tetherPoint;
            _jointRef.enabled = true;
        }
            
    }

    public void UntetherObject()
    {
        if (_tetheredObjectRB != null)
        {
            _jointRef.enabled = false;
            _tetheredObjectRB = null;
        }
           
    }



}
