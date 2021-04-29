using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour
{
    public float speed = 50;
    public float upSpeed = 100;

    public GameObject rightBullet;
    public GameObject leftBullet;

    public List<AudioClip> AudioClips;
    
    private bool puedoSaltar = false;
    
    private SpriteRenderer sr;
    private Animator animator;
    private Rigidbody2D rb2d;
    private AudioSource audioSource;

    public Text scoreText;

    private int Score = 0;
    private float maxItangibleTime = 1f;
    private float intangibleTime = 0f;


    private float switchColorDelay = .1f;
    private float switchColorTime = 0f;
    

    private bool esIntangible = false;

    private Color originalColor;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); // obtengo el objeto spriterenderer de Player
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        originalColor = sr.color;
        Debug.Log("Mensaje de inicio");

        
       
    }

    // Update is called once per frame
    void Update()
    {

        switchColorTime += Time.deltaTime;
        if (switchColorTime > switchColorDelay)
        {
            SwitchColor();
        }


        scoreText.text = "PUNTAJE: " + Score;
        
        if (Input.GetKeyDown(KeyCode.A))
        {

            if (!sr.flipX)
            {
                var position = new Vector2(transform.position.x + 1.5f, transform.position.y - .5f);
                Instantiate(rightBullet, position, rightBullet.transform.rotation);
            }
            else
            {
                var position = new Vector2(transform.position.x - 2.5f, transform.position.y - .5f);
                Instantiate(leftBullet, position, leftBullet.transform.rotation);
            }
            audioSource.PlayOneShot(AudioClips[1]);
        }


        setIdleAnimation();
        
        if (Input.GetKey(KeyCode.C))
        {
            setSlideAnimation();
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            sr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            sr.flipX = true;
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            rb2d.velocity = Vector2.up * upSpeed;
            puedoSaltar = false;
            audioSource.PlayOneShot(AudioClips[0]);
        }

        if (esIntangible && intangibleTime < maxItangibleTime)
        {
            Debug.Log("Intangible");
            intangibleTime += Time.deltaTime;
            Parpadear();
            DeshabilitarColisionConEnemigo();
        }

        if (intangibleTime >= maxItangibleTime)
        {
            HabilitarColisionConEnemigo();
            intangibleTime = 0;
            esIntangible = false;
            sr.enabled = true;
        }
    }

    private void HabilitarColisionConEnemigo()
    {
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
    
    private void DeshabilitarColisionConEnemigo()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
    }

    private void SwitchColor()
    {
        if(sr.color == originalColor)
            sr.color = Color.green;
        else
            sr.color = originalColor;
        switchColorTime = 0;
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 8)
            puedoSaltar = true;
        
        if(other.gameObject.layer == 9)
            Debug.Log("End Game");

        if (other.gameObject.tag == "Enemy")
        {
            esIntangible = true;
        }
    }

    private void Parpadear()
    {
        sr.enabled = !sr.enabled;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyJump")
            IncrementerPuntajeEn5();
    }

    private void setSlideAnimation()
    {
        animator.SetInteger("Estado", 3);
    }

    private void setRunAnimation()
    {
        animator.SetInteger("Estado", 1);
    }
    
    private void setJumpAnimation()
    {
        animator.SetInteger("Estado", 2);
    }
    
    private void setIdleAnimation()
    {
        animator.SetInteger("Estado", 0);
    }

    public void IncrementerPuntajeEn10()
    {
        Score += 10;
    }
    
    public void IncrementerPuntajeEn5()
    {
        Score += 5;
    }

}
