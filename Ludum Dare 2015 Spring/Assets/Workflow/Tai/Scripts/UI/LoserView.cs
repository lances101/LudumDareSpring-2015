using UnityEngine;
using System.Collections;

public class LoserView : MonoBehaviour
{
    private GUIController guiController;

    void Start()
    {
        guiController = GameController.Instance.GetComponent<GameController>().guiController;
    }

    public void ChangeView()
    {
        guiController.LoserGameButton();
    }
}

