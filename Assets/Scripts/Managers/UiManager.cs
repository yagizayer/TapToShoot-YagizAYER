using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Text ScoreCounter;
    [SerializeField] private Text TargetScoreCounter;

    private GameManager _gameManager;
    private EventManager _eventManager;
    private void Start()
    {
        Initialize();
    }

    //----------------
    private void Initialize()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _eventManager = _gameManager.GameEventManager;

        TargetScoreCounter.text = _gameManager.TargetScore.ToString();
    }
    public void UpdateScoreUiElement() => ScoreCounter.text = _gameManager.CurrentScore.ToString();
}
