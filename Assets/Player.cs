using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HUD.enabled = false;
    }

    // Update is called once per frame

    [SerializeField]
    private Canvas HUD;
    
    RaycastHit hitData;
    [SerializeField]
    private LayerMask layer;
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
            Debug.Log("Hit something!");
            if (hitData.collider.GetComponentInParent<Interactable>())
            {
                Interactable hitObject = hitData.collider.GetComponentInParent<Interactable>();
                Debug.Log("yuh");
                //hitObject.GetComponent<MeshRenderer>().material.SetColor("_BaseColor",Random.ColorHSV());
                HUD.enabled = true;
            }
        }
        else
        {
            HUD.enabled = false;
        }
    }
}
