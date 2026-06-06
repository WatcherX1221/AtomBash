using NUnit.Framework;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class ScoresHandler : MonoBehaviour
{
    public SavedScores[] LoadedScores = new SavedScores[0];
    public SavedScores levelScore;
    public int ScoreCount = 0;
    public bool hasLoadedScoresThisSession;
    public bool needsLoadScore;

    public GameObject ScrollObject;
    public GameObject SeedScoreInstance;

    void Start()
    {
        hasLoadedScoresThisSession = false;
        needsLoadScore = true;
    }
    public void LoadScores()
    {

        string dataPath = System.IO.Path.Combine(Application.dataPath, "saves");


        try
        {
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
                Debug.Log("Created Directory at:" + dataPath);
            }

        }
        catch (System.Exception ex)
        {
            string ErrorMessages = "Failed to Create folder" + ex.Message;
            Debug.LogError(ErrorMessages);
        }

        if (needsLoadScore)
        {
            if (!hasLoadedScoresThisSession)
            {
                ReadFiles(dataPath);
                hasLoadedScoresThisSession = true;
            }
            if (hasLoadedScoresThisSession)
            {
                LoadedScores = new SavedScores[0];
                ReadFiles(dataPath);
                hasLoadedScoresThisSession = true;
            }
            if (LoadedScores.Length > 0)
            {
                for (int i = 0; i < LoadedScores.Length; i++)
                {
                    GameObject newSeedInstance = Instantiate(SeedScoreInstance, ScrollObject.transform);
                    newSeedInstance.GetComponent<SeedInstanceManager>().GetSeedScores(LoadedScores[i]);
                }
                Debug.Log("Rect Size: " + ScrollObject.GetComponent<RectTransform>().rect);
            }
            needsLoadScore = false;
        }

    }

    public void SaveSeed()
    {

        string dataPath = System.IO.Path.Combine(Application.dataPath, "saves");
        Debug.Log("Seed: " + levelScore.Seed + "Charge: " + levelScore.Charge + "Subatoms: " + levelScore.Subatoms + "Neutrons: " + levelScore.Neutrons + "Moves: " + levelScore.MovesLeft);
        try
        {
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
                Debug.Log("Created Directory at:" + dataPath);
            }
            dataPath += "/" + levelScore.Seed + ".json";

            try
            {
                string saveData = JsonUtility.ToJson(levelScore);
                System.IO.File.WriteAllText(dataPath, saveData);
            }
            catch (System.Exception ex)
            {
                string ErrorMessages = "Failed to Write" + ex.Message;
                Debug.LogError(ErrorMessages);
            }
        }
        finally
        {

        }
        needsLoadScore = true;
    }

    public void ReadFiles(string path)
    {
        string[] files = System.IO.Directory.GetFiles(path);
        LoadedScores = new SavedScores[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            string fileData = System.IO.File.ReadAllText(files[i]);
            LoadedScores[i] = JsonUtility.FromJson<SavedScores>(fileData);
        }
    }

    public void DeleteSaveData()
    {
        string dataPath = System.IO.Path.Combine(Application.dataPath, "saves");
        System.IO.Directory.Delete(dataPath);
    }
}
