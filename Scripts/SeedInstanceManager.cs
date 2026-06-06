using TMPro;
using UnityEngine;

public class SeedInstanceManager : MonoBehaviour
{
    public void GetSeedScores(SavedScores scores)
    {
        GameObject panel = transform.Find("Panel").gameObject;
        panel.transform.Find("SeedValue").GetComponent<TMP_Text>().text = scores.Seed.ToString();
        panel.transform.Find("ChargeValue").GetComponent<TMP_Text>().text = scores.Charge.ToString();
        panel.transform.Find("NeutronValue").GetComponent<TMP_Text>().text = scores.Neutrons.ToString();
        panel.transform.Find("MoveValue").GetComponent<TMP_Text>().text = scores.MovesLeft.ToString();
        panel.transform.Find("SubatomValue").GetComponent<TMP_Text>().text = scores.Subatoms.ToString();
    }
}
