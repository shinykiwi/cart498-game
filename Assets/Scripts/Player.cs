using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HUD.enabled = false;
        input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame

    [SerializeField]
    private Canvas HUD;
    
    RaycastHit hitData;
    [SerializeField]
    private LayerMask layer;
    
    private StarterAssetsInputs input;
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
            if (hitData.collider.GetComponentInParent<Door>())
            {
                Door door = hitData.collider.GetComponentInParent<Door>();
               
                HUD.enabled = true;

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
        }
        else
        {
            HUD.enabled = false;
        }
    }
    
    
}
