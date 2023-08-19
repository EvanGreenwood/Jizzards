 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicComputeSpheres : MonoBehaviour
{
    struct Boid
    {
        public Vector3 position;
        public Vector3 velocity;
    }
    public int SphereAmount = 17;
    public ComputeShader Shader;

    public GameObject Prefab;

    ComputeBuffer resultBuffer;
    int kernel;
    uint threadGroupSize;
    Boid[] output;

    Transform[] instances;

    void Start()
    {
        //program we're executing
        kernel = Shader.FindKernel("CalculateVelocities");
        Shader.GetKernelThreadGroupSizes(kernel, out threadGroupSize, out _, out _);

        List<Boid> boids = new List<Boid>();
        for (int i = 0; i < SphereAmount; i++)
        {
            Boid boid = new Boid();
            boid.position = new Vector3(-1 + Random.value * 20, 0.1f + Random.value * 2, Random.value *  12);
            boid.velocity = Random.onUnitSphere;
            boids.Add(boid);
        }

        //buffer on the gpu in the ram
        resultBuffer = new ComputeBuffer(SphereAmount, sizeof(float) * 6);
        output = new Boid[SphereAmount];
        resultBuffer.SetData(boids.ToArray());
        Shader.SetInt("N", SphereAmount);

        //spheres we use for visualisation
        instances = new Transform[SphereAmount];
        for (int i = 0; i < SphereAmount; i++)
        {
            instances[i] = Instantiate(Prefab, transform).transform;
        }
    }

    void Update()
    {
        Shader.SetFloat("Time", Time.deltaTime);
        Shader.SetBuffer(kernel, "Result", resultBuffer);
        int threadGroups = (int)((SphereAmount + (threadGroupSize - 1)) / threadGroupSize);
        Shader.Dispatch(kernel, threadGroups, 1, 1);
        resultBuffer.GetData(output);

        for (int i = 0; i < instances.Length; i++)
            instances[i].localPosition = output[i].position;
    }

    void OnDestroy()
    {
        resultBuffer.Dispose();
    }
}