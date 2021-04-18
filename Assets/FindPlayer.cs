using System.Collections;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    public GameObject playerFind;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerFind = collision.gameObject;
        }
    }

    private void Start()
    {
        StartCoroutine(Autodestruction());
    }

    public IEnumerator Autodestruction()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
