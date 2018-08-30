using UnityEngine;

[ExecuteInEditMode]
public class RunScript : MonoBehaviour {

    public bool runPopulateLevelFromFile = false;
    public bool runDestroyLevelData = false;

    // Update is called once per frame
    void Update () {
		if(runPopulateLevelFromFile)
        {
            GenerateLevel();
            runPopulateLevelFromFile = false;
            runDestroyLevelData = false;
        }
        if (runDestroyLevelData)
        {
            DestroyLevelData();
        }
    }


    public void GenerateLevel()
    {
        GameObject.Find("LevelParser").GetComponent<LevelParser>().PopulateLevelFromFile();
        //GameObject.Find("LevelData").GetComponent<LevelData>().PopulateLevelData();
    }

    public void DestroyLevelData()
    {
        GameObject tiles = GameObject.Find("Tiles");
        if(tiles != null)
        {
            foreach (Transform child in tiles.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        GameObject hazards = GameObject.Find("Hazards");
        if (hazards != null)
        {
            foreach (Transform child in hazards.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        GameObject player = GameObject.Find("Player");
        if(player != null)
        {
            DestroyImmediate(player);
        }
        //GameObject.Find("LevelData").GetComponent<LevelData>().PopulateLevelData();
    }
}
