using UnityEngine;

public class Cloud : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public float growAmount;
    public float growSpeed;

    private float minX, maxX;
    private float minY, maxY;

    public bool dirSwitch;
    public Vector2 randomDir;
    public Vector2 randomDirInvert;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        int rng = Random.Range(0, 4);

        if(rng == 0)
        {
            
        }
        else if(rng == 1)
        {
            spriteRenderer.flipX = true;
        }
        else if (rng == 2)
        {
            spriteRenderer.flipY = true;
        }
        else if (rng == 3)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = true;
        }

        minX = transform.position.x - growAmount;
        maxX = transform.position.x + growAmount;

        minY = transform.position.y - growAmount;
        maxY = transform.position.y + growAmount;

        float rngX = Random.Range(0f, 1f);
        float rngY = Random.Range(0f, 1f);

        randomDir = new Vector2(rngX, rngY);
        randomDir = randomDir.normalized;
        randomDirInvert = new Vector2(-randomDir.x, -randomDir.y);
    }

    void Update()
    {
        if (dirSwitch == false)
        {
            rb.MovePosition(rb.position + randomDir * growSpeed * Time.fixedDeltaTime);
            if(transform.position.x > maxX || transform.position.y > maxY)
            {
                dirSwitch = true;
            }
        }
        else
        {
            rb.MovePosition(rb.position + randomDirInvert * growSpeed * Time.fixedDeltaTime);
            
            if (transform.position.x < minX || transform.position.y < minY)
            {
                dirSwitch = false;
            }
        }

    }
}
