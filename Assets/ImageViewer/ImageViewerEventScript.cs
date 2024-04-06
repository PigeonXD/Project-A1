using UnityEngine;
using UnityEngine.UI;

public class ImageViewerEventScript : MonoBehaviour
{
    [SerializeField] int sortTypeIndex;                         // Decides which set of Images to use
    private Image imageDisplay;                                 // Used to set displayed Images
    private Color transparent = new Color(1, 1, 1, 0);    // Sets the alpha of an object to 0;
    private Color grayedOut = new Color(0.3f, 0.3f, 0.3f, 1);       // Sets the color to gray in order to convey the button being disabled;
    private int spriteArrayLength;                              // Length of the Array with target Images
    private int currentImage = -1;                               // Variable responsible for tracking the current display image
    private GameObject prevB;
    private GameObject nextB;
    private Text pageNumText;

    private Sprite[] spriteArray;
    [SerializeField] Sprite[] selectionSortSpriteArray;

    void Start()
    {
        LoadSprites();
        spriteArrayLength = spriteArray.Length - 1;
        imageDisplay = transform.GetChild(4).GetComponent<Image>();
        imageDisplay.color = transparent;

        prevB = transform.GetChild(2).gameObject;
        nextB = transform.GetChild(1).gameObject;
        pageNumText = GameObject.Find("ImageNum").GetComponent<Text>();
        UpdateText();
        UpdateDisplayImage();
    }

    private void LoadSprites()
    {
        switch (sortTypeIndex)
        {
            case 0: spriteArray = selectionSortSpriteArray; break;
            case 1: break;
            case 2: break;
            case 3: break;
            case 4: break;
            case 5: break;
            case 6: break;
            case 7: break;
            case 8: break;
            case 9: break;
        }
    }

    private void UpdateDisplayImage()
    {
        if (currentImage > -1) imageDisplay.sprite = spriteArray[currentImage];
        if(currentImage == -1)
        {
            imageDisplay.color = transparent;
            prevB.GetComponent<Image>().color = grayedOut;
            prevB.GetComponent<Button>().interactable = false;
        }
        else if (currentImage > -1 && currentImage < spriteArrayLength)
        {
            imageDisplay.color = Color.white;
            nextB.GetComponent<Image>().color = Color.white;
            prevB.GetComponent<Image>().color = Color.white;
            prevB.GetComponent<Button>().interactable = true;
            nextB.GetComponent<Button>().interactable = true;
        }
        else
        {
            nextB.GetComponent<Image>().color = grayedOut;
            nextB.GetComponent<Button>().interactable = false;
        }
    }

    public void NextButton()
    {
        if (currentImage < spriteArrayLength)
        {
            currentImage++;
            UpdateDisplayImage();
        }
        UpdateText();
    }

    public void PrevButton()
    {
        if (currentImage > -1)
        {
            currentImage--;
            UpdateDisplayImage();
        }
        UpdateText();
    }

    private void UpdateText()
    {
        pageNumText.text = (currentImage + 2) + "/" + (spriteArrayLength + 2);
    }

}
