using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EngineEffectsController : MonoBehaviour
{
    //Declarations
    [SerializeField] private Animator _engineCollectionAnimatorRef;
    [SerializeField] private string _animParameterName = "isShipThrusting";


    //Monobehaviours



    //Internal Utilities



    //Getters, Setters, & Commands
    public void FireEngines()
    {
        if (_engineCollectionAnimatorRef != null)
            _engineCollectionAnimatorRef.SetBool(_animParameterName, true);
    }

    public void CutEngines()
    {
        if (_engineCollectionAnimatorRef != null)
            _engineCollectionAnimatorRef.SetBool(_animParameterName, false);
    }

}
