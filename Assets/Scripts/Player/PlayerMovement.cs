using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : SingletonBehaviour<PlayerMovement>
{
    [SerializeField] private float _speed = 3;
    void Start()
    {
        
    }
    public Vector3 GetPosition()
    {
        return transform.position.WithY(0);
    }


    void Update()
    {
        Vector3 direction = new Vector3((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) ? 1 : 0), 0, (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ? -1 : 0)).normalized;

        transform.Translate(direction * Time.deltaTime * _speed);
    }
}
