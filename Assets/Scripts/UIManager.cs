using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] public GameObject fadeCanvas;

   private void Start()
   {
      fadeCanvas = Instantiate(fadeCanvas, transform);
   }
}
