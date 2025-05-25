using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // 싱글톤 패턴

    public TMP_Text scoreText;  // UI 텍스트 연결
    private int score = 0;

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
