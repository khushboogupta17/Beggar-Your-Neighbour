using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Game Event",menuName ="Game Event")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> Listeners { get; } = new List<GameEventListener>();

    public void Raise()
    {
        for(int i = Listeners.Count-1;i>=0 ; i--)
        {
            Listeners[i].OnEventRaised();
        }
    }

    
    public void RegisterListener(GameEventListener listener)
    {
        Listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventListener listener)
    {
        Listeners.Remove(listener);
    }

    
}
