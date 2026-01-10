using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vert : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int value = 1;

    [Header("Forces")]
    [SerializeField] float pushForce = 1.5f;

    [SerializeField] float pullStrength = 1f;     
    [SerializeField] float orbitStrength = 1f;    
    [SerializeField] float orbitDamping = 2f;

    [SerializeField] float rotationSpeed = 180f;

    [Header("Timing")]
    [SerializeField] float delayBeforePull = 0.3f;

    private Vector2 velocity;
    private float timer;
    private bool pulling;

    public bool isActive = true;

    Vector2 center;

    void Start()
    {
        center = Vector2.zero;
    }


    private void Awake()
    {
        isActive = true;

        Vector2 baseDir = ((Vector2)transform.position - center).normalized;
        float angleOffset = Random.Range(-15f, 15f);
        Vector2 rotatedDir = Quaternion.Euler(0f, 0f, angleOffset) * baseDir;
        
        float force = Random.Range(0.75f, pushForce);

        velocity = rotatedDir * force;
    }

    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;

        Vector2 toCenter = (center - (Vector2)transform.position).normalized;

        if (timer < delayBeforePull)
        {
            transform.position += (Vector3)(velocity * Time.deltaTime);
            return;
        }

        Vector2 tangent = new Vector2(-toCenter.y, toCenter.x);

        Vector2 desiredVelocity =
            toCenter * pullStrength +
            tangent * orbitStrength;

        velocity = Vector2.Lerp(
            velocity,
            desiredVelocity,
            Time.deltaTime * orbitDamping
        );

        transform.position += (Vector3)(velocity * Time.deltaTime);
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

}
