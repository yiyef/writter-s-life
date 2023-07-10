using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public static GameObject AddChild(GameObject parent, GameObject prefab, bool instantiate = true)
    {
        GameObject go = prefab;
        if (instantiate)
        {
            go = GameObject.Instantiate(prefab);
        }

        if (parent && go)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = UnityEngine.Vector3.zero;
            t.localRotation = UnityEngine.Quaternion.identity;
            t.localScale = UnityEngine.Vector3.one;
        }

        return go;
    }

    public static void GetSpawnGameObjectCount(GameObject parent, GameObject prefab, List<GameObject> spawnList, int count)
    {
        prefab.SetActive(true);

        if (spawnList == null)
        {
            spawnList = new List<GameObject>();
        }
        
        int nowHaveSpawnCount = spawnList.Count;

        if (nowHaveSpawnCount <= 0)
        {
            spawnList.Add(prefab);
            nowHaveSpawnCount = nowHaveSpawnCount + 1;
        }

        int subNum = nowHaveSpawnCount - count;
        for (int i = 0; i < -subNum; i++)
        {
            spawnList.Add(AddChild(parent, prefab));
        }

        nowHaveSpawnCount = spawnList.Count;
        for (int i = 0; i < nowHaveSpawnCount; i++)
        {
            GameObject spawnGo = spawnList[i];
            spawnGo.SetActive(i < count);
        }
    }
}