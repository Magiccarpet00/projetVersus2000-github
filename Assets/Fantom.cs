using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantom : MonoBehaviour
{
    public Transform playerToFocus;
    public float speed;  

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerToFocus.position, speed * Time.deltaTime);
    }
}
