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

    // Description Text Variables
    private GameObject textCol;

    // Sprite Variables
    [SerializeField] Sprite[] spriteArray;

    void Start()
    {
        // Load Description Text
        textCol = transform.GetChild(5).gameObject;
        // The Rest Is in UpdateText(); ------------------------------------
                                                                        // |
        // Load Description Images                                      // | 
        spriteArrayLength = spriteArray.Length - 1;                     // |
        imageDisplay = transform.GetChild(4).GetComponent<Image>();     // |
        imageDisplay.color = transparent;                               // |
                                                                        // |
        // Load UI                                                      // |
        prevB = transform.GetChild(2).gameObject;                       // |
        nextB = transform.GetChild(1).gameObject;                       // |
        pageNumText = transform.GetChild(3).GetComponent<Text>();       // |
        UpdateText(); // <--------------------------------------------------
        UpdateDisplayImage();
    }

    private void UpdateDisplayImage()
    {
        if(spriteArrayLength == -1)
        {
            prevB.GetComponent<Image>().color = grayedOut;
            nextB.GetComponent<Image>().color = grayedOut;
            nextB.GetComponent<Button>().interactable = false;
            prevB.GetComponent<Button>().interactable = false;
        }

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

        for (int i = 0; i < textCol.transform.childCount; i++)
        {
            textCol.transform.GetChild(i).gameObject.SetActive(false);
        }
        textCol.transform.GetChild(currentImage + 1).gameObject.SetActive(true);
    }
}
