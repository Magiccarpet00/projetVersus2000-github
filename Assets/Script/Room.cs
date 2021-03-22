using UnityEngine;

public class Room : MonoBehaviour
{
    

//-----------------------------------------------------------
    public bool openRight;    
    public bool openLeft;    
    public bool openUp;    
    public bool openDown;
    public void TransformationRoom()
    {
        //Cette methode va rensegné les boolean ci-dessus
        ApertureCheck();

        // 1 ouverture
        {
            // if (openRight && !openDown && !openLeft && !openUp) pour voir la version refactorisée
            if (openRight == true && openDown == false && openLeft == false && openUp == false)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
            }
            if (openRight == false && openDown == true && openLeft == false && openUp == false)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.DOWN.GetComponent<SpriteRenderer>().sprite;
            }
            if (openRight == false && openDown == false && openLeft == true && openUp == false)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.LEFT.GetComponent<SpriteRenderer>().sprite;
            }
            if (openRight == false && openDown == false && openLeft == false && openUp == true)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.UP.GetComponent<SpriteRenderer>().sprite;
            }
        }
        

    }
//-----------------------------------------------------------
    public void ApertureCheck()
    {
        Vector2 rightCheck = new Vector2(transform.position.x + LevelGenerator.instance.offSet, transform.position.y);
        Vector2 leftCheck = new Vector2(transform.position.x - LevelGenerator.instance.offSet, transform.position.y);
        Vector2 upCheck = new Vector2(transform.position.x, transform.position.y + LevelGenerator.instance.offSet);
        Vector2 downCheck = new Vector2(transform.position.x, transform.position.y - LevelGenerator.instance.offSet);

        if (Physics2D.OverlapCircle(rightCheck, Constants.CIRCLE_RADIUS) != null)
        {
            openRight = true;
        }

        if (Physics2D.OverlapCircle(leftCheck, Constants.CIRCLE_RADIUS) != null)
        {
            openLeft = true;
        }

        if (Physics2D.OverlapCircle(upCheck, Constants.CIRCLE_RADIUS) != null)
        {
            openUp = true;
        }

        if (Physics2D.OverlapCircle(downCheck, Constants.CIRCLE_RADIUS) != null)
        {
            openDown = true;
        }
    }



}
