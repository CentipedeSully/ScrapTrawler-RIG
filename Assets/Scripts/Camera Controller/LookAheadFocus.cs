using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAheadFocus : MonoBehaviour
{
    //Declarations
    [Header("Look Ahead Settings")]
    [SerializeField][Min(0)] private float _maxLookAheadDistance = 5;
    [SerializeField] [Min(0)] private float _transitionDuration = 1.5f;
    [SerializeField] private Rigidbody2D _shipRB;

    [Header("Debugging Utilities")]
    [SerializeField] private bool _isDebugActive = false;
    [SerializeField] private Vector2 _inputDirection = Vector2.zero;




    //Monobehaviours
    private void Update()
    {
        DriftFocusTowardsInputDirection();

    }




    //Internal Utils
    private void DriftFocusTowardsInputDirection()
    {
        Vector2 targetPosition = _inputDirection * _maxLookAheadDistance;
        Vector2 currentPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        Vector2 shipVelocity = _shipRB.velocity;

        if ( currentPosition != targetPosition)
            transform.localPosition =  Vector2.Lerp(currentPosition, targetPosition, _transitionDuration);
        

    }


    //Getters, Setters, & Commands
    public void SetInput(Vector2 newInput)
    {
        _inputDirection = newInput;
        _inputDirection.x = Mathf.Clamp(_inputDirection.x, -1, 1);
        _inputDirection.y = Mathf.Clamp(_inputDirection.y, -1, 1);
    }



}
