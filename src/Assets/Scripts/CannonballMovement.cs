using UnityEngine;
using System.Collections;

public class CannonballMovement : MonoBehaviour
{
    [SerializeField]
    private float Lifetime;

    private float spawnTime;

    void Awake()
    {
        spawnTime = Time.time;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if ((Time.time - spawnTime) > Lifetime)
        {
            Destroy(gameObject);
        }
    }
}
