using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Collider2D hitbox;

    public AudioClip sound_explosion;
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
            if (clip.name == "explosion") 
            {
                animLength = clip.length;
                // si on étiat propre on quitterait la boucle à ce moment
            }
        }
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
        AudioManager.instance.PlayClipAt(sound_explosion, transform.position);
    }
    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
