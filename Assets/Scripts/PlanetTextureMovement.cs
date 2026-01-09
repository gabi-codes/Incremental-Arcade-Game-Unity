using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTextureMovement : MonoBehaviour
{
    public float speed = 1f;          
    public float resetX = -3.5f;        

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
