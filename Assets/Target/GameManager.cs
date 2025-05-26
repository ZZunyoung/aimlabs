using UnityEngine;
using TMPro; // TextMeshPro ��� ��
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameoverText;
    public Text timeText;
    public Text recordText;

    private bool isGameover;
    private int score = 0;
    private float time;
    public TextMeshProUGUI scoreText; // UI �����

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"���� ����: ���� ���� = {score}");
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    void Start()
    {
        isGameover = false;
        time = 30;
    }

    void Update()
    {
        if (!isGameover)
        {
            time -= Time.deltaTime;
            timeText.text = "Time : " + (int)time;
            if ((int)time == 0)
            {
                isGameover = true;
                gameoverText.SetActive(true);
                float bestRecord = PlayerPrefs.GetFloat("BestRecord");
                if (score > bestRecord)
                {
                    bestRecord = score;
                    PlayerPrefs.SetFloat("BestRecord", bestRecord);
                }
                recordText.text = "Best Record : " + (int)bestRecord;
            }
        }
        else
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("mapExampeScene");
            }
        }
    }
}
