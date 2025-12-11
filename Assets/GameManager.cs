using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public void StartGame()
    {
        tutorialCanvas.SetActive(false);
    }
}