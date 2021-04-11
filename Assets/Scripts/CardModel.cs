using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


public class CardModel : MonoBehaviour
{
    [SerializeField]
    private string cardName;
    [SerializeField]
    private Sprite front_Sprite;
    [SerializeField]
    private Sprite back_Sprite;
    [SerializeField]
    private int _chances;

    public int Chances
    {
        get
        {
            return _chances;
        }
    }
    

    public void InitializeValues(string _name,Sprite _frontsprite,Sprite _backSprite,int _chances)
    {
        cardName = _name;
        front_Sprite = _frontsprite;
        back_Sprite = _backSprite;
        this._chances = _chances;

        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = front_Sprite;
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = back_Sprite;
        transform.name = cardName;
    }
  

    public void PlayCard(UnityAction callback)
    {
        Sequence mySeq = DOTween.Sequence();
        mySeq.Append(transform.DOLocalMoveY(0f, 1f));
        mySeq.Append(transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f));
        mySeq.AppendCallback(delegate { callback.Invoke(); });
       // Debug.Log("card played: " + cardName);
        
    }

    public void StackCard(UnityAction callback=null)
    {
        Sequence mySeq = DOTween.Sequence();
        mySeq.Append(transform.DOLocalMoveY(0f, 1f));
        mySeq.Append(transform.DORotateQuaternion(Quaternion.Euler(0f, 180f, 0f), 0.5f));
        if(callback!=null)
        mySeq.AppendCallback(delegate { callback.Invoke(); });
       
       // Debug.Log("card stacked: " + cardName);
   
    }
}
