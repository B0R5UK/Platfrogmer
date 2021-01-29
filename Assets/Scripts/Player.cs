using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator animator;
    public bool canShoot;

    public void Initialize()
    {
        canShoot = false;
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        animator.Play("Base Layer.Appearing");
    }

    public void SubtractLife()
    {
        if (canShoot)
        {
            canShoot = false;
            GameController.Instance.UIController.UpdateShuriken();
        }

        StartCoroutine(PlayerDeath());
    }

    public void PickedUp(PickupType type)
    {
        if (type == PickupType.Shuriken)
        {
            canShoot = true;
            GameController.Instance.UIController.UpdateShuriken();
            return;
        }

        GameController.Instance.AddScore(1);
        GameController.Instance.UIController.UpdateScore();
    }

    IEnumerator PlayerDeath()
    {
        GameController.Instance.AudioController.PlaySound(SoundType.Damage);
        playerMovement.FreezePlayer();
        animator.Play("Base Layer.Disappearing");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        GameController.Instance.ReloadLevel();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            SubtractLife();
    }

}
