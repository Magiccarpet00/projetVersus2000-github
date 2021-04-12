using System.Collections;
using UnityEngine;

public class Fantom : MonoBehaviour
{
    public Transform playerToFocus;
    public float speed;

    public SpriteRenderer spiral;
    public SpriteRenderer realSprite;

    public bool isActivated;

    private void Start()
    {        
        StartCoroutine(Invocation());
    }

    public IEnumerator Invocation()
    {
        GetComponent<SpriteRenderer>().sprite = spiral.sprite;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().sprite = realSprite.sprite;
        isActivated = true;
    }

    private void Update()
    { 
        if(isActivated == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerToFocus.position, speed * Time.deltaTime);
        }        
    }

}
