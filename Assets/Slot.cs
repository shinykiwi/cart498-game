using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private int id;
    private bool occupied = false;
    private Image image;
    private readonly Color beige = new Color(204, 168, 122, 81);

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void Fill(Sprite sprite)
    {
        if (image)
        {
            image.sprite = sprite;
            occupied = true;
            image.color = Color.white;
        }
        else
        {
            Debug.Log("Error: No image object found in Slot " + id);
        }
        
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
        image.sprite = null;
        image.color = beige;
    }
}
