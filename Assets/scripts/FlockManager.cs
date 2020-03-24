using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public static FlockManager Instance;
    private void Awake() {
        if (Instance!= this) Destroy (Instance);
        if (Instance == null) Instance =  this;
    }
    public int boidNum = 100;
    public float boundSize = 5;
    [HideInInspector] public Vector3 goalPos;
    [SerializeField] private float goalRefresh = 5;
    public GameObject[] boids;
    [SerializeField] private GameObject boidPrefab = null;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        boids = new GameObject[boidNum];
        for (int i = 0; i < boidNum; i++)
        {
            GameObject currentBoid = (GameObject) Instantiate (boidPrefab, new Vector3 (
                Random.Range (-boundSize, boundSize),
                Random.Range (-boundSize, boundSize),
                Random.Range (-boundSize, boundSize)
            ), Random.rotation, transform);
            boids[i] = currentBoid;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer+= Time.deltaTime;
        if (timer >= goalRefresh) {
            goalRefresh = Random.Range (1, 5);
            timer = 0;
            goalPos = Random.insideUnitSphere * Random.Range (1, boundSize);
        }
    }
}
