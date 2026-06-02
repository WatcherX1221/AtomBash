using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

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

    public bool IsPlaying;

    private void Start()
    {
        IsPlaying = false;
        scoreManager = new ScoreManager();
        UpdateScores(0, 0);
        UpdateUI(0);
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

    public void UpdateUI(int currentPage)
    {
        List<GameObject> uiElements = new List<GameObject>();

        uiElements.AddRange(GameObject.FindGameObjectsWithTag("UI"));

        for (int i = 0; i < uiElements.Count; i++)
        {
            if ((int)uiElements[i].GetComponent<UIType>().uiType == currentPage)
            {
                uiElements[i].gameObject.SetActive(true);   
            }
            else
            {
                uiElements[i].gameObject.SetActive(false);
            }
        }
        
    }
}
