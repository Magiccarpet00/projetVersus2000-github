using System.Collections;
using UnityEngine;

public class Fantom : MonoBehaviour
{
    public Transform playerToFocus;
    public float speed;

    public Animator animator;
    public float timeBeforeInvok;

    public bool isActivated;

    private void Start()
    {
        
        StartCoroutine(Invocation());
    }

    public IEnumerator Invocation()
    {        
        yield return new WaitForSeconds(1f);
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
