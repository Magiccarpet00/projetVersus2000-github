using System.Collections;
using UnityEngine;

public class AttackVersus : MonoBehaviour
{
    public GameObject playerToFocus;
    public GameObject player;
    public GameObject currentRoom;


    //RED ATTACK
    public bool redCanMove;
    public Vector2 newPos;
    public float offSetLifeTime;
    public int redDamage;
    public AudioClip sound_RedAttackVersusSpawn;
    public AudioClip sound_BlueAttackVersusSpawn;
    public AudioClip sound_BlueAttackVersusShot;

    private void Start()
    {
        if(player.GetComponent<PlayerCharacter>().character == Character.BLUE)
        {
            StartCoroutine(BlueAttackVersus(playerToFocus, player));
        }

        if (player.GetComponent<PlayerCharacter>().character == Character.RED)
        {
            StartCoroutine(RedAttackVersus(playerToFocus, player));
            StartCoroutine(RedAttackVersusTimer());
        }

        if (player.GetComponent<PlayerCharacter>().character == Character.GREEN)
        {
            GreenAttackVersus();
        }
    }

    private void Update()
    {
        if (redCanMove)
        {
            RedMove(playerToFocus);
        }

        if (GameManager.instance.playersPosition[playerToFocus] != currentRoom) // [Code Review] on passe dans update du coup pas opti
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator BlueAttackVersus(GameObject _playerToFocus, GameObject _player)
    {

        AudioManager.instance.PlayClipAt(sound_BlueAttackVersusSpawn, transform.position);
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
            AudioManager.instance.PlayClipAt(sound_BlueAttackVersusShot, transform.position);

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

    public IEnumerator RedAttackVersus(GameObject _playerToFocus, GameObject _player)
    {
        AudioManager.instance.PlayClipAt(sound_RedAttackVersusSpawn, transform.position);


        while (true)
        {
            float rngTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(rngTime);

            float rngX = Random.Range(-1f, 1f);
            float rngY = Random.Range(-1f, 1f);
            newPos = new Vector2(_playerToFocus.transform.position.x + rngX, playerToFocus.transform.position.y + rngY);
        }

        
    }

    public void RedMove(GameObject _playerToFocus)
    {        
        transform.position = Vector2.MoveTowards(transform.position, newPos, 0.2f * Time.deltaTime);
    }

    public IEnumerator RedAttackVersusTimer()
    {
        yield return new WaitForSeconds(3f); // Le premiere Timer pour attendre que tout se renseigne dans Combo manager 

        float timer = 4f - offSetLifeTime;

        yield return new WaitForSeconds(timer);
        GetComponent<Animator>().SetTrigger("die");
    }


    public void GreenAttackVersus()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerHealth>().isInvincible == false)
            {
                if (player.GetComponent<PlayerCharacter>().character == Character.RED)
                {
                    StartCoroutine(collision.GetComponent<PlayerHealth>().TakeDamage(newPos.normalized, redDamage));
                }
            }                
        }        
    }
}
