using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelParser : MonoBehaviour
{

    public GameObject tiles;
    public TextAsset csvFile;
    public GameObject player;
    public GameObject baseTile;
    public GameObject hazards;
    public List<LevelObject> levelObjects;
    public bool hasPopulated = false;

    public string[] _lines;

    public void PopulateLevelFromFile()
    {
        if (!hasPopulated)
        {
            if (csvFile != null)
            {

                int z = 0;
                _lines = Regex.Split(csvFile.text, "\n");
                foreach (string _line in _lines)
                {
                    int x = 0;
                    string[] _tiles = _line.Split(',');

                    foreach (string _tile in _tiles)
                    {
                        if (_tile != null && !_tile.Equals(""))
                        {
                            GameObject go = InitGameObjectFromKey(_tile);

                            if (go.tag.Equals("Enemy"))
                            {
                                go.transform.position = new Vector3(x, .6f, z);
                                go.transform.SetParent(hazards.transform);
                                GameObject floor = Instantiate(baseTile);
                                floor.transform.position = new Vector3(x, 0, z);
                                floor.transform.SetParent(tiles.transform);
                                string name = floor.name.Replace("(Clone)", "");
                                floor.name = name;
                            }
                            else
                            {
                                go.transform.SetParent(tiles.transform);
                                go.transform.position = new Vector3(x, 0, z);
                            }
                            if (_tile.Contains("enter"))
                            {
                                GameObject _player = Instantiate(player);
                                string name = _player.name.Replace("(Clone)", "");
                                _player.name = name;
                                _player.transform.position = new Vector3(x, .6f, z);
                            }
                        }
                        else if (_tile.Equals(""))
                        {
                            GameObject floor = Instantiate(baseTile, new Vector3(x, 0, z), Quaternion.identity);
                            floor.transform.SetParent(tiles.transform);
                            string name = floor.name.Replace("(Clone)", "");
                            floor.name = name;
                        }
                        x++;
                    }
                    z++;
                }
                hasPopulated = true;
            }
        }
    }

    private GameObject InitGameObjectFromKey(string _key)
    {
        _key = _key.Trim();
        foreach (LevelObject _levelObject in levelObjects)
        {
            if (_key.Equals(_levelObject.key))
            {
                GameObject go = Instantiate(_levelObject.go);
                string name = go.name.Replace("(Clone)", "");
                go.name = name;
                return go;
            }
            if (_key.Equals("enter"))
            {

            }
        }
        if (_key.StartsWith("e"))
        {
            string[] _enemy = _key.Split('_');
            foreach (LevelObject _levelObject in levelObjects)
            {
                if (_enemy[0].Equals(_levelObject.key))
                {
                    GameObject go = Instantiate(_levelObject.go);
                    string name = go.name.Replace("(Clone)", "");
                    go.name = name;
                    switch (_enemy[1])
                    {
                        case "L":
                            go.GetComponent<EnemyController>().SetFacing(Facing.Left);
                            break;
                        case "R":
                            go.GetComponent<EnemyController>().SetFacing(Facing.Right);
                            break;
                        case "U":
                            go.GetComponent<EnemyController>().SetFacing(Facing.Up);
                            break;
                        case "D":
                            go.GetComponent<EnemyController>().SetFacing(Facing.Down);
                            break;
                    }
                    return go;
                }
            }
        }
        return null;
    }

    [System.Serializable]
    public struct LevelObject
    {
        public string key;
        public GameObject go;
    }

}

