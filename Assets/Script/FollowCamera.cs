using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform newPos;
    public float timeOffSet;
    public Vector3 posOffSet;

    private Vector3 velocity;

    //Pour 2 joueurs
    public GameObject playerToFollow;    

    private void Update()
    {
         newPos = GameManager.instance.playersPosition[playerToFollow].GetComponent<Transform>();
         transform.position = Vector3.SmoothDamp(transform.position, newPos.position + posOffSet, ref velocity, timeOffSet);                
    }
    
}
