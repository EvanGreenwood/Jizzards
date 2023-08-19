using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FluidDisplay
{
    [SerializeField] private float _rotationSpeed = 30;
    protected override void LateUpdate()
    {
        base.LateUpdate();
        //
        if (_object.velocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_object.velocity.WithY(0), Vector3.up), Time.deltaTime * _rotationSpeed);
        }
    }
}
