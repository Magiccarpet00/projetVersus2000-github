using UnityEngine;

public class TargetBullet : MonoBehaviour
{
    public GameObject playerToFocus;
    public Vector2 dirrectionBullet;
    public int damage;
    public float speed;
    public float accuracy; // plus il est proche de 0 plus c'est precis
    public void Start()
    {
        float rngX = Random.Range(-accuracy, accuracy);
        float rngY = Random.Range(-accuracy, accuracy);

        Vector2 target = new Vector2(playerToFocus.transform.position.x + rngX, playerToFocus.transform.position.y + rngY);

        dirrectionBullet = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        dirrectionBullet = dirrectionBullet.normalized;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                                                 new Vector2(transform.position.x + dirrectionBullet.x,
                                                             transform.position.y + dirrectionBullet.y),
                                                 speed * Time.deltaTime);
    }
}
