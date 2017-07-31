using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Tooltip("Velocity enemy")]
    [Range(0.8F, 10F)]
    public float speed;

    private AudioSource source;
    [Tooltip("Sond when enemy is clicked")]
    public AudioClip audioClicked;
    [Tooltip("Sound when the enemy collides with the player")]
    public AudioClip audioCollider;

    private float seconds;
    private float minuts;


    public Animator anim;

    public GameObject gameController;

    private Transform target;


    private void Start()
    {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0];
        source = GetComponent<AudioSource>();
        seconds = gameController.GetComponent<GameController>().seconds;
        minuts = gameController.GetComponent<GameController>().minuts;
    }

    void Update()
    {
        Moviment();

        SpeedTime();
    }

    private void SpeedTime()
    {
        if (seconds <= 10 && minuts == 0)
            speed = 0.8f;
        else if (seconds > 10 && seconds <= 45 && minuts == 0)
            speed = 1.5f;
        else if (seconds <= 10 && minuts == 1)
            speed = 2.0f;
        else if (seconds > 10 && seconds <= 45 && minuts < 2 && minuts > 0)
            speed = 2.5f;
        else if (seconds <= 10 && minuts < 3 && minuts > 1)
            speed = 3.5f;
        else if (seconds > 10 && seconds <= 45 && minuts < 3 && minuts > 1)
            speed = 5.0f;
        else
            speed = 5.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.GetComponent<GameController>().DownBattery(1);

            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            anim = player.GetComponent<Animator>();
            anim.Play("DamageAnimation");
            source.clip = audioCollider;
            source.PlayOneShot(audioCollider, 1F);
            Destroy(gameObject);
        }

    }

    private void OnMouseDown()
    {
        UnityEngine.Debug.Log("Clicked!!");
        source.clip = audioClicked;
        source.PlayOneShot(audioClicked, 1F);
        DestroyObject(gameObject);
    }

    private void Moviment()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime / transform.localScale.x);
    }
}
