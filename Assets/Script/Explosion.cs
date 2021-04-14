using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Collider2D hitbox;
    private void Start()
    {
        StartCoroutine(ActivationHitbox());
    }

    public IEnumerator ActivationHitbox()
    {
        yield return new WaitForSeconds(0.15f);
        hitbox.enabled = true;
        yield return new WaitForSeconds(0.35f);
        Destroy(this.gameObject);
    }
}
