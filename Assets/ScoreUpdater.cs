using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    public DirtController dirtController;
    public string beforeText = "Remaining dirt: ";
    public string afterText = " %";

    void Update()
    {
        scoreText.text = beforeText + (int)((float)dirtController.dirtCount / (float)dirtController.startingDirtCount * 100f) + afterText;
    }
}
