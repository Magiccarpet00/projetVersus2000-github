using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Animator animator;
    public GameObject atachedEnemy;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Epée") || collision.CompareTag("Explosion") || collision.CompareTag("Range_Attack"))
        {
            animator.SetTrigger("plop");
            if (collision.CompareTag("Range_Attack"))
            {
                GameObject rangeAttackRetrit = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.RangeAttackRetrit]) as GameObject, transform.position, Quaternion.identity);                
            }
        }

        if (collision.CompareTag("Player"))
        {
            if(collision.GetComponent<PlayerCharacter>().character == Character.RED)
            {
                if(collision.GetComponent<PlayerAttack>().isAttacking == true)
                {
                    animator.SetTrigger("plop");
                }
            }
        }
    }

    public void AnimationDestroy() // Elle est appelé dans plop
    {
        atachedEnemy.GetComponent<Enemy>().destroyBubble = true;
        Destroy(this.gameObject);
    }
}
