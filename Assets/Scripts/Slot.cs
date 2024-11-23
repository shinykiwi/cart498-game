using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private int id;
    private bool occupied = false;
    private Image image;
    private Image buttonImage;
    private Outline outline;
    private readonly Color beige = new Color(204, 168, 122, 81);

    private void Start()
    {
        buttonImage = GetComponentsInChildren<Image>()[0];
        image = GetComponentsInChildren<Image>()[1]; // Gets the nested image instead
        outline = GetComponent<Outline>();

        image.enabled = false;
    }

    public void Fill(Sprite sprite)
    {
        if (image)
        {
            image.enabled = true;
            image.sprite = sprite;
            occupied = true;
        }
        else
        {
            Debug.Log("Error: No image object found in Slot " + id);
        }
        
    }

    public void ToggleSelect()
    {
        buttonImage.fillCenter = !buttonImage.fillCenter;
        outline.enabled = !outline.enabled;
    }

    public int GetID()
    {
        return id;
    }

    public bool IsFull()
    {
        return occupied;
    }
    
    public override string ToString()
    {
        return "Slot " + id;
    }

    public void Clear()
    {
        occupied = false;
        image.enabled = false;
    }
}
