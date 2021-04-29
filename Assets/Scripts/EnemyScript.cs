using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.left * 20;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "EnemiesCleaner")
            Destroy(this.gameObject);
    }
}
