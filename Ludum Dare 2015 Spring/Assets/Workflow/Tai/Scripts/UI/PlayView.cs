using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayView : MonoBehaviour
{
    private GUIController guiController;
    public GameObject ChildPanel;

    void Start()
    {
        guiController = GameController.Instance.GetComponent<GameController>().guiController;
    }

    public void MenuView()
    {
        guiController.MenuGameButton();
    }

    public void AddChild(Sprite spriteChild, int arrayPosition)
    {

    }

}
