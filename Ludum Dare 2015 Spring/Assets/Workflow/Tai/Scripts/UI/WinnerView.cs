using UnityEngine;
using System.Collections;

public class WinnerView : MonoBehaviour
{
    private GUIController guiController;

    void Start()
    {
        guiController = GameController.Instance.GetComponent<GameController>().guiController;
    }

    public void ChangeView()
    {
        guiController.WinnerGameButton();
    }
}

