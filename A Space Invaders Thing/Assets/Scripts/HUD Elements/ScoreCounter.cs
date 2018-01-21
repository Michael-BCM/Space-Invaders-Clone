using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public GameObject GameManager;
    public TextMesh _TextMesh;

    void Awake() { _TextMesh = GetComponent<TextMesh>(); }

    void Update ()
    {
        int score = GameManager.GetComponent<GameLoop>().score;
       _TextMesh.text = "Score: " + score.ToString("D5") + "000";
    }
}
