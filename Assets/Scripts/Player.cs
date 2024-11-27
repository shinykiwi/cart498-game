using System.Linq;
using Cinemachine;
using Descant.Components;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField] private HUD hud;
    [SerializeField] private LayerMask layer;
    [SerializeField] private CinemachineVirtualCamera npcLookAtCamera;
    [SerializeField] private int rayDistance = 5;
    
    private RaycastHit hitData; 
    private FirstPersonController movement;
    private StarterAssetsInputs input;
    private PlayerInput playerInput;
    private bool talking = false;
    private Npc lastTalkedTo = null;
    private MailBag mailbag;

    private int coffeeCount = 0;
    [SerializeField] private GameObject mailbagObject;

    [SerializeField] private UIManager uiManager;
    
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();
        movement = GetComponent<FirstPersonController>();
        mailbagObject = Instantiate(mailbagObject);
        mailbag = mailbagObject.GetComponent<MailBag>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        FireRay();
        
        // Bring up the mailbag
        if (Input.GetKeyDown(KeyCode.I))
        {
            mailbag.Toggle();
            ToggleCursor();
            playerInput.enabled = !playerInput.enabled;
        }
    }

    void ToggleCursor()
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.visible ? CursorLockMode.Confined : CursorLockMode.Locked;
    }
    
    void FireRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
        RaycastHit hitData;

        Physics.Raycast(ray, out hitData);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        
        if (Physics.Raycast(ray, out hitData, rayDistance, layer))
        {
            if (!talking)
            {
                hud.Show();
            }
            
            // Handling doors
            if (hitData.collider.GetComponentInParent<Door>() is { } door)
            {
                hud.SetActionText(door.ToString());
                // If E is pressed then do something
                if (Input.GetKeyDown(KeyCode.E))
                {
                    door.Open(transform, uiManager.fadeCanvas); 
                }
            }
            
            // Handling letters
            else if (hitData.collider.GetComponent<Letter>() is { } letter)
            {
                // Prevents the following if the mailbag is already full
                if (mailbag.IsFull()) return;
                
                hud.SetActionText(letter.ToString());

                // If player presses E
                if (Input.GetKeyDown(KeyCode.E))
                {
                    mailbag.AddLetter(letter);
                }
            }
            
            // Handling coffee
            else if (hitData.collider.GetComponent<Coffee>() is { } coffee)
            {
                if (coffee.CanBePickedUp())
                {
                    hud.SetActionText(coffee.ToString());
                
                    // If E is pressed then do something
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        coffee.Hide();
                        coffeeCount++;
                    }
                }

            }
            
            else if (hitData.collider.GetComponent<Tray>() is { } tray)
            {
                if (tray.IsEmpty())
                {
                    hud.SetActionText("Place coffee");
                    
                    // If E is pressed
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        tray.PlaceCoffee();
                    }
                    
                   
                }
            }
            
            // Handling NPCs
            else if (hitData.collider.GetComponentInParent<Npc>() is { } npc)
            {
                hud.SetActionText(npc.ToString());
                
                // If E is pressed then do something
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (talking != true)
                    {
                        if (lastTalkedTo == null)
                        {
                            lastTalkedTo = npc;
                            
                        }
                        npc.OnEnd += DoneTalking;
                        
                        ToggleTalking();
                        npc.TalkTo(); // calls descant dialogue trigger to display
                        npcLookAtCamera.LookAt = npc.transform; // Swivels to look at NPC
                        
                    }
                    
                }
            }
            
            else
            {
                hud.Hide();
            }
            
        }
        else
        {
            hud.Hide();
        }
    }

    void ToggleTalking()
    {
        talking = !talking;
        hud.Toggle();
        npcLookAtCamera.enabled = !npcLookAtCamera.enabled;
        playerInput.enabled = !playerInput.enabled;
        ToggleCursor();
    }

    void DoneTalking()
    {
        ToggleTalking();
        lastTalkedTo.OnEnd -= DoneTalking;
    }

    
}
