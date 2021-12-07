using System.Collections;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public int damage;
    public float speed;
    public Character character;

    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y + 1);
        transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall") || collision.CompareTag("Enemy") || collision.CompareTag("Bubble"))
        {
            animator.SetTrigger("hit");
            speed = 0;
        }
    }
}
