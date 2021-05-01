using System.Collections;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public int damage;
    public float speed;
    public bool isBlock;

    void Update()
    {
        if (!isBlock)
        {
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y + 1);
            transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            isBlock = true;
            StartCoroutine(AutoDestruction());
        }
    }

    public IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
