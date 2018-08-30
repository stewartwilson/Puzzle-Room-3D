using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public List<GameObject> hazards;
    public string currentLevelName = "Level1";
    public float turnDelay;
    public float movementSpeed;

    public void SetHazards(List<GameObject> _hazards)
    {
        hazards = _hazards;
    } 

    public void TakeTurn()
    {
        foreach(GameObject go in hazards)
        {
            if ("Enemy".Equals(go.tag))
            {
                EnemyController ec = go.GetComponent<EnemyController>();
                if (ec != null)
                {
                    ec.TakeTurn();
                }
            }
        }
    }

    public void loadLevel(string newlevelName)
    {
        SceneManager.LoadScene(newlevelName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentLevelName);
        currentLevelName = newlevelName;

    }

    public void reloadLevel()
    {
        SceneManager.UnloadSceneAsync(currentLevelName);
        SceneManager.LoadScene(currentLevelName, LoadSceneMode.Additive);

    }

}
