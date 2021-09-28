using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> PlayerInputEvent = new UnityEvent<Vector2>();
    [SerializeField] private UnityEvent<GameObject, GameObject> ProjectileCollisionEvent = new UnityEvent<GameObject, GameObject>();
    [SerializeField] private UnityEvent GameEnded = new UnityEvent();


    public void InvokePlayerInputEvent(Vector2 inputPos) => PlayerInputEvent.Invoke(inputPos);
    public void InvokeProjectileCollisionEventEvent(GameObject me, GameObject other) => ProjectileCollisionEvent.Invoke(me, other);
    public void InvokeGameEndedEvent() => GameEnded.Invoke();

    //----------------------

    public void TestFunc(string message)
    {
        Debug.Log(message);
    }
}
