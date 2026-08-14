using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TMP_Text scoreText;
    public float score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = player.position.z;
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }
}
