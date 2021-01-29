using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Vector2 Velocity;
    private Rigidbody2D rb;
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (GameController.Instance.Player.playerMovement.sr.flipX)
            Velocity = new Vector2(-speed, 0);
        else
            Velocity = new Vector2(speed, 0);
    }

    private void Update()
    {
        rb.velocity = Velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player") && !collision.CompareTag("FallingRock"))
            Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
