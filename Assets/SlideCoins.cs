using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCoins : MonoBehaviour
{
    private float destinationSlideX;
    private float destinationSlideY;
    private bool onSlide = true;

    void Start()
    {
        destinationSlideX = transform.position.x + Random.Range(-1f, 1f);
        destinationSlideY = transform.position.y + Random.Range(-1f, 1f);

        StartCoroutine(TimeSlide());
    }
    private IEnumerator TimeSlide()
    {
        yield return new WaitForSeconds(0.5f);
        onSlide = false;
    }
    void Update()
    {
        if (onSlide)
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, destinationSlideX, Constants.SLIDE_MOVEMENT),
                                         Mathf.Lerp(transform.position.y, destinationSlideY, Constants.SLIDE_MOVEMENT));
        }        
    }
}
