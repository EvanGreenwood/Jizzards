using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BasicComputeSpheres bloodController;
    public BasicComputeSpheres boidController;
    public int boidIndex = -1;
    [SerializeField] private float _rotationSpeed = 30;
    private float _movementCounter = 0;
    [SerializeField] private float _movementRate = 3;

    public void Setup(BasicComputeSpheres boidController, int index)
    {
        bloodController = boidController.bloodController;
        this.boidController = boidController;
        boidIndex = index;
    }
    public void Kill()
    {
        boidController.KillBoid( boidIndex);
        bloodController.SpawnBoids(3, transform.position);
        Destroy(gameObject);
    }
    public void SetPositionRotation(Vector3 position, Vector3 direction)
    {
        /*
        float dist =  (transform.position - position).magnitude;
        if (dist > 0)
        {
            _movementCounter += _movementRate;
            float yOffset = Mathf.Abs(Mathf.Sin(_movementCounter) * 0.5f);
            float yDiff = yOffset - transform.position.y   ;
            //
            transform.position = position.WithY(yOffset) ;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction + Vector3.up * (yDiff / dist * 30.5f), Vector3.up), Time.deltaTime * _rotationSpeed);
        }
        else
        {
        */
        transform.position = position ;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction + Vector3.up  , Vector3.up), Time.deltaTime * _rotationSpeed);
        //}
    }
    
}
