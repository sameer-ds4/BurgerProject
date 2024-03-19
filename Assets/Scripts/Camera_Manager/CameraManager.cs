using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    //public Camera uiCamera;
    public Camera inGameCamera;
    public Light dirlight;

    [Header("Camera Pos & Rot Valus")]
    public Vector3 startCameraPos;
    public Vector3 startCameraRot;
    public Vector3 playCameraPos;
    public Vector3 playCameraRot;

    [Header ("FOV Settings")]
    public float playerNormalFov = 60;


    private void Awake()
    {
        Instance = this;
    }

    public void SetCameraPosOnPlay(Vector3 updateCameraPos, Vector3 updateCameraRot,float time)
    {
        inGameCamera.transform.DOMove(updateCameraPos, time);
        inGameCamera.transform.DORotate(updateCameraRot, time);
    }
    public void SetCameraFOV(float fov)
    {
        inGameCamera.DOFieldOfView(fov, 0.3f);
    }
       
    public void SetCamParent(GameObject parent)
    {
        inGameCamera.transform.parent = parent.transform;
        SetCameraStartValus();
    }

    private void SetCameraStartValus()
    {
        inGameCamera.transform.position = startCameraPos;
        inGameCamera.transform.eulerAngles = startCameraRot;
        inGameCamera.fieldOfView = playerNormalFov;
    }

    public void SetCameraFocus(Vector2 posPoint, int offset)
    {
        inGameCamera.transform.position = new Vector3(posPoint.x, 18, posPoint.y + 2.65f);
        inGameCamera.orthographicSize = inGameCamera.orthographicSize + 2 * offset;
    }
}