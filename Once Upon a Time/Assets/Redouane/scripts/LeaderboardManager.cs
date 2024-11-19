using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] List<string> usernames;
    List<float> scores;

    [SerializeField] TextMeshProUGUI scoreboardText;

    void Start()
    {
        scores = new List<float>(new float[usernames.Count]);
        for (int i = 0; i < usernames.Count; i++)
        {
            scores[i] = PlayerPrefs.GetFloat(usernames[i]);
            scoreboardText.text += usernames[i] + ": " + scores[i] + "\n";
            
        }
        List<KeyValuePair<string, float>> leaderboard = new List<KeyValuePair<string, float>>();
        for (int i = 0; i < usernames.Count; i++)
        {
            leaderboard.Add(new KeyValuePair<string, float>(usernames[i], scores[i]));
        }

        leaderboard = leaderboard.OrderByDescending(entry => entry.Value).ToList();


        scoreboardText.text = ""; 
        foreach (KeyValuePair<string, float> entry in leaderboard)
        {
            scoreboardText.text += entry.Key + ": " + entry.Value.ToString("N2") + "seconden"+ "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
