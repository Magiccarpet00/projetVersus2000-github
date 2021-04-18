using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Dictionary<Direction, Vector2> allDirection = new Dictionary<Direction, Vector2>();
    public Direction currDir;
    public int _i;
    private float lifeTime = 1f;

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
}
