using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Dictionary<Direction, Vector2> allDirection = new Dictionary<Direction, Vector2>();
    public Direction currDir;
    private float lifeTime = 5f; //[DEUS EX FLOTINA BUGYBUG] c'est en augmentant le lifeTime que le bug de coroutine à été resolue
    public float damage;
    private bool hasToutch;

    //[CodeReview?]
    void Start()
    {
        allDirection.Add(Direction.RIGHT, new Vector2(1f, 0f));
        allDirection.Add(Direction.DOWN, new Vector2(0f, -1f));
        allDirection.Add(Direction.LEFT, new Vector2(-1f, 0f));
        allDirection.Add(Direction.UP, new Vector2(0f, 1f));

        StartCoroutine(LifeTime());
    }

    void Update()
    {
        Vector2 newPos = new Vector2(transform.position.x + allDirection[currDir].x, transform.position.y + allDirection[currDir].y);
        
        transform.position = Vector2.MoveTowards(transform.position, newPos, Constants.FANTOM_SHOT_FORCE * Time.deltaTime);
    }

    public System.Collections.IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerHealth>().isInvincible == false)
            {
                if (hasToutch == false)
                {
                    hasToutch = true;
                    StartCoroutine(collision.GetComponent<PlayerHealth>().TakeDamage(allDirection[currDir], damage));
                }                
            }
        }
    }
}
