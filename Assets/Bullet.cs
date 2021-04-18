using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Dictionary<int, Vector2> allDirection = new Dictionary<int, Vector2>();
    public int _i;
    private float lifeTime = 1f;

    void Start()
    {
        allDirection.Add(0, new Vector2(1f, 0f));
        allDirection.Add(1, new Vector2(0f, -1f));
        allDirection.Add(2, new Vector2(-1f, 0f));
        allDirection.Add(3, new Vector2(0f, 1f));

        StartCoroutine(LifeTime());
    }

    void Update()
    {
        Vector2 newPos = new Vector2(transform.position.x + allDirection[_i].x, transform.position.y + allDirection[_i].y);

        transform.position = Vector2.MoveTowards(transform.position, newPos, Constants.FANTOM_SHOT_FORCE * Time.deltaTime);
    }

    public System.Collections.IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}
