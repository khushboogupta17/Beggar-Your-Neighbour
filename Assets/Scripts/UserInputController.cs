using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputController : MonoBehaviour
{
    [SerializeField]
    private GameEvent gameEvent;
    [SerializeField]
    private ParticleSystem tapEffect;

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            gameEvent.Raise();
            tapEffect.transform.position = Input.GetTouch(0).position;
            tapEffect.Play();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameEvent.Raise();
            tapEffect.transform.position = new Vector3(newPos.x,newPos.y,2f);

            tapEffect.Play();

        }
    }

}
