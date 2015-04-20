using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    private GameController gameController;
    private GUIController guiController;
    
    [Header("PACHINKO")]
    public GameObject pachinko;
    private GameObject instantiatePachinko;
    private PachinkoController pachinkoController;

    [Header("KINDER GARDEN")]
    public GameObject kinderGarden;
    private GameObject instantiateKinderGarden;

    [Header("UNITS PREFS")]
    public int minNumChildren;
    public int kidsNeeded;
    public int maxNumChildren;
    public int teachersCount;
    

    [Header("SPAWN POINTS")] 
    public GameObject spawnPointManager;

    [Header("UNIT PREFABS")]
    public GameObject boyTemplate;
    public GameObject teacherTemplate;
    public Sprite childBall;
    private int numChildren;
    
    private List<GameObject> childrenList;
    private List<int> neededBoyList;
    private GameObject people;

    private Transform[] arrayPosition;

    [Header("SPAM")]
    public float radius = 0.5f;

    void Awake()
    {
        numChildren = Random.Range(minNumChildren, maxNumChildren + 1);
    }

    void Start()
    {
        
        gameController = GameController.Instance.GetComponent<GameController>();
        guiController = gameController.guiController;

        CreateKinderGarden();
        CreatePachinko();
        LoadLevelGame();
    }

    private void CreateKinderGarden()
    {
        if (kinderGarden)
            instantiateKinderGarden = Instantiate(kinderGarden);
        else
            Debug.Log("No encontro kinderGarden");
    }

    private void CreatePachinko()
    {
        instantiatePachinko = GameObject.Find("Pachinko"); //Instantiate(pachinko);
        pachinkoController = instantiatePachinko.GetComponent<PachinkoController>();
    }

    private void LoadLevelGame()
    {
        guiController.ActivateView("PlayView");
        StartCoroutine("WaitForCreateChildre", 0.5f);
    }

    private GameObject CreateGOAtPos(GameObject go, Vector2 pos)
    {
        return (GameObject) Instantiate(go, pos, Quaternion.identity);
        
    }
    
    IEnumerator WaitForCreateChildre(float sec)
    {
        
        yield return new WaitForSeconds(sec);
        childrenList = new List<GameObject>();
        people = new GameObject("People");
        people.transform.SetParent(transform);
        var manager = spawnPointManager.GetComponent<SpawnpointManager>();
        var boyPoints = manager.GetBoyPoints();
        var boyIds = Utils.GetUniqueRandomInt(15, 0, 15);
        var teacherPoints = manager.GetTeacherPoints();
        var girlPoints = manager.GetGirlPoints();
        var neededKidsCounter = 0;
        neededBoyList = new List<int>();
        var pointRandomizer = Utils.GetUniqueRandomInt(numChildren, 0, boyPoints.Length);
        
        for (int index = 0; index < numChildren; index++)
        {
            var boy = CreateGOAtPos(boyTemplate, boyPoints[pointRandomizer[index]]);
            boy.GetComponent<BoyController>().BoyID = boyIds[index];
            boy.transform.SetParent(people.transform);
            if (neededKidsCounter < kidsNeeded)
            {
                neededBoyList.Add(boyIds[index]);
                string spriteString = "Sprites/Boys/boy_" + boyIds[index] + "/boy_down_idle";
                Debug.Log("Trying to load sprite from " + spriteString);
                guiController.AddChildGUI(boyIds[index], Resources.Load<Sprite>(spriteString));
                neededKidsCounter++;
            }
            childrenList.Add(boy);
        }
        pointRandomizer = Utils.GetUniqueRandomInt(teachersCount, 0, teacherPoints.Length);
        
        for (int index = 0; index < teachersCount; index++)
        {
            var teach = CreateGOAtPos(teacherTemplate, teacherPoints[pointRandomizer[index]]);
            teach.transform.SetParent(people.transform);
        }
        
    }

    IEnumerator WaitForFinishLevel(float sec)
    {
        yield return new WaitForSeconds(sec);
        gameController.FinishLevel();
    }

    private void CreateBall(Sprite childBallSprite, int boyID)
    {
        pachinkoController.CreateBall(childBallSprite, boyID);
    }

    private GameObject FindNeededBoy(int boyId)
    {
        foreach (var boy in childrenList)
        {
            if (boy == null) continue;
            if (boy.GetComponent<BoyController>().BoyID == boyId)
            {
                return boy;
            }
        }
        return null;
    }
    private void RemoveChild(int boyID)
    {
        GameController.Instance.audioController.PlayGlobalFX("game_capture");
        var voiceId = Random.Range(1, 4);
        GameController.Instance.audioController.PlayGlobalFX("voice_girl_captured_"+voiceId);
        guiController.RemoveChildGUI(boyID);
        var go = FindNeededBoy(boyID);
        if (go == null) return;
        neededBoyList.Remove(boyID);
        childrenList.Remove(go);
        Destroy(go);
        CheckWinConditions();
    }

    private void CheckWinConditions()
    {
        Debug.Log("BOYS LEFT" + neededBoyList.Count);
        if (neededBoyList.Count == 0)
        {
            gameController.FinishLevel();
        }
    }
    public void NotifyPachinko(int indexPosition, bool result)
    {
        if (indexPosition == -1) return;

        if (!result)
            RemoveChild(indexPosition);
        else
            ChildComeBack(indexPosition);
    }

    private void ChildComeBack(int index)
    {
        GameController.Instance.audioController.PlayGlobalFX("game_teleport");
        GameObject geChild = FindNeededBoy(index);
        gameController.guiController.UpdateChildGUI(index, Resources.Load<Sprite>("Sprites/Boys/boy_" + geChild.GetComponent<BoyController>().BoyID + "/boy_down_idle"));
        geChild.gameObject.SetActive(true);
    }

    public void SendToPachinko(GameObject geChild)
    {
        if (geChild.GetComponent<BoyController>())
        {
            BoyController child = geChild.GetComponent<BoyController>();
            int boyId = geChild.GetComponent<BoyController>().BoyID;
            if (!neededBoyList.Contains(boyId))
            {
                GameController.Instance.audioController.PlayGlobalFX("voice_girl_idontlikeyou");
                gameController.GameOver();
                return;
            }
            guiController.UpdateChildGUI(boyId, childBall);
            
            CreateBall(childBall, boyId);
            child.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No lo envio al pachinko!!!");
        }
    }
}
