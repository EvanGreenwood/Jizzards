 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class BasicComputeSpheres : MonoBehaviour
{
    public BasicComputeSpheres bloodController;
    public bool press1ToSpawn = false;
    struct Boid
    {
        public Vector3 position;
        public Vector3 velocity;
        public float variance;
          public int status;
    }
    class Spawning
    {
        public Vector3 position;
        public int count;
    }
    private List<Boid> _boids = new List<Boid>();
    private List<Spawning> _spawnings = new List<Spawning>();
    public int startAmount = 40;
    public int maxAmount = 400;
    public int increment = 10;
    public float centerAttraction = 12;
    public float velocityTargetting = 9.6f;
    public ComputeShader Shader;

    public Enemy enemyPrefab;

    ComputeBuffer resultBuffer;
    int kernel;
    uint threadGroupSize;
    Boid[] output;

    Enemy[] enemyInstances;
    //
    private Vector3 spawningBoidsPoint;
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
        Shader.SetFloat("centerAttraction", centerAttraction); 
        Shader.SetFloat("velocityTargetting", velocityTargetting);
        //spheres we use for visualisation
        enemyInstances = new Enemy[maxAmount];
        for (int i = 0; i < startAmount; i++)
        {
            Enemy enemy  = Instantiate(enemyPrefab, transform) as Enemy;
            enemyInstances[i] = enemy;
            enemyInstances[i].Setup(this,i);
        }
    }

    void Update()
    {
        if (press1ToSpawn && Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnBoids(increment, new Vector3((Random.Range(0, 2) * 2 - 1) * 30, 0, (Random.Range(0, 2) * 2 - 1) * 20));
        }
        //
        if (_spawnings.Count  > 0)
        {
            for (int s = _spawnings.Count - 1; s >= 0; s--)
            {
                _spawnings[s].count--;
                Vector3 pos = _spawnings[s].position;
                //
                for (int i = 0; i < _boids.Count; i++)
                {
                    if (_boids[i].status == 0)
                    {

                        Boid boid = new Boid();
                        boid.position = pos + Random.onUnitSphere.WithY(0) * 0.04f;
                        boid.velocity = Random.onUnitSphere.WithY(0);
                        boid.variance = 0.8f + Random.value * 0.4f;
                        boid.status = 1;
                        _boids[i] = boid;
                        //
                        // resultBuffer.SetData(_boids.ToArray());
                        resultBuffer.SetData(new Boid[1] { boid }, 0, i, 1);
                        //
                        enemyInstances[i] = Instantiate(enemyPrefab, transform);
                        enemyInstances[i].Setup(this, i);
                        //  
                        //
                        _spawningBoids--;
                        break;
                    }
                }
                if (_spawnings[s].count <= 0)
                {
                    _spawnings.RemoveAt(s);
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

        for (int i = 0; i < enemyInstances.Length; i++)
        {
            if (enemyInstances[i] != null && output[i].status > 0)
            {
                enemyInstances[i].SetPositionRotation(output[i].position, output[i].velocity);
              //  instances[i].localPosition = output[i].position;

              //  instances[i].rotation = Quaternion.Lerp(instances[i].rotation, Quaternion.LookRotation(output[i].velocity, Vector3.up), Time.deltaTime * 30);
                //
            }
          //  Debug.Log(" i " + i + " velocity " + output[i].velocity + " instances[i].forward " + instances[i].forward);
            // 
        }
    }
    public void SpawnBoids(int count, Vector3 point)
    {
        Spawning s = new Spawning();
        s.count = count;
        s.position = point;
        _spawnings.Add(s);
    }
    public void KillBoid(int index)
    {
        Boid boid = new Boid();
        boid.position = Vector3.zero;
        boid.velocity = Vector3.zero;
        boid.variance = 1;
        boid.status = 0;
        _boids[index] = boid;
        //
        resultBuffer.SetData(new Boid[1] { boid }, 0, index, 1);
    }
    void OnDestroy()
    {
        resultBuffer.Dispose();
    }
}