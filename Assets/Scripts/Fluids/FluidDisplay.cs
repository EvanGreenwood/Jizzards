using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidDisplay : MonoBehaviour
{
    [SerializeField] private FluidType fluidType;
    protected FluidObject _object;
    protected virtual void Start()
    {
        _object = FluidManager.Instance.AddFluid(transform.position,  fluidType);
        Debug.Log("Create object  " + _object + " type  " + fluidType);
    }

    
   protected virtual  void LateUpdate()
    {
        transform.position = _object.position;
        Debug.Log("LateUpdate  " + _object.position);
    }
}
