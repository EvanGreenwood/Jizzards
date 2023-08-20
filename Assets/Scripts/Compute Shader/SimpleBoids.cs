 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class BasicComputeSpheres : MonoBehaviour
{
    struct Boid
    {
        public Vector3 position;
        public Vector3 velocity;
        public float variance;
          public int status;
    }
   private List<Boid> _boids = new List<Boid>();
    public int startAmount = 40;
    public int maxAmount = 400;
    public int increment = 10;
    public ComputeShader Shader;

    public GameObject Prefab;

    ComputeBuffer resultBuffer;
    int kernel;
    uint threadGroupSize;
    Boid[] output;

    Transform[] instances;
    //
    private int _spawningBoids = 0;

    void Start()
    {
        //program we're executing
        kernel = Shader.FindKernel("CalculateVelocities");
        Shader.GetKernelThreadGroupSizes(kernel, out threadGroupSize, out _, out _);

      
        for (int i = 0; i < maxAmount; i++)
        {
            Boid boid = new Boid();
            boid.position = new Vector3(-20 + Random.value * 30, 0.1f + Random.value * 2, -10 + Random.value *  20);
            boid.velocity = Random.onUnitSphere;
            boid.variance = 0.8f + Random.value * 0.4f;
              boid.status = i <= startAmount ? 1: 0;
            _boids.Add(boid);
        }

        //buffer on the gpu in the ram
        resultBuffer = new ComputeBuffer(maxAmount, sizeof(float) * 7  + sizeof(int)  );
        output = new Boid[maxAmount];
        resultBuffer.SetData(_boids.ToArray());
        Shader.SetInt("N", maxAmount);

        //spheres we use for visualisation
        instances = new Transform[maxAmount];
        for (int i = 0; i < startAmount; i++)
        {
            instances[i] = Instantiate(Prefab, transform).transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
            _spawningBoids = increment; 
        }
        //
        if (_spawningBoids > 0)
        {
            for (int i = 0; i < _boids.Count; i++)
            {
                if (instances[i]  == null )
                {

                    Boid boid = new Boid();
                    boid.position = new Vector3((Random.Range(0, 2) * 2 - 1) * 40 + Random.value *8.1f, 0, (Random.Range(0, 2) * 2 - 1) * 20 + Random.value * 8.1f);
                    boid.velocity = Random.onUnitSphere.WithY(0);
                    boid.variance = 0.8f + Random.value * 0.4f;
                     boid.status = 1;
                    _boids[i] = boid;
                    //
                   // resultBuffer.SetData(_boids.ToArray());
                      resultBuffer.SetData(new Boid[1] { boid }, 0, i, 1);
                    //
                    instances[i] = Instantiate(Prefab, transform).transform;
                    //
                    Shader.SetInt("N", _boids.Count);
                    Debug.Log(" Boids count " + _boids.Count);
                    //
                    _spawningBoids--;
                    break;
                }
            }
        }
        //
        Shader.SetFloat("Time", Time.deltaTime);
        Shader.SetVector("Center", PlayerMovement.Instance.transform.position);
        Shader.SetBuffer(kernel, "Result", resultBuffer);
        int threadGroups = (int)((maxAmount + (threadGroupSize - 1)) / threadGroupSize);
        Shader.Dispatch(kernel, threadGroups, 1, 1);
        resultBuffer.GetData(output);

        for (int i = 0; i < instances.Length; i++)
        {
            if (instances[i] != null)
            {
                instances[i].localPosition = output[i].position;

                instances[i].rotation = Quaternion.Lerp(instances[i].rotation, Quaternion.LookRotation(output[i].velocity, Vector3.up), Time.deltaTime * 30);
                //
            }
          //  Debug.Log(" i " + i + " velocity " + output[i].velocity + " instances[i].forward " + instances[i].forward);
            // 
        }
    }

    void OnDestroy()
    {
        resultBuffer.Dispose();
    }
}