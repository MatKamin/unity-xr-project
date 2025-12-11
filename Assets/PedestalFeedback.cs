using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PedestalFeedback : MonoBehaviour
{
    [Header("Visuals")]
    public MeshRenderer pedestalRenderer;
    public Material emptyMaterial;
    public Material filledMaterial;

    void Start()
    {
        if (pedestalRenderer != null && emptyMaterial != null)
        {
            pedestalRenderer.material = emptyMaterial;
        }
    }

    public void OnItemPlaced(SelectEnterEventArgs args)
    {
        if (pedestalRenderer != null && filledMaterial != null)
        {
            pedestalRenderer.material = filledMaterial;
        }
    }

    public void OnItemRemoved(SelectExitEventArgs args)
    {
        if (pedestalRenderer != null && emptyMaterial != null)
        {
            pedestalRenderer.material = emptyMaterial;
        }
    }
}