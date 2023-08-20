using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
public class ClickOnBoids : MonoBehaviour
{
     
    void Start()
    {
        
    }

    //  
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //  Debug.Log("click " + Camera.main.ScreenPointToRay(Input.mousePosition).direction + " Input.mousePosition " + Input.mousePosition + " world " + Camera.main.ScreenToWorldPoint(Input.mousePosition).WithZ(40));

            Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition.WithZ(1));
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition.WithZ(40)) - origin).normalized;
            //
            Debug.Log("click " + direction + " origin " + origin);
            //
            /*
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(new Ray(origin, direction), out float enter))
            {
                Vector3 point = origin + direction * enter;
                Debug.Log("kill here " + point);
                //
                foreach (Collider enemyCollider in Physics.OverlapSphere(point, 0.8f, Layer.Enemy))
                {
                    Debug.Log("Try kill enemy " + enemyCollider.gameObject.name);
                    Enemy e = enemyCollider.GetComponent<Enemy>();
                    if (e != null)
                    {
                        e.boidController.KillBoid(e.boidIndex);
                    }
                }
            }
            */
            if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000 , 1 << Layer.Ground ))
            {
                Debug.Log("kill here " + hit.point);
                foreach (Collider enemyCollider in Physics.OverlapSphere(hit.point, 0.8f, 1 << Layer.Enemy) )
                {
                    Debug.Log("Try kill enemy " + enemyCollider.gameObject.name);
                    Enemy e = enemyCollider.GetComponent<Enemy>();
                    if (e != null)
                    {
                        e.Kill();
                    }
                }
            }
        }
    }
}
