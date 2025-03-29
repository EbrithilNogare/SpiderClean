using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    public DirtController dirtController;

    void Update()
    {
        scoreText.text = (int)(100 - (float)dirtController.dirtCount / (float)dirtController.startingDirtCount * 100f) + "%";
    }
}
