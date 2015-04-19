using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayView : MonoBehaviour
{
    private GUIController guiController;
    public GameObject ChildPanel;

    void Start()
    {
        if (GameObject.Find("GUIController(Clone)"))
            guiController = GameObject.Find("GUIController(Clone)").GetComponent<GUIController>();
    }

    public void MenuView()
    {
        guiController.MenuGameButton();
    }

    public void AddChild(Sprite spriteChild, int arrayPosition)
    {

    }

}
