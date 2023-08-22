using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeAnimController : MonoBehaviour
{
    //Declarations
    [SerializeField] private Animator _strafeAnimatorRef;
    [SerializeField] private string _animParameterName = "StrafeDirection";


    //Monobhaviours



    //Internal Utils



    //Getters, Setters, & Commands
    public void PlayStrafeLeftAnim()
    {
        _strafeAnimatorRef.SetInteger(_animParameterName, -1);
    }

    public void PlayStrafeRightAnim()
    {
        _strafeAnimatorRef.SetInteger(_animParameterName, 1);
    }

    public void NeutralizeStrafe()
    {
        _strafeAnimatorRef.SetInteger(_animParameterName, 0);
    }


}
