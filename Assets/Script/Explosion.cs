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
        /*
         * [CODE REVIEW] Comment retrouver la durée d'exécution d'une animation
         * Félix est incapable de trouver ça tout seul du coup je vais l'aider "j'ai cherché et j'ai pas trouvé jte jure" (mon oeil)
         */

        yield return new WaitForSeconds(0.15f); // have you heard about Constants? 
        hitbox.enabled = true; // pour pas buter instant les autres mobs
        yield return new WaitForSeconds(0.35f);
        Destroy(this.gameObject);
    }
}
