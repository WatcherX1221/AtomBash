using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ScoreManager
{
    public int ChargeScore;
    public int NeutronScore;
    public int MovesLeft;
    public int SubatomsLeft;
}

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;

    public GameObject chargeUI;
    public GameObject neutronUI;
    public GameObject moveUI;
    public GameObject subatomUI;

    public bool IsPlaying;

    List<GameObject> uiPanels = new List<GameObject>();

    private void Start()
    {
        uiPanels.AddRange(GameObject.FindGameObjectsWithTag("UI"));

        IsPlaying = false;
        scoreManager = new ScoreManager();
        UpdateScores(0, 0, 0, true);
        UpdateUI(0);
    }
    public void UpdateScores(int chargeScore, int neutronScore, int subatomsScore, bool initialUpdate)
    {
        scoreManager.ChargeScore += chargeScore;
        scoreManager.NeutronScore += neutronScore;
        scoreManager.SubatomsLeft += subatomsScore;
        if (!initialUpdate)
        {
            GetComponent<ScoresHandler>().levelScore.Charge = scoreManager.ChargeScore;
            GetComponent<ScoresHandler>().levelScore.Neutrons = scoreManager.NeutronScore;
            GetComponent<ScoresHandler>().levelScore.Subatoms = scoreManager.SubatomsLeft;
        }
        
        ScoreUIUpdate(scoreManager);

        if (scoreManager.SubatomsLeft <= 0 && !initialUpdate)
        {
            EndGame();
        }
    }

    public void ScoreUIUpdate(ScoreManager score)
    {
        chargeUI.GetComponent<TMP_Text>().text = "Charge: " + score.ChargeScore;
        neutronUI.GetComponent<TMP_Text>().text = "Neutrons: " + score.NeutronScore;
        moveUI.GetComponent<TMP_Text>().text = "Moves: " + score.MovesLeft;
        subatomUI.GetComponent<TMP_Text>().text = "Subatoms: " + score.SubatomsLeft;
    }

    public void UpdateUI(int currentPage)
    {       
        switch(currentPage)
        {
            // Generate
            case 0:
                foreach (GameObject go in uiPanels)
                {
                    if (go.name == "GeneratePanel")
                    {
                        go.SetActive(true);
                    }
                    else
                    {
                        go.SetActive(false);
                    }
                }
                break;
            // Level Select
            case 1:
                foreach (GameObject go in uiPanels)
                {
                    if (go.name == "ScoresPanel")
                    {
                        go.SetActive(true);
                    }
                    else
                    {
                        go.SetActive(false);
                    }
                }
                break;
            // Gameplay
            case 2:
                foreach (GameObject go in uiPanels)
                {
                    if (go.name == "GameplayPanel")
                    {
                        go.SetActive(true);
                    }
                    else
                    {
                        go.SetActive(false);
                    }
                }
                break;
            case 3:
                foreach (GameObject go in uiPanels)
                {
                    if (go.name == "GameEndPanel")
                    {
                        go.SetActive(true);
                    }
                    else
                    {
                        go.SetActive(false);
                    }
                }
                break;
        }
        
    }
    public void EndGame()
    {
        IsPlaying = false;
        Debug.Log(GetComponent<ScoresHandler>().levelScore);
        GetComponent<ScoresHandler>().SaveSeed();
        UpdateUI(3);
    }
}
