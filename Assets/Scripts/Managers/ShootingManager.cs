using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootingManager : MonoBehaviour
{
    [SerializeField] private ProjectileMode _shootingMode = ProjectileMode.Pistol;
    [Header("Grenade Settings")]
    [SerializeField, Range(.1f, 100f)] private float _grenadeSpeed = 5;
    [SerializeField, Range(.01f, 100f)] private float _grenadeRange = 5;
    [SerializeField, Range(.01f, 100f)] private float _grenadeForce = 25;

    [Header("Bullet Settings")]
    [SerializeField, Range(.1f, 100f)] private float _bulletSpeed = 5;


    public float GrenadeRange => _grenadeRange;
    public List<GameObject> LastTargets => _lastTargets;
    [HideInInspector]
    public bool AbleToShoot = true;


    private GameManager _gameManager;
    private EventManager _eventManager;
    private Camera _camera;
    private List<GameObject> _lastTargets = new List<GameObject>();


    private void Start()
    {
        Initialize();
    }

    public void Shoot(Vector2 mousePos)
    {
        if (!AbleToShoot) return;
        Ray r = _camera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(r, out RaycastHit hit, 100f))
            if (hit.transform.CompareTag(_gameManager.ShootableTag))
                CreateProjectile(_shootingMode, r.direction);
    }

    //-------------------

    private void Initialize()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _eventManager = _gameManager.GameEventManager;
        _camera = Camera.main;
    }
    private void CreateProjectile(ProjectileMode projectileMode, Vector3 direction)
    {
        GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        projectile.transform.position = _camera.transform.position;
        Projectile projectileMeta = projectile.AddComponent<Projectile>();
        projectileMeta._myMode = _shootingMode;
        Rigidbody projectileRB = projectile.AddComponent<Rigidbody>();
        projectileRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
        projectileRB.mass = .01f;

        float speedMuliplier = 1;
        if (projectileMode == ProjectileMode.Pistol)
        {
            speedMuliplier = _bulletSpeed;
            projectile.transform.localScale = Vector3.one * .1f;
        }
        if (projectileMode == ProjectileMode.Grenade)
        {
            speedMuliplier = _grenadeSpeed;
            projectile.transform.localScale = Vector3.one * .25f;
        }

        projectileRB.AddForce(direction * speedMuliplier / 100, ForceMode.Impulse);
    }

    //-------------------

    public void ExecuteShot(GameObject projectile, GameObject other)
    {
        _lastTargets = new List<GameObject>();
        if (_shootingMode == ProjectileMode.Pistol)
        {
            if (other.GetComponent<Rigidbody>() == null)
                _lastTargets.Add(other);
            Rigidbody tempRB = other.GetComponent<Rigidbody>();
            if (tempRB == null) tempRB = other.AddComponent<Rigidbody>();
            tempRB.AddForce((other.transform.position - _camera.transform.position).normalized, ForceMode.VelocityChange);
        }
        if (_shootingMode == ProjectileMode.Grenade)
        {
            Vector3 explosionCenter = Vector3.Lerp(projectile.transform.position, other.transform.position, .5f);
            Collider[] targets = Physics.OverlapSphere(explosionCenter, GrenadeRange);
            foreach (Collider cube in targets)
            {
                if (!cube.gameObject.CompareTag(_gameManager.ShootableTag)) continue;
                Rigidbody cubeRB = cube.GetComponent<Rigidbody>();
                if (cubeRB == null)
                {
                    _lastTargets.Add(cube.gameObject);
                    cubeRB = cube.gameObject.AddComponent<Rigidbody>();
                }
                cubeRB.AddExplosionForce(_grenadeForce * 100, explosionCenter, GrenadeRange);
            }
        }
    }

    public void ChangeShootableState(bool newState)
    {
        AbleToShoot = newState;
    }
}
