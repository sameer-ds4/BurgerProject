using UnityEngine;
using UnityEditor;
using System.IO;

public class Screenshot : MonoBehaviour
{
    public Camera mainCam;

    public int cWidth;
    public int cHeight;

    public string folderPath;

    public GameObject[] groupShotPics;

    private RenderTexture rt;
    private Texture2D screenshotOutput;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            TakeScreenshot();
    }

    private void TakeScreenshot()
    {
        if (mainCam == null)
            mainCam = GetComponent<Camera>();

        for (int i = 0; i < groupShotPics.Length; i++)
        {
            Debug.LogError("1");
            groupShotPics[i].SetActive(true);

            rt = new RenderTexture(cWidth, cHeight, 24);
            mainCam.targetTexture = rt;

            screenshotOutput = new Texture2D(cWidth, cHeight, TextureFormat.RGBA32, false);
            mainCam.Render();

            RenderTexture.active = rt;
            screenshotOutput.ReadPixels(new Rect(0, 0, cWidth, cHeight), 0, 0);

            mainCam.targetTexture = null;
            RenderTexture.active = null;
            Debug.LogError("2");
            Destroy(rt);

            byte[] bytes = screenshotOutput.EncodeToPNG();
            File.WriteAllBytes(folderPath + "Img" + i + ".PNG", bytes);
            Debug.LogError("3");
            AssetDatabase.Refresh();

            // groupShotPics[i].SetActive(false);
        }
    }
}

public enum Format
{
    RAW,
    JPG,
    PNG
}
