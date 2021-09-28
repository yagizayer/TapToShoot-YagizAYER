using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public ProjectileMode _myMode;
    private GameManager _gameManager;
    private EventManager _eventManager;
    private bool collidedOnce = false;

    private void Start()
    {
        Initialize();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(_gameManager.ShootableTag) && !collidedOnce)
        {
            collidedOnce = true;
            _eventManager.InvokeProjectileCollisionEventEvent(gameObject, other.gameObject);
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        Destroy(gameObject, 5);
    }

    //---------------------

    private void Initialize()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _eventManager = _gameManager.GameEventManager;
    }
}
