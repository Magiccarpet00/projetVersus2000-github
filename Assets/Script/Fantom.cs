using System.Collections;
using UnityEngine;

public class Fantom : MonoBehaviour
{
    public Transform playerToFocus;
    public float speed;

    public Animator animator;
    public float timeBeforeInvok; // J'aimerrais bien utiliser avec lanimation au lieux d'une constante
    public float lifeTime;

    public bool isDead;
    public bool isActivated;

    private void Start()
    {        
        StartCoroutine(Invocation());
    }

    public IEnumerator Invocation()
    {        
        yield return new WaitForSeconds(timeBeforeInvok);
        isActivated = true;
        yield return new WaitForSeconds(lifeTime);
        Die();
    }

    private void Update()
    {
        if(isActivated == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerToFocus.position, speed * Time.deltaTime);
        }
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;

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
    }

