using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {           
            GameController.Instance.Player.animator.Play("Base Layer.PlayerKnockback");
            GameController.Instance.Player.playerMovement.SpikesKnockback();
            GameController.Instance.Player.SubtractLife();
        }
    }
}
