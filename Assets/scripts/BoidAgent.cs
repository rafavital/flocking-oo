using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAgent : MonoBehaviour
{
    public float speed = 10;
    [SerializeField] private float cohesionDist = 2;
    [SerializeField] private float avoidanceDist = 1;
    [SerializeField] private float rotationSpeed = 1;
    private GameObject[] mBoids;
    
    [SerializeField] private int neighbourCount;
    private bool returning = false;

    void Start()
    {
        mBoids = FlockManager.Instance.boids;
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = new Color (1,1,1,0.1f);
    //     Gizmos.DrawSphere (transform.position, cohesionDist);
    // }
    
    void Update()
    {
        if (Vector3.Distance (transform.position, Vector3.zero) >= FlockManager.Instance.boundSize) returning = true;
        else returning = false;

        if (returning) {
            Vector3 distance = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (distance), rotationSpeed * Time.deltaTime);
        } else {
            if (Random.Range (0,5) < 1) ApplyRules ();
            ApplyRules ();
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }

    private void ApplyRules () {
        Vector3 avoidDir = Vector3.zero;
        Vector3 stirDir = Vector3.zero;
        Vector3 cohesionDir = Vector3.zero;
        Vector3 avgForward = Vector3.zero;

        float distance = 0;
        float avgSpeed = 0.1f;
        neighbourCount = 0;
        
        Vector3 goalPos = FlockManager.Instance.goalPos;

        foreach (var boid in mBoids)
        {
            if (boid != gameObject) {

                distance = Vector3.Distance (boid.transform.position, transform.position);
            
                if (distance <= cohesionDist) {
                    neighbourCount ++;  
                    Debug.DrawLine (transform.position, boid.transform.position, new Color (1,1,1,0.5f));
                    cohesionDir += boid.transform.position;
                    avgForward += boid.transform.forward;
                    if (distance <= avoidanceDist) {
                        avoidDir += transform.position - boid.transform.position;
                    }

                    avgSpeed += boid.GetComponent <BoidAgent> ().speed;
                } 
            }
        }

        if (neighbourCount > 0) {

            cohesionDir = cohesionDir/neighbourCount + (goalPos - transform.position);
            speed = avgSpeed / neighbourCount;
            speed = Mathf.Clamp (speed, 0, 10);
            stirDir = (cohesionDir + avoidDir * 1000 + avgForward) - transform.position;
            if (stirDir != Vector3.zero) {
                transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (stirDir), Time.deltaTime * rotationSpeed);
            }

        }

    }
}
