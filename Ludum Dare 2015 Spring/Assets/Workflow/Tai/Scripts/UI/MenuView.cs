using UnityEngine;
using System.Collections;

public class MenuView : MonoBehaviour {
    private GUIController guiController;

    void Start()
    {
        if (GameObject.Find("GUIController(Clone)"))
            guiController = GameObject.Find("GUIController(Clone)").GetComponent<GUIController>();
    }

    public void ExitView()
    {
        guiController.InitGameButton();
    }

    public void PlayView()
    {
        guiController.MenuGameButton();
    }
}