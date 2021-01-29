using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Collider2D objectCollider;

    Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            StartCoroutine(StartFalling());
    }

    IEnumerator StartFalling()
    {
        yield return new WaitForSeconds(0.5f);
        animator.Play("Base Layer.FlyingPlatformTurnOff");
        yield return new WaitForSeconds(1f);
        animator.enabled = false;//turn off animations
        rb.isKinematic = false;
        objectCollider.enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
