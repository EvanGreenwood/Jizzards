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
      transform.position =  position;

       transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * _rotationSpeed);
    }
    
}
