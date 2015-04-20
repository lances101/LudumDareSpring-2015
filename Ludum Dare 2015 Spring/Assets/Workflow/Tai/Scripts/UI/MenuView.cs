using UnityEngine;
using System.Collections;

public class MenuView : MonoBehaviour {
    private GUIController guiController;

    void Start()
    {
        guiController = GameController.Instance.GetComponent<GameController>().guiController;
    }

    public void ExitView()
    {
        guiController.InitGameButton();
    }

    public void PlayView()
    {
        guiController.ResumeGameButton();
    }
}