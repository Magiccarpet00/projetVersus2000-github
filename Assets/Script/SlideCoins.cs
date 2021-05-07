using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCoins : MonoBehaviour
{
    private float destinationSlideX;
    private float destinationSlideY;
    private bool onSlide = true;

    private float slideOffSet = 0.5f;
    void Start()
    {
        destinationSlideX = transform.position.x + Random.Range(-slideOffSet, slideOffSet);
        destinationSlideY = transform.position.y + Random.Range(-slideOffSet, slideOffSet);

        StartCoroutine(TimeSlide());
    }
    private IEnumerator TimeSlide()
    {
        yield return new WaitForSeconds(0.2f);
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
