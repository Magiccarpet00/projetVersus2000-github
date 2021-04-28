using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using static PlayerMovement;

public class PlayerAttack : MonoBehaviour
{
    public bool isAttacking;
    public bool isBufferingAttack;

    public Animator animator;

    Dictionary<InputBufferDirection, InfoAttack> directionOffSet_And_Rotation = new Dictionary<InputBufferDirection, InfoAttack>();
    public GameObject rangeAttackPrefab;
    public GameObject epeePrefab;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerInput playerInput;

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

    public void Awake()
    {
        // on regarde dans quelle dirrection est l'input buffer
        directionOffSet_And_Rotation[InputBufferDirection.UP] = new InfoAttack(new Vector2(0f, Constants.OFFSET_ATTACK), 90f,Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.LEFT] = new InfoAttack(new Vector2(-Constants.OFFSET_ATTACK,0f), 180f, Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.DOWN] = new InfoAttack(new Vector2(0f, -Constants.OFFSET_ATTACK), 270f, Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.RIGHT] = new InfoAttack(new Vector2(Constants.OFFSET_ATTACK, 0f), 0f, Constants.SECOND_ATTACK_CD);        
    }    
    
    void Update()
    {   //ATTACK
        if (Input.GetButton(playerInput.button0) 
            && !isAttacking 
            && !playerHealth.isInvincible 
            && !playerHealth.dead
            && !playerMovement.isBetweenRooms)
        {
            //[Annimation]
            animator.SetBool("Button_Down", true);

            isBufferingAttack = true;
            playerMovement.checkSwitchBoxMove("isBufferingAttack", isBufferingAttack);
        }

        else if (Input.GetButtonUp(playerInput.button0)
                && !isAttacking 
                && !playerHealth.isInvincible 
                && !playerHealth.dead
                && !playerMovement.isBetweenRooms) // isInvincible c'est quand on se fait touché donc c'est ptet bof comme nom...
        {
            isBufferingAttack = false;
            playerMovement.checkSwitchBoxMove("isBufferingAttack", isBufferingAttack);
            //[Annimation]
            animator.SetBool("Button_Down", false);

            //Lance l'attaque
            isAttacking = true;
            playerMovement.checkSwitchBoxMove("isAttacking", isAttacking);

            StartCoroutine(Attack()); // C'est bof d'après félix            
        }

        // RANGE ATTACK
        if (Input.GetButtonDown(playerInput.button1)
            && !isAttacking
            && !playerHealth.isInvincible
            && !playerHealth.dead
            && !playerMovement.isBetweenRooms)
        {
            RangeAttaque();
        }
    }

    public IEnumerator Attack()
    {
        InfoAttack infoAttack = directionOffSet_And_Rotation[playerMovement.InputBuffer];
        GameObject epee = Instantiate(epeePrefab, transform.position + transform.TransformDirection(infoAttack.OffsetAttack), infoAttack.RotatiisAttacking);
        epee.GetComponent<Sword>().player = this.gameObject; // Pour dire à qui appartient cette épee;

        epee.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(infoAttack.DelayAttack);
        
        Destroy(epee);

        isAttacking = false;
        playerMovement.checkSwitchBoxMove("isAttacking", isAttacking);
        
    }

    // RANGE ATTACK

    public void RangeAttaque()
    {
        Instantiate(rangeAttackPrefab, transform.position, Quaternion.identity);
    }

}