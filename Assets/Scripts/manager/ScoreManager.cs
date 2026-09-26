using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TMP_Text scoreText;
    public static float score;

    void Update()
    {
        score = player.position.z;
        scoreText.text = $"Score: {Mathf.FloorToInt(score)}";
    }
}