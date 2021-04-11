using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
       
        public string name;
        public Sprite front_Sprite;
        public Sprite back_Sprite;
        public int chances;
       

     };

  
    [SerializeField]
    private GameEvent gameEvent;
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private List<Pool> pools = new List<Pool>();
    

    private  Queue<GameObject> poolQueue;
    public int poolCount {
        get
        {
            return pools.Count;
        }
    }
    public static CardPool instance;

    private void Awake()
    {
        instance = this;
        ShuffleList(pools);
        poolQueue = new Queue<GameObject>();
        foreach (Pool pool in pools)
        {
            GameObject obj = InstantiatePrefab(pool);

            obj.SetActive(false);
           
            poolQueue.Enqueue(obj);
        }

        Debug.Log("Queue count " + poolQueue.Count);
        gameEvent.Raise();
    }
    void ShuffleList(List<Pool> list)
    {
        System.Random rnd = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            Pool value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

       
    }

    
    public GameObject SpawnFromPool(Transform parent)
    {

        GameObject objectToSpawn = poolQueue.Dequeue();
        objectToSpawn.SetActive(true);

        objectToSpawn.transform.parent = parent;
        objectToSpawn.transform.localPosition = Vector3.zero;
        objectToSpawn.transform.rotation = Quaternion.Euler(0f,180f,0f);

 
        poolQueue.Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    GameObject InstantiatePrefab(Pool pool)
    {
        GameObject obj = Instantiate(cardPrefab);
        obj.GetComponent<CardModel>().InitializeValues(pool.name, pool.front_Sprite, pool.back_Sprite, pool.chances);
        return obj;
    }
}
