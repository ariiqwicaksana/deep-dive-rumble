using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int coinCount;
    public int pieceCount;
    public int scrollCount;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI pieceText;
    public TextMeshProUGUI scrollText;
    public Image healthBar;
    public float healthAmount = 100f;

    public GameObject coinPrefab;
    public GameObject piecePrefab;
    public GameObject scrollPrefab;
    public float spawnRadius = 1f;

    public GameObject pausePanel;
    public GameObject losePanel; // Reference to the Lose Panel UI
    private bool isPaused = false;
    public string sceneName;

    private bool isGameOver = false; // Prevent multiple triggers of game-over

    [System.Serializable]
    public class Spawner
    {
        public GameObject objectToSpawn;
        public Transform spawnPoint;
        public float spawnDelay;
    }

    public List<Spawner> spawners;

    // Timer Variables
    private float survivalTime = 0f; // Timer for survival duration
    public TextMeshProUGUI timerText; // Reference to the timer TextMeshProUGUI

    void Start()
    {
        StartCoroutine(SpawnItem(coinPrefab, 20f));
        StartCoroutine(SpawnItem(piecePrefab, 30f));
        StartCoroutine(SpawnItem(scrollPrefab, 40f));

        foreach (Spawner spawner in spawners) 
        {
            StartCoroutine(SpawnObject(spawner));
        }
    }

    void Update()
    {
        // Update UI for coin, piece, and scroll count
        coinText.text = coinCount.ToString();
        pieceText.text = pieceCount.ToString();
        scrollText.text = scrollCount.ToString();

        // Update timer if the game is not over
        if (!isGameOver)
        {
            UpdateSurvivalTimer();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        CheckGameOver(); // Continuously check if health has reached 0
    }

    void UpdateSurvivalTimer()
    {
        survivalTime += Time.deltaTime; // Add delta time to the survival timer
        int minutes = Mathf.FloorToInt(survivalTime / 60); // Calculate minutes
        int seconds = Mathf.FloorToInt(survivalTime % 60); // Calculate seconds

        // Format and display the timer
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }

    IEnumerator SpawnItem(GameObject itemPrefab, float interval)
    {
        while (true)
        {
            Vector3 randomPos = Random.insideUnitCircle * spawnRadius;
            Instantiate(itemPrefab, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, spawnRadius);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void CheckGameOver()
    {
        if (healthAmount <= 0 && !isGameOver)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0; // Stop the game
        losePanel.SetActive(true); // Show the lose panel
    }

    IEnumerator SpawnObject(Spawner spawner)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawner.spawnDelay);

            Instantiate(spawner.objectToSpawn, spawner.spawnPoint.position, spawner.spawnPoint.rotation);
        }
    }
}
