using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ObjectPl
{
    public List<GameObject> prefab = new List<GameObject>();

    public List<int> prefabcount = new List<int>();
}
public class ObjectPool : MonoSingleton<ObjectPool>
{
    [SerializeField]
    ObjectPl objectPoolData;

    Dictionary<PoolObjectType, Queue<GameObject>> poolObjectMap = new Dictionary<PoolObjectType, Queue<GameObject>>();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < objectPoolData.prefab.Count; i++)
        {
            poolObjectMap.Add((PoolObjectType)i, new Queue<GameObject>());

            for (int j = 0; j < objectPoolData.prefabcount[i]; j++)
            {
                poolObjectMap[(PoolObjectType)i].Enqueue(CreateNewObject(i));
            }
        }
    }

    private GameObject CreateNewObject(int index)
    {
        var newObj = Instantiate(objectPoolData.prefab[index]);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public GameObject SetObject(PoolObjectType type)
    {
        if (Instance.poolObjectMap[type].Count > 0)
        {
            var obj = Instance.poolObjectMap[type].Dequeue();
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject((int)type);
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);

            return newObj;
        }
    }

    public GameObject GetObject(PoolObjectType type)
    {
        if (Instance.poolObjectMap[type].Count > 0)
        {
            var obj = Instance.poolObjectMap[type].Dequeue();
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject((int)type);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(transform);

            return newObj;
        }
    }

    public void ReturnObject(PoolObjectType type, GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolObjectMap[type].Enqueue(obj);
    }
}
