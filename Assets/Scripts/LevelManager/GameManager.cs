using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Particle System Data")]
    public ParticleSystemData _ParticleSystemData;

    [Header("Game Component Data")]
    public FoodObjects foodObjects;
    public GridData gridData;

    [Header("Scene Data")]
    public Transform levelSpawnPoint;
    public Transform foodParent;



    // public LevelContainerData _levelContainerData;

    protected GameObject levelLoaded;
    [HideInInspector]
    public LevelContainerBase currenterLevelContainer;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //CreateLevel();
    }

}



    // public void CreateLevel()
    // {        
    //     //AudioManager.Instance.PlaySound("BG");
        //UIManager.Instance.OnStartDisabel();
        //UIManager.Instance.SetLevelIDNumber();
        // if (levelLoaded)
        // {
        //     Destroy(levelLoaded);
        // }
        // InstantiateLevel();
        //ThemeManager.Instance.InstantiateLevelTheme(((int)currenterLevelContainer.themeType));
    // }
    // private void InstantiateLevel()
    // {
    //     ClearSpawnedOBJ();
    //     if (SaveDataHandler.Instance.LevelID >= _levelContainerData.levelContainers.Count)
    //     {
    //         //Debug.Log(PlayerPrefs.GetInt("LEVELID"));
    //         int re = SaveDataHandler.Instance.LevelID % _levelContainerData.levelContainers.Count;
    //         levelLoaded = Instantiate(Resources.Load<LevelContainerBase>("Levels/" + _levelContainerData.levelContainers[SaveDataHandler.Instance.LevelID]).gameObject, levelSpawnPoint.position, Quaternion.identity);
    //         currenterLevelContainer = levelLoaded.GetComponent<LevelContainerBase>();
    //         SpawnInChild(levelLoaded);
    //     }
    //     else
    //     {
    //         levelLoaded = Instantiate(Resources.Load<LevelContainerBase>("Levels/" + _levelContainerData.levelContainers[SaveDataHandler.Instance.LevelID]).gameObject, levelSpawnPoint.position, Quaternion.identity);
    //         currenterLevelContainer = levelLoaded.GetComponent<LevelContainerBase>();
    //         SpawnInChild(levelLoaded);
    //     }
    // }
//     public void NextLevel()
//     {
//         CreateLevel();
//     }
//     public void RetryLevel()
//     {
//         NextLevel();
//     }


//     public void SpawnInChild(GameObject makeChild)
//     {
//         makeChild.transform.parent = levelSpawnPoint.transform;
//     }

//     public void ClearSpawnedOBJ()
//     {
//         foreach (Transform child in levelSpawnPoint.transform)
//         {
//             Destroy(child.gameObject);
//         }
//     }
// }