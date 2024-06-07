using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPrefabReference : MonoBehaviour
{
    public static CardPrefabReference Instance;
    [SerializeField]
    public List<GameObject> prefabs = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetPrefab(string prefabName)
    {
        foreach (GameObject prefab in prefabs)
        {
            if(prefab.name == prefabName)
                return prefab;
        }
        return null;
    }
}
