using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    [Header("Controllers")]
    public LevelController levelController;
    public GUIController guiController;

    [Header("Sounds")]
    private AudioSource audioSource;
    public AudioClip soundTrack;
    public AudioClip girlFX1;

    private bool flagPause = false;

	void Start () {
        levelController = new LevelController();
        guiController = new GUIController();
	}

    public void PauseGameTime()
    {
        if (!flagPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        flagPause = !flagPause;
    }

    private void LoadSounds()
    {
        //Carga los sonidos.......
    }

    public void GameOver()
    {
        //Para el juego y muestra la nueva gui, he inicia la vista principal....
    }

    public void Winner()
    {
        //Para el juego y muestra la nueva gui, he inicia la vista principal....
    }

    internal void FinishLevel()
    {
        //Finaliza el nivel y carga la scena final
    }
}
