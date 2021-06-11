using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Prefab dynamique")]
    public GameObject closeAttackPrefab;
    public GameObject rangeAttackPrefab;
    public GameObject versusAttackPrefab;

    public Character character;

    //Je sais pas trop ou le mettre
    public int goAttackVersus = 0;
    // 0 => on attend dans la coroutine AttackVersus de ComboManager
    // 1 => on lance l'attack Versus de l'autre sur lui même


    // BLUE ATRIBUE
    [Header(" --BLUE-- ")]
    public GameObject blueSprite; // Le sprite contient l'animator, les animation et l'ombre

    public GameObject blueCloseAttack;
    public GameObject blueRangeAttack;
    public GameObject blueVersusAttack;

    public GameObject blueBullet;
    public int blueDamageBullet;
    public float blueAccuracyBullet;
    public float blueSpeedBullet;
    public float blueIntervalSpeedBullet;
    public int blueCountBullet;

    // RED ATRIBUE
    [Header(" --RED-- ")]
    public GameObject redSprite;
    public CircleCollider2D redAttackHitbox;
    public GameObject redRangeAttack;
    public int redDamageBullet;
    public float redSpeedBullet;


    public GameObject redVersusAttack;
    public int redVersusAttackCount;
    public float redVersusAttackSpeed;
    public int redVersusAttackDamage;
    


    private void Awake() // Ici j'utilise Awake sinon les ref au animator dans les autres script non pas le temps de s'executer dans start
    {
        if(character == Character.BLUE)
        {
            GameObject sprite = Instantiate(blueSprite, transform.position, Quaternion.identity);
            sprite.transform.parent = this.transform;

            closeAttackPrefab = blueCloseAttack;
            rangeAttackPrefab = blueRangeAttack;
            versusAttackPrefab = blueVersusAttack;
        }

        else if(character == Character.RED)
        {
            GameObject sprite = Instantiate(redSprite, transform.position, Quaternion.identity);
            sprite.transform.parent = this.transform;
            rangeAttackPrefab = redRangeAttack;
            versusAttackPrefab = redVersusAttack;
        }
    }

}
