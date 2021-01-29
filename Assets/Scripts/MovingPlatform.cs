using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform position1;
    [SerializeField] private Transform position2;
    [SerializeField] private float movingSpeed;
    private Vector3 nextPosition;
    void Start()
    {
        nextPosition = position1.position;
    }

    void Update()
    {
        if (transform.position == position1.position)
        {
            nextPosition = position2.position;
        }
        if (transform.position == position2.position)
        {
            nextPosition = position1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, movingSpeed * Time.deltaTime);       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GameController.Instance.Player.playerMovement.StickPlayerToPlatform(transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GameController.Instance.Player.playerMovement.UnStickPlayerFromPlatform();
    }
}
