using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayView : MonoBehaviour
{
    private GUIController guiController;

    void Start()
    {
        if (GameObject.Find("GUIController(Clone)"))
            guiController = GameObject.Find("GUIController(Clone)").GetComponent<GUIController>();
    }

    public void MenuView()
    {
        guiController.MenuGameButton();
    }

}
