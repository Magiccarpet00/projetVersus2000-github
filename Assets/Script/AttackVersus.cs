using System.Collections;
using UnityEngine;

public class AttackVersus : MonoBehaviour
{
    public GameObject playerToFocus;
    public GameObject player;

    //BLUE
    public Animator animator;

    private void Start()
    {
        if(player.GetComponent<PlayerCharacter>().character == Character.BLUE)
        {
            StartCoroutine(BlueAttackVersus(playerToFocus));
        }

        if (player.GetComponent<PlayerCharacter>().character == Character.RED)
        {
            RedAttackVersus();
        }

        if (player.GetComponent<PlayerCharacter>().character == Character.GREEN)
        {
            GreenAttackVersus();
        }
    }

    public IEnumerator BlueAttackVersus(GameObject _playerToFocus)
    {
        animator = GetComponent<Animator>();
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("fire");
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = Instantiate(player.GetComponent<PlayerCharacter>().blueBullet,
                                            transform.position,
                                            Quaternion.identity);

            TargetBullet targetBullet = bullet.GetComponent<TargetBullet>();
            targetBullet.accuracy = 1f;
            targetBullet.damage = 1;
            targetBullet.speed = 4f;
            targetBullet.playerToFocus = _playerToFocus;
            yield return new WaitForSeconds(0.5f);
        }        

        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("end");
    }

    public void RedAttackVersus()
    {

    }

    public void GreenAttackVersus()
    {

    }

}
