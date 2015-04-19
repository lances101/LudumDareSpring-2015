using UnityEngine;
using System.Collections;

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
    public int maxNumChildren;
    public int teachersCount;
    

    [Header("SPAWN POINTS")] 
    public GameObject spawnPointManager;

    [Header("UNIT PREFABS")]
    public GameObject boyTemplate;
    public GameObject teacherTemplate;
    public Sprite childBall;
    private int numChildren;
    
    private ArrayList childrenList;
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
        gameController = this.transform.parent.GetComponent<GameController>();
        guiController = this.transform.parent.FindChild("GUIController(Clone)").GetComponent<GUIController>();

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
        guiController.ActivateView(3);
        StartCoroutine("WaitForCreateChildre", 0.5f);
    }

    private GameObject CreateGOAtPos(GameObject go, Vector2 pos)
    {
        return (GameObject) Instantiate(go, pos, Quaternion.identity);
        
    }
    private GameObject CreateChild(int index)
    {
        Vector3 position = arrayPosition[index].position;
        Vector2 positio2D = new Vector2(position.x, position.y);

        
        //RaycastHit2D environmentHit = Physics2D.CircleCast(positio2D, radius, Vector2.zero);//, 1, 1 << LayerMask.NameToLayer("Environment"));

        //if (environmentHit)
        //{
        //    CreateChild(index);
        //}

        return (GameObject)Instantiate(boyTemplate, position, Quaternion.identity);
    }

    IEnumerator WaitForCreateChildre(float sec)
    {
        
        yield return new WaitForSeconds(sec);
        childrenList = new ArrayList();
        people = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var manager = spawnPointManager.GetComponent<SpawnpointManager>();
        var boyPoints = manager.GetBoyPoints();
        var boyIds = Utils.GetUniqueRandomInt(numChildren, 0, numChildren);
        var teacherPoints = manager.GetTeacherPoints();
        var girlPoints = manager.GetGirlPoints();

        var pointRandomizer = Utils.GetUniqueRandomInt(numChildren, 0, boyPoints.Length);
        
        for (int index = 0; index < numChildren; index++)
        {
            var boy = CreateGOAtPos(boyTemplate, boyPoints[pointRandomizer[index]]);
            boy.GetComponent<BoyController>().BoyID = boyIds[index];
            boy.transform.SetParent(people.transform);
            childrenList.Add(boyIds[index]);
        }
        pointRandomizer = Utils.GetUniqueRandomInt(teachersCount, 0, teacherPoints.Length);
        
        for (int index = 0; index < teachersCount; index++)
        {
            var teach = CreateGOAtPos(teacherTemplate, teacherPoints[pointRandomizer[index]]);
            teach.transform.SetParent(people.transform);
        }
        //GameObject.Find("Girl").transform.position= girlPoints[Random.Range(0, girlPoints.Length)];

    }

    IEnumerator WaitForFinishLevel(float sec)
    {
        yield return new WaitForSeconds(sec);
        gameController.FinishLevel();
    }

    private void CreateBall(Sprite childBallSprite, int indexPositionChildrenList)
    {
        pachinkoController.CreateBall(childBallSprite, indexPositionChildrenList);
    }

    private void RemoveChild(int index)
    {
        guiController.RemoveChildGUI(index);
        RemoveChildList(index);
    }

    private void RemoveChildList(int index)
    {
        GameObject geChild = (GameObject)childrenList[index];
        childrenList.Remove(geChild);
        Destroy(geChild);
    }

    private int FindChildList(int idChild)
    {
        int length = childrenList.Count;

        for (int i = 0; i < length; i++)
        {
            GameObject geChild = (GameObject)childrenList[i];
            BoyController child = geChild.GetComponent<BoyController>();

            if (child.BoyID == idChild)
                return i;
        }

        return -1;
    }

    public void NotifyPachinko(int indexPosition, bool result)
    {
        if (result)
            RemoveChild(indexPosition);
        else
            ChildComeBack(indexPosition);
    }

    private void ChildComeBack(int index)
    {
        GameObject geChild = (GameObject)childrenList[index];
        geChild.gameObject.SetActive(true);
    }

    public void SendToPachinko(GameObject geChild)
    {
        if (geChild.GetComponent<BoyController>())
        {
            BoyController child = geChild.GetComponent<BoyController>();
            //int indexPositionChildrenList = FindChildList(child.BoyID);
            int indexPositionChildrenList = childrenList.IndexOf(child.BoyID);
            CreateBall(childBall, indexPositionChildrenList);
            child.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No lo envio al pachinko!!!");
        }
    }
}
