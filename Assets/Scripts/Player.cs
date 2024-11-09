using System.Linq;
using Cinemachine;
using StarterAssets;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private HUD hud;
    [SerializeField] private LayerMask layer;
    [SerializeField] private CinemachineVirtualCamera npcLookAtCamera;
    [SerializeField] private int rayDistance = 5;
    
    private RaycastHit hitData; 
    private ThirdPersonController movement;
    private StarterAssetsInputs input;
    private bool talking = false;
    private Npc lastTalkedTo = null;

    private MailBag mailbag;
    [SerializeField] private GameObject mailbagObject;
    
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        movement = GetComponent<ThirdPersonController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mailbagObject = Instantiate(mailbagObject);
        mailbag = mailbagObject.GetComponent<MailBag>();
    }
    
    void Update()
    {
        FireRay();
        
        // Bring up the mailbag
        if (Input.GetKeyDown(KeyCode.F))
        {
            //ebug.Log(mailbag.ToString());
            mailbag.Toggle();
        }
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
            if (hitData.collider.GetComponentInParent<Door>() is { } door)
            {
                hud.SetActionText(door.ToString());
                // If E is pressed then do something
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // If the door is not already being used
                    if (!door.InUse())
                    {
                        door.Open(); 
                    }
                }
            }
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
                        Debug.Log(talking);
                    }
                    
                }
            }
            else if (hitData.collider.GetComponentInParent<Letter>() is { } letter)
            {
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
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.visible ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    void DoneTalking()
    {
        ToggleTalking();
        lastTalkedTo.OnEnd -= DoneTalking;
    }

    
}
