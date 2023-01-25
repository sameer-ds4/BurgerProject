using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    //public Camera uiCamera;
    public Camera inGameCamera;
    public GameObject directionalLight;
    public Light dirlight;

    [Header("Camera Pos & Rot Valus")]
    public Vector3 startCameraPos;
    public Vector3 startCameraRot;
    public Vector3 playCameraPos;
    public Vector3 playCameraRot;

    [Header ("FOV Settings")]
    public float playerNormalFov = 60;
    public float playerSlowFov = 55;
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


    public void ChangeLight (Vector3 updaterot, float timet, float intensity)
    {
        directionalLight.transform.DORotate(updaterot, timet);
        dirlight.intensity = intensity;
    }
}