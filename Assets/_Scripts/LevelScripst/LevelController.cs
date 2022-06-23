using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public int carrentRoomIndex = -1;
    public static int _coins;

    public List<GameObject> roomList;


    [SerializeField] public bool gameStarted;

    [SerializeField] private int _getHitCoins;
    [SerializeField] private int _takeHitCoins;

    [SerializeField] private float _loadingTime;
    

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _nextLevelDoor;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loadingWindow;
    [SerializeField] private GameObject _gameOverWindow;
    //[SerializeField] private GameObject _coinsPanel;

    [SerializeField] private Transform _startPosition;

    [SerializeField] private Text _coinsInfo;
    [SerializeField] private Text _coinsScoreResult;
    [SerializeField] private Button _restartButton;


    private void OnEnable()
    {
        PlayerController.playerGetHit += GetHitScore;
        PlayerController.playerDie += GameOverWindow;
        enemyHeathSys.enemyGetHit += TakeHitScore;
        PlayerEnemyScaner.enemyDetection += RoomComplate;
        MenuController.startGame += RoomSearch;
        nextLevel.nextRoomEnter += RoomSearch;
        nextLevel.winLevel += ShowScoreCoins;
    }


    private void OnDisable()
    {
        PlayerController.playerGetHit -= GetHitScore;
        PlayerController.playerDie -= GameOverWindow;
        enemyHeathSys.enemyGetHit -= TakeHitScore;
        PlayerEnemyScaner.enemyDetection -= RoomComplate;
        MenuController.startGame -= RoomSearch;
        nextLevel.nextRoomEnter -= RoomSearch;
        nextLevel.winLevel -= ShowScoreCoins;
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        else Destroy(gameObject);
    }


    void Start()
    {
        _restartButton.onClick.AddListener(RoomSearch);
        _restartButton.onClick.AddListener(Restart);
    }


    private void Hide(GameObject window)
    {
        window.SetActive(false);
    }


    private void Show(GameObject window)
    {
        window.SetActive(true);
    }


    private void RoomSearch()
    {
        Hide(_nextLevelDoor);
        StartCoroutine(Loading());
    }


    private void RoomComplate()
    {
        if (PlayerEnemyScaner.instance.enemyDetected == false)
        {
            Show(_nextLevelDoor);
        }

        else Hide(_nextLevelDoor);
    }


    private void Restart()
    {
        Destroy(GameObject.FindGameObjectWithTag("Room"));
    }


    private void GameOverWindow()
    {
        carrentRoomIndex = -1;
        gameStarted = false;
        Show(_gameOverWindow);
    }


    private void PlayerInstantiate()
    {
        Instantiate(_player, _startPosition.position, Quaternion.identity);
    }


    private void GetHitScore()
    {
        _coins -= _getHitCoins;
        _coinsInfo.text = $"{_coins}";
    }


    private void TakeHitScore()
    {
        _coins += _takeHitCoins;
        _coinsInfo.text = $"{_coins}";
    }


    private void ShowScoreCoins()
    {
        _coinsScoreResult.text = $"{_coins}";
    }


    IEnumerator Loading()
    {
        yield return new WaitForSeconds(0);
        Hide(_gameOverWindow);
        Show(_loadingWindow);

        yield return new WaitForSeconds(_loadingTime);
        Hide(_loadingWindow);

        if (gameStarted == true) //видаляєм кімнату
        {
            Destroy(GameObject.FindGameObjectWithTag("Room"));
            roomList.Remove(roomList[carrentRoomIndex]);
        }

        else // Старт гри
        {
            _coins = 0;
            gameStarted = true;
            PlayerInstantiate();
        }
        

        if (carrentRoomIndex != roomList.Count) //наступна кімната
        {
            carrentRoomIndex += 1;
            roomList = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms"));
            Instantiate(roomList[carrentRoomIndex], new Vector3(0, 0, 0), Quaternion.identity);
        }

        else
        {
            carrentRoomIndex = -1;
            gameStarted = false;
            Show(_winWindow);
        }

        Show(_nextLevelDoor);

        StopAllCoroutines();
    }
}
