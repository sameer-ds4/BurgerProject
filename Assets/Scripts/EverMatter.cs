using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EverMatter : MonoBehaviour
{
    BurgerPart everMatterType;
    public GridManager gridManager;
    public BurgerObject burgerObject;
    int occurence = 0;

    private Dictionary<BurgerObject, int> matchPairs;

    public BurgerObject[] row;
    public BurgerObject[] col;

    void Start()
    {
        gridManager = GameManager.Instance.gridManager;
        ImportData();
    }

    private void ImportData()
    {
        row = gridManager.match_H.ToArray();
        col = gridManager.match_V.ToArray();
        // Debug.LogError("Hig");
        
        CheckOccurence(row);
        CheckOccurence(col);

        burgerObject.burgerPart = everMatterType;
    }

    private void CheckOccurence(BurgerObject[] objs)
    {
        matchPairs = new Dictionary<BurgerObject, int>();
        for (int i = 0; i < objs.Length; i++)
        {
            if(matchPairs.ContainsKey (objs[i]))
            {
                int tempCount = matchPairs[objs[i]];

                if(tempCount > occurence)
                {
                    occurence = tempCount;
                    everMatterType = objs[i].burgerPart;
                }

                matchPairs.Remove(objs[i]);
                matchPairs.Add(objs[i], tempCount++);
            }
            else
            {
                matchPairs.Add(objs[i], 1);
            }
        }
    }
}
