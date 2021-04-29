using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class BallController : MonoBehaviour
{
    public float velocityX = 10f;
    private Rigidbody2D rb;
    private PlayerController playerController;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
        
        Destroy(gameObject, 3);
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.right * velocityX;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            playerController.IncrementerPuntajeEn10();
        }
    }
}
