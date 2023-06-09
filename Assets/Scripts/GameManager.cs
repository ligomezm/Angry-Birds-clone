using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private Canvas selectPlayerCanvas;
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private Slingshot slingshot;

    private float delay;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject birdPrefab;

    private void Awake()
    {
        UICanvas.enabled = false;
        gameOverCanvas.enabled = false;
        winCanvas.enabled = false;
        delay = 3f;
    }

    private void Start()
    {
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemiesInScene)
        {
            enemies.Add(enemy);
        }
    }

    private void FixedUpdate()
    {
        WinGame();
        GameOver();
    }

    public void UpdateEnemyList(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public void StartGame()
    {
        if (characterManager.birdPrefab != null)
        { 
            selectPlayerCanvas.enabled = false;
            UICanvas.enabled = true;
            birdPrefab = characterManager.birdPrefab;
            slingshot.CreateBird();
        }
    }

    public void WinGame()
    {
        if (enemies.Count == 0)
        {
            //AudioManager.Instance.PlaySFX("GameCompleted");
            Invoke("ActivateWinCanvas", delay);
        }
    }

    public void GameOver()
    {
        if (slingshot.lives < 0 && enemies.Count > 0)
        {
            //AudioManager.Instance.PlaySFX("GameOver");
            Invoke("ActivateGameOverCanvas", delay);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ActivateGameOverCanvas()
    {
        gameOverCanvas.enabled = true;
    }

    void ActivateWinCanvas()
    {
        winCanvas.enabled = true;
    }
}
