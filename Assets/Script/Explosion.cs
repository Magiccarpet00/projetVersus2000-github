using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Collider2D hitbox;
    private void Start()
    {
        ActivationHitbox();
        hitbox = this.GetComponent<Collider2D>();
    }

    public void ActivationHitbox()
    {
        Animator animator = this.GetComponentInChildren<Animator>();
        float animLength;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;    //Get Animator controller

        foreach (var clip in ac.animationClips)
        {
            if (clip.name == "explosion") // valeur en dur ;)
            {
                animLength = clip.length;
                // si on étiat propre on quitterait la boucle à ce moment
            }
        }
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }
    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
