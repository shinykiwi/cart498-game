using System.Linq;
using Cinemachine;
using Descant.Components;
using StarterAssets;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private HUD hud;
    [SerializeField] private LayerMask layer;
    [SerializeField] private CinemachineVirtualCamera npcLookAtCamera;
    [SerializeField] private int rayDistance = 5;
    
    private RaycastHit hitData; 
    private FirstPersonController movement;
    private StarterAssetsInputs input;
    private bool talking = false;
    private Npc lastTalkedTo = null;
    private MailBag mailbag;
    [SerializeField] private GameObject mailbagObject;

    [SerializeField] private UIManager uiManager;
    
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            mailbag.Toggle();
            ToggleCursor();
            input.enabled = !input.enabled;
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
            
            //Debug.Log(hitData.collider.name);
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
            // Handling letters
            else if (hitData.collider.GetComponentInParent<Letter>() is { } letter)
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
        ToggleCursor();
    }

    void DoneTalking()
    {
        ToggleTalking();
        lastTalkedTo.OnEnd -= DoneTalking;
    }

    
}
