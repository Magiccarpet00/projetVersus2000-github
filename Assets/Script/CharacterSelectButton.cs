using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    public Character character;
    public Image image;

    public Image blueImage;
    public Image redImage;
    public Image greenImage;

    void Start()
    {
        image = GetComponent<Image>();

        if(character == Character.BLUE)
        {
            image.sprite = blueImage.sprite;
        }
        else if(character == Character.RED)
        {
            image.sprite = redImage.sprite;
        }
        else if (character == Character.GREEN)
        {
            image.sprite = greenImage.sprite;
        }
    }

    
    void Update()
    {
        
    }
}
