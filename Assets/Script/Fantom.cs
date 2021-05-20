using System.Collections;
using UnityEngine;

public class Fantom : MonoBehaviour
{
    public GameObject room;
    public GameObject playerToFocus;
    public float speed;
    public int damage;

    public Animator animator;
    public SpriteRenderer shadow;
    public float timeBeforeInvok; // J'aimerrais bien utiliser avec lanimation au lieux d'une constante
    public float lifeTime;
    public float stopTime;

    public bool isDead;
    public bool isRunning;

    private void Start()
    {        
        StartCoroutine(Invocation());
    }

    public IEnumerator Invocation()
    {        
        yield return new WaitForSeconds(timeBeforeInvok);
        isRunning = true;
        shadow.enabled = true;
        yield return new WaitForSeconds(lifeTime-stopTime);
        isRunning = false;
        animator.SetTrigger("pre_die");
        yield return new WaitForSeconds(stopTime);
        Die();
    }

    private void Update()
    {
        if(isRunning == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerToFocus.transform.position, speed * Time.deltaTime);
        }

        if(GameManager.instance.playersPosition[playerToFocus] != room) // [Code Review] on passe dans update du coup pas opti
        {
            Destroy(gameObject);
        }

        
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;

            // [Code review] suggestion pour d'éventuelles balles en plus (ex: diagonales...) pour pas tout reconstruire
            //for (int i = 0; i < 3; i++)
            //{
            //    GameObject bullet = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Ghost_bullet]) as GameObject, transform.position, Quaternion.identity);
            //    bullet.GetComponent<Bullet>().currDir = Direction.UP;
            //}

            GameObject bullet = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Ghost_bullet]) as GameObject, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().currDir = Direction.UP;
            GameObject bullet2 = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Ghost_bullet]) as GameObject, transform.position, Quaternion.identity);
            bullet2.GetComponent<Bullet>().currDir = Direction.DOWN;
            GameObject bullet3 = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Ghost_bullet]) as GameObject, transform.position, Quaternion.identity);
            bullet3.GetComponent<Bullet>().currDir = Direction.LEFT;
            GameObject bullet4 = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Ghost_bullet]) as GameObject, transform.position, Quaternion.identity);
            bullet4.GetComponent<Bullet>().currDir = Direction.RIGHT;

        }
        
        Destroy(this.gameObject);
        }

    private void OnTriggerEnter2D(Collider2D collision) // ça resemble bcp a la methode dans ennemy [Code Review]
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerHealth>().isInvincible == false)
            {
                Vector2 dir = collision.transform.position - this.transform.position;
                dir = dir.normalized;

                StartCoroutine(collision.GetComponent<PlayerHealth>().TakeDamage(dir, damage));
            }
        }
    }
}



