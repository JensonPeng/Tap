using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static TargetPooler instance;
    private void Awake()
    {
        instance = this;
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> targetPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                targetPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, targetPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position - Vector3.up * 0.635f - Vector3.forward;

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

}
