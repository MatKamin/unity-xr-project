using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TouchTriggerLogic : MonoBehaviour
{
    [Header("Requirements")]
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor pedestalSocket;
    public GameObject successCanvas;

    void Start()
    {
        if (successCanvas != null)
        {
            successCanvas.SetActive(false);
        }
    }
    
    public void CheckAndActivate(HoverEnterEventArgs args)
    {
        if (pedestalSocket.hasSelection)
        {
            successCanvas.SetActive(true);
            Debug.Log("Success! Canvas shown.");
        }
        else
        {
            Debug.Log("Locked: You must place the cube first.");
        }
    }
}