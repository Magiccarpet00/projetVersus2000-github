using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class PlayerAttack : MonoBehaviour //C'est plus vraiment player attaque c'est player Ability  
{
    public bool isAttacking;
    public bool onCoolDown;

    //BLUE Varriable
    public bool isBufferingAttack;
    public bool isDashing;
    Dictionary<InputBufferDirection, InfoAttack> directionOffSet_And_Rotation = new Dictionary<InputBufferDirection, InfoAttack>();

    public Animator animator;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerInput playerInput;
    public PlayerInventory playerInventory;
    public PlayerCharacter playerCharacter;

    public void Awake()
    {
        // on regarde dans quelle dirrection est l'input buffer
        directionOffSet_And_Rotation[InputBufferDirection.UP] = new InfoAttack(new Vector2(0f, Constants.OFFSET_ATTACK), 90f,Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.LEFT] = new InfoAttack(new Vector2(-Constants.OFFSET_ATTACK,0f), 180f, Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.DOWN] = new InfoAttack(new Vector2(0f, -Constants.OFFSET_ATTACK), 270f, Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.RIGHT] = new InfoAttack(new Vector2(Constants.OFFSET_ATTACK, 0f), 0f, Constants.SECOND_ATTACK_CD);        
    }    
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        playerInput = GetComponent<PlayerInput>();
        playerInventory = GetComponent<PlayerInventory>();
        animator = GetComponentInChildren<Animator>();
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    public struct InfoAttack
    {
        public InfoAttack(Vector2 offsetAttack, float rotatiisAttacking, float delayAttack)
        {
            OffsetAttack = offsetAttack;
            RotatiisAttacking = Quaternion.Euler(0f, 0f, rotatiisAttacking);
            DelayAttack = delayAttack;
        }

        public Vector2 OffsetAttack { get; set; }
        public Quaternion RotatiisAttacking { get; set; }
        public float DelayAttack { get; set; }
    }
    
    void Update()
    {   // Bouton A enfoncer
        if (Input.GetButton(playerInput.button0) 
            && !isAttacking 
            && !onCoolDown
            && !playerHealth.isInvincible 
            && !playerHealth.dead
            && !playerMovement.isBetweenRooms)
        {
            if(playerCharacter.character == Character.BLUE)
            {
                BluePreCloseAttack();
            }

            else if(playerCharacter.character == Character.RED)
            {
                StartCoroutine(RedCloseAttack());
            }

            else if (playerCharacter.character == Character.GREEN)
            {

            }
        }

        // Bouton A relacher
        else if (Input.GetButtonUp(playerInput.button0)
                && !isAttacking 
                && !playerHealth.isInvincible 
                && !playerHealth.dead
                && !playerMovement.isBetweenRooms)
        {
             if(playerCharacter.character == Character.BLUE)
             {
                    StartCoroutine(BlueCloseAttack());
             }

             else if(playerCharacter.character == Character.RED)
             {

             }

             else if (playerCharacter.character == Character.GREEN)
             {

             }
        }

        // Bouton B enfoncer
        if (Input.GetButtonDown(playerInput.button1)
            && !isAttacking
            && !playerHealth.isInvincible
            && !playerHealth.dead
            && !playerMovement.isBetweenRooms
            && playerInventory.munitionRangeAttack > 0
            )
        {
            if (playerCharacter.character == Character.BLUE)
            {
                BlueRangeAttaque();
            }

            else if (playerCharacter.character == Character.RED)
            {
                StartCoroutine(RedRangeAttack());
            }

            else if (playerCharacter.character == Character.GREEN)
            {

            }
        }

        // Bouton X Enfoncer 
        if (Input.GetButtonDown(playerInput.button2)
            && !isAttacking
            && !playerHealth.isInvincible
            && !playerHealth.dead
            && !playerMovement.isBetweenRooms
            && !isDashing)
        {
            if (playerCharacter.character == Character.BLUE)
            {
                StartCoroutine(BlueDash());
            }

            else if (playerCharacter.character == Character.RED)
            {

            }

            else if (playerCharacter.character == Character.GREEN)
            {

            }
        }
    }

    // BLUE COMPETENCE
    public void BluePreCloseAttack()
    {
        animator.SetBool("Button_Down", true);

        isBufferingAttack = true;
        playerMovement.checkSwitchBoxMove("isBufferingAttack", isBufferingAttack);
    }
    public IEnumerator BlueCloseAttack()
    {
        isBufferingAttack = false;
        playerMovement.checkSwitchBoxMove("isBufferingAttack", isBufferingAttack);
        //[Annimation]
        animator.SetBool("Button_Down", false);

        //Lance l'attaque
        isAttacking = true;
        playerMovement.checkSwitchBoxMove("isAttacking", isAttacking);

        InfoAttack infoAttack = directionOffSet_And_Rotation[playerMovement.InputBuffer];
        GameObject closeAttack = Instantiate(playerCharacter.closeAttackPrefab,
                                             transform.position + transform.TransformDirection(infoAttack.OffsetAttack),
                                             infoAttack.RotatiisAttacking);

        closeAttack.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(infoAttack.DelayAttack);

        isAttacking = false;
        playerMovement.checkSwitchBoxMove("isAttacking", isAttacking);
    }    
    public void BlueRangeAttaque()
    {
        animator.SetTrigger("flip");
        Vector2 posRangeAttaque = new Vector2(transform.position.x, transform.position.y + 0.5f);
        GameObject rangeAttack = Instantiate(playerCharacter.rangeAttackPrefab, posRangeAttaque, Quaternion.identity);

        float speedTime = 1f;
        float speedStrenght = 0.6f;
        int smoothsness = 2;
        StartCoroutine(SpeedUp(speedTime, speedStrenght, smoothsness)); // [CodeReview] Je sais pas ou mettre les caracteristique des competence je trouve que dans constante c'est bizzare

        playerInventory.munitionRangeAttack--;
        //playerInventory.UpdateUI();
    }
    public IEnumerator BlueDash()
    {
        float dashTime = 0.2f;
        float dashStrenght = 4f;
        int smoothsness = 4;
        isDashing = true;
        animator.SetTrigger("dash");
        StartCoroutine(SpeedUp(dashStrenght, dashTime, smoothsness));
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }

    // RED COMPETENCE
    public IEnumerator RedCloseAttack()
    {
        onCoolDown = true;
        isAttacking = true;
        animator.SetTrigger("close_attack");

        //Size hit box --UP--
        CircleCollider2D hitbox = GetComponent<CircleCollider2D>();
        float oldRadius = hitbox.radius;
        Vector2 oldOffset = hitbox.offset;

        hitbox.radius = playerCharacter.redAttackHitbox.radius;
        hitbox.offset = playerCharacter.redAttackHitbox.offset;        

        //Dash
        float dashTime = 0.35f;
        float dashStrenght = 5f;
        int smoothness = 1;

        StartCoroutine(SpeedUp(dashStrenght, dashTime, smoothness));

        yield return new WaitForSeconds(dashTime);

        //Size hit box --DOWN--
        hitbox.radius = oldRadius;
        hitbox.offset = oldOffset;

        isAttacking = false;
        yield return new WaitForSeconds(0.15f);
        onCoolDown = false;
    }

    public void RedCloseAttackRetrit()
    {
        StartCoroutine(playerMovement.Bumping(-playerMovement.movement));
    }

    public IEnumerator RedRangeAttack()
    {
        Dictionary<int, Vector2> localDir = new Dictionary<int, Vector2>();
        localDir.Add(0, Vector2.right);
        localDir.Add(1, Vector2.down);
        localDir.Add(2, Vector2.left);
        localDir.Add(3, Vector2.up);

        Dictionary<int, float> localRot = new Dictionary<int, float>();
        localRot.Add(0, 0f);
        localRot.Add(1, 270f);
        localRot.Add(2, 180f);
        localRot.Add(3, 90f);

        animator.SetTrigger("range_attack");
        yield return new WaitForSeconds(0.66f);
        for (int i = 0; i < 4; i++)
        {
            GameObject rangeAttack = Instantiate(playerCharacter.redRangeAttack, transform.position, Quaternion.Euler(0f, 0f, localRot[i]));
            TargetBullet bullet = rangeAttack.GetComponent<TargetBullet>();

            bullet.bulletBehaviour = BulletBehaviour.DIRECTIONAL;
            bullet.speed = playerCharacter.redSpeedBullet;
            bullet.damage = playerCharacter.redDamageBullet;
            bullet.dirrectionBullet = localDir[i];
            bullet.player = this.gameObject;
        }
    }

    // GREEN COMPETENCE


    // UTILITAIRE
    public IEnumerator SpeedUp(float buffSpeed, float buffTime, int smoothness)
    {
        playerMovement.maxMoveSpeed += buffSpeed;

        for (int i = 0; i < smoothness; i++)
        {
            yield return new WaitForSeconds(buffTime / smoothness);
            playerMovement.maxMoveSpeed -= buffSpeed / smoothness;
        }
    }
}