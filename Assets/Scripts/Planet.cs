using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed = 1f;          // prêdkoœæ przesuwania
    public float resetX = -3.5f;        // po jakiej odleg³oœci resetujemy

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= resetX)
        {
            transform.position = new Vector2(6.5f, 0f);
        }
    }
}
