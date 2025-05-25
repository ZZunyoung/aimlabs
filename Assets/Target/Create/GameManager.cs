using UnityEngine;
using UnityEngine.UI; // UI 표시용 (선택)
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text scoreText;
    public int score = 0;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"[점수] 현재 점수: {score}");

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
