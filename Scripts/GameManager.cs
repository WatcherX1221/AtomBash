using TMPro;
using UnityEngine;

public class ScoreManager
{
    public int ChargeScore;
    public int NeutronScore;
    public int MovesLeft;


}

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;

    public GameObject chargeUI;
    public GameObject neutronUI;

    private void Start()
    {
        scoreManager = new ScoreManager();
        UpdateScores(0, 0);
    }
    public void UpdateScores(int chargeScore, int neutronScore)
    {
        scoreManager.ChargeScore += chargeScore;
        scoreManager.NeutronScore += neutronScore;
        ScoreUIUpdate(scoreManager);
    }

    public void ScoreUIUpdate(ScoreManager score)
    {
        chargeUI.GetComponent<TMP_Text>().text = "Charge: " + score.ChargeScore;
        neutronUI.GetComponent<TMP_Text>().text = "Neutrons: " + score.NeutronScore;
    }
}
