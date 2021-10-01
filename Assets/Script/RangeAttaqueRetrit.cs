using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttaqueRetrit : MonoBehaviour
{
    public bool canMove;
    public GameObject playerToFollow;
    public float speed;

    private void Start()
    {
        StartCoroutine(FindPlayerVoid());
    }

    public IEnumerator FindPlayerVoid() //[code review] J'ai bien cette thecnique pour quoi ne pas en faire qqch de générique.
    {
        GameObject detectionPlayer = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Detection_player]) as GameObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f); //comme ça find player a le temps de find
        playerToFollow = detectionPlayer.GetComponent<FindPlayer>().playerFind;
    }

    void Update()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerToFollow.transform.position, speed * Time.deltaTime);
        }   
    }

    public void StartMoveAnimator()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            collision.GetComponent<PlayerInventory>().munitionRangeAttack++;
            //collision.GetComponent<PlayerInventory>().UpdateUI();
        }
    }
}
