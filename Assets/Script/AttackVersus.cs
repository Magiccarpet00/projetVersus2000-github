using System.Collections;
using UnityEngine;

public class AttackVersus : MonoBehaviour
{
    public GameObject playerToFocus;
    public GameObject player;

    private void Start()
    {
        if(player.GetComponent<PlayerCharacter>().character == Character.BLUE)
        {
            StartCoroutine(BlueAttackVersus(playerToFocus, player));
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

    public IEnumerator BlueAttackVersus(GameObject _playerToFocus, GameObject _player)
    {
        PlayerCharacter playerCharacter = _player.GetComponent<PlayerCharacter>();
        Animator animator = GetComponent<Animator>();
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("fire");
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < playerCharacter.blueCountBullet; i++)
        {
            GameObject bullet = Instantiate(player.GetComponent<PlayerCharacter>().blueBullet,
                                            transform.position,
                                            Quaternion.identity);

            TargetBullet targetBullet = bullet.GetComponent<TargetBullet>();
            targetBullet.accuracy = playerCharacter.blueAccuracyBullet;
            targetBullet.damage = playerCharacter.blueDamageBullet;
            targetBullet.speed = playerCharacter.blueSpeedBullet;
            targetBullet.playerToFocus = _playerToFocus;
            targetBullet.bulletBehaviour = BulletBehaviour.TARGET;
            yield return new WaitForSeconds(playerCharacter.blueIntervalSpeedBullet);
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
