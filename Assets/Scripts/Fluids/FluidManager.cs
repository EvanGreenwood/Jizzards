using Framework;
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FluidObject
{
    public Vector3 position; 
    public Vector3 velocity;
    public float height = 0;
    public float ySpeed = 0;

    public float attractionRange =3;
    public float attractionForce = 0.3f;
    public float repulsionRange = 1;
    public float repulsionForce = 4;
    public bool playerAttaction = false;
    public float playerAttactionForce = 0;
    public bool aimForTargetSpeed = false;
    public float targetSpeed = 2; 
    public float targetSpeedLerp = 2;
    public float drag = 2;
    //
    public FluidType type;
    //
    public FluidObject(Vector3 position, Vector3 velocity, FluidType type)
    {
        this.position = position;
        this.velocity = velocity;
        repulsionRange = type.repulsionRange;
        repulsionForce = type.repulsionForce;
        attractionRange = type.attractionRange;
        attractionForce = type.attractionForce;
        playerAttaction = type.playerAttraction;
       playerAttactionForce =type.playerAttractionForce;
        aimForTargetSpeed =type.aimForTargetSpeed;
        targetSpeed = type.targetSpeed * (0.7f + Random.value * 0.6f); 
        targetSpeedLerp = type.targetSpeedLerp; 
        drag = type.drag;
    }
}
public class FluidManager : SingletonBehaviour<FluidManager>
{
    public List<FluidObject> fluids = new List<FluidObject>();
    void Start()
    { 

    }
    public FluidObject AddFluid( Vector3 pos, FluidType type)
    {
        FluidObject fluid = new FluidObject(pos, Vector3.zero, type);
        fluids.Add(fluid);
        return fluid;
    }
    //  
    void Update()
    {
        Vector3 playerPos = PlayerMovement.Instance.GetPosition();
        //
        SimulateFluids(playerPos);
    }

    private void SimulateFluids(Vector3 playerPos)
    {
        float t = Time.deltaTime;
        //
      //  Debug.Log("Update fluids " + fluids.Count);
        //
        for (int i = fluids.Count - 1; i >= 0; i--)
        {
            if (fluids[i].playerAttaction)
            {
                Vector3 playerDiff = (playerPos - fluids[i].position).WithY(0);
                fluids[i].velocity += playerDiff.normalized * t * fluids[i].playerAttactionForce;
            }
            for (int j = 0; j < fluids.Count; j++)
            {
                if (j != i)
                {
                    Vector3 fluidDiff =( fluids[j].position - fluids[i].position).WithY(0);
                    float fluidDist = fluidDiff.magnitude;
                    if (fluidDist < fluids[i].attractionRange)
                    {
                        fluids[i].velocity += (fluidDiff / fluidDist) * fluids[i].attractionForce * (1 - fluidDist / fluids[i].attractionRange) * t;
                    }
                    if (fluidDist < fluids[i].repulsionRange)
                    {
                        float m = fluidDist / fluids[i].repulsionRange;
                        fluids[i].velocity += (fluidDiff / fluidDist) * -fluids[i].repulsionForce * (1 - m * m) * t;
                    }
                }
            }
            fluids[i].velocity *= (1 - t * fluids[i].drag) ;
            if (fluids[i].aimForTargetSpeed)
            {
                fluids[i].velocity = Vector3.Lerp(fluids[i].velocity, fluids[i].velocity.normalized * fluids[i].targetSpeed, t * fluids[i].targetSpeedLerp);
            }
            //
            fluids[i].position += fluids[i].velocity * t;
        }
    }
}
