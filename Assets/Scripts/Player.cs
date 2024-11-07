using Cinemachine;
using StarterAssets;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Canvas HUD;
    [SerializeField] private LayerMask layer;
    [SerializeField] private CinemachineVirtualCamera npcLookAtCamera;
    
    private RaycastHit hitData; 
    private ThirdPersonController movement;
    private StarterAssetsInputs input;
    private bool isInConversation = false;
    
    void Start()
    {
        HUD.enabled = false;
        input = GetComponent<StarterAssetsInputs>();
        movement = GetComponent<ThirdPersonController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        FireRay();
    }
    
    void FireRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
        RaycastHit hitData;

        Physics.Raycast(ray, out hitData);
        Debug.DrawRay(ray.origin, ray.direction * 5, Color.red);
        
        if (Physics.Raycast(ray, out hitData, 5, layer))
        {
            if (hitData.collider.GetComponentInParent<Door>() is { } door)
            {
                HUD.enabled = !isInConversation;
                Debug.Log(isInConversation);

                // If E is pressed then do something
                if (input.interact)
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
                HUD.enabled = !isInConversation;
                
                // If E is pressed then do something
                if (input.interact)
                {
                    if (!npc.IsInConversation())
                    {
                        ToggleMovement();
                        npc.TalkTo(); // calls descant dialogue trigger to display
                        isInConversation = true;
                        ToggleHUD();
                        //npcLookAtCamera.LookAt = npc.transform;
                        npcLookAtCamera.enabled = true;
                    }
                }
            }
        }
        else
        {
            HUD.enabled = false;
        }
    }

    void ToggleMovement()
    {
        ToggleHUD();
        Cursor.visible = !Cursor.visible;

        if (Cursor.visible)
        {
            Debug.Log("cursor visible");
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Debug.Log("cursor not visible");
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        
    }

    void ToggleHUD()
    {
        HUD.enabled = !HUD.enabled;
    }
    
    
}
