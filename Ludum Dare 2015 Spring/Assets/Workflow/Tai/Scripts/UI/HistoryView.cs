﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HistoryView : MonoBehaviour
{
    private GUIController guiController;
    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("GUIController(Clone)"))
            guiController = GameObject.Find("GUIController(Clone)").GetComponent<GUIController>();
    }

    public void PlayView()
    {
        guiController.StarGameButton();
    }
}
