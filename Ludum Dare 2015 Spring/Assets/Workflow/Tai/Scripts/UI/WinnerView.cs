using UnityEngine;
using System.Collections;

public class WinnerView : MonoBehaviour
{
    private GUIController guiController;

    void Start()
    {
        if (GameObject.Find("GUIController(Clone)"))
            guiController = GameObject.Find("GUIController(Clone)").GetComponent<GUIController>();
    }

    public void ChangeView()
    {
        guiController.WinnerGameButton();
    }
}

