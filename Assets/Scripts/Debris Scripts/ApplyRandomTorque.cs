using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRandomTorque : MonoBehaviour
{
    //Declarations
    [SerializeField] private float _minRotationalforce= -50;
    [SerializeField] private float _maxRotationalforce = 50;



    //Monobehaviours
    private void Start()
    {
        ApplyRandomRotation();
    }



    //Internal Utils
    private void ApplyRandomRotation()
    {
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(_minRotationalforce, _maxRotationalforce) * Time.deltaTime);
    }



    //Getters, Setters, & Commands





}
