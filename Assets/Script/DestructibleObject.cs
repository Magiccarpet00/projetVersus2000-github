using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public SpriteRenderer[] sprite;
    public Animator animator;
    public BoxCollider2D boxCollider;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Epée") || collision.CompareTag("Explosion"))
        {
            for (int i = 0; i < sprite.Length; i++) // Desactiver les sprites de la caisse statique
            {
                sprite[i].enabled = false;
            }

            animator.SetTrigger("Destroy");
            boxCollider.enabled = false;
        }
    }
}
