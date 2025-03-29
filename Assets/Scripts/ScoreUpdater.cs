using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    [Header("Per scene")]
    public DirtController dirtController;

    [Header("Global")]
    public TMPro.TextMeshProUGUI scoreText;
    public string beforeText = "Remaining dirt: ";
    public string afterText = " %";

    void Update()
    {
        scoreText.text = beforeText + (int)((float)dirtController.dirtCount / (float)dirtController.startingDirtCount * 100f) + afterText;
    }
}
