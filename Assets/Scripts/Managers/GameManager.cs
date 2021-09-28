using UnityEngine;
using Helper;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Other Variables")]
    [SerializeField] private Functions _functions;
    [SerializeField] private string _shootableObjectsTag;

    public EventManager GameEventManager => _eventManager;
    public ShootingManager GameShootingManager => _shootingManager;
    public string ShootableTag => _shootableObjectsTag;
    public int CurrentScore => _currentScore;

    public int TargetScore { get => _targetScore; }

    private EventManager _eventManager;
    private ShootingManager _shootingManager;
    private int _currentScore = 0;
    private int _targetScore = 15;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        // there is no dragging, no need for touch event
        if (Input.GetMouseButtonDown(0))
            _eventManager.InvokePlayerInputEvent(Input.mousePosition);
    }


    //-----------------------

    private void Initialize()
    {
        _eventManager = FindObjectOfType<EventManager>();
        _shootingManager = FindObjectOfType<ShootingManager>();
        _targetScore = FindObjectOfType<CreateWall>().WallSize;
    }
    public void IncreaseScore()
    {
        _currentScore += GameShootingManager.LastTargets.Count;
        if (_currentScore >= _targetScore)
            _eventManager.InvokeGameEndedEvent();
    }

    //----------------------------
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
