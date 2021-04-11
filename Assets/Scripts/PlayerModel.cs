using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Player Model",menuName ="Player Model")]
public class PlayerModel : ScriptableObject
{
    [SerializeField]
    public string playerName;
    [SerializeField]
    public int playerCount = 1;

    Deque<GameObject> deQueue = new Deque<GameObject>() ;

    public int dequeueCount
    {
        get
        {
            return deQueue.Count;
        }
    }

    public GameObject Deque_Front()
    {
        if (deQueue.Count > 0)
            return deQueue.PopFirst();
        else return null;
    }

    public GameObject Deque_last()
    {
        if (deQueue.Count > 0)
            return deQueue.PopLast();
        else return null;
    }


    public void Enqueue_Front(GameObject key)
    {

        deQueue.AddFirst(key);
    }

    public void Enque_Back(GameObject key)
    {
        deQueue.AddLast(key);
       
    }

  
  
   
}
