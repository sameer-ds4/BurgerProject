using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LevelContainer : LevelContainerBase
{
    new public static LevelContainer Instance;
    //public SplineComputer splinecomputer;
    public Camera cam;
    public int dots_count;


    [Header("UI Elememts")]
    public GameObject charSelect;
    public GameObject play;


    [Header("Game Objects")]


    private bool glaf;

    public Text Dialogue;
    //--- Hidden ---------
    Ray ray;
    RaycastHit hit;

    private void Awake ()
    {
        Instance = this;
    }

    void Start ()
    {
        cam = Camera.main;
        CameraSetting();
    }

    void CameraSetting ()
    {
        CameraManager.Instance.startCameraPos = new Vector3(0, 3, -10);
        //CameraManager.Instance.startCameraRot = new Quaternion(12, 0, 0, 0);
        //CameraManager.Instance.SetCamParent(trash.gameObject);
    }

    private void Update ()
    {


    }

    
}