using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType {Melon, Shuriken}

public class Pickup : MonoBehaviour
{
    public PickupType myType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.Instance.AudioController.PlaySound(SoundType.PickUp);
            GameController.Instance.Player.PickedUp(myType);
            Destroy(gameObject);
        }
    }
}
