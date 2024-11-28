using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] List<string> usernames;
    List<float> scores;
    int lastgame;
    Dictionary<string,float> keyValuePairs = new Dictionary<string,float>();
    [SerializeField] TextMeshProUGUI scoreboardText;
    private float counter;

    private PointManager PointManager;

    void Start()
    {
        lastgame = PlayerPrefs.GetInt("game");
        switch (lastgame)
        {
            case 1:
                Tag();
                break;
            case 2:
                PointManager.Knikkeren();
                break;

        }

        counter += Time.deltaTime;
        if (counter >= 10)
        {
            SceneManager.LoadScene(0);
        }
    }


    void Tag()
    {
        scores = new List<float>(new float[usernames.Count]);
        for (int i = 0; i < usernames.Count; i++)
        {
            scores[i] = PlayerPrefs.GetFloat(usernames[i]);
            scoreboardText.text += usernames[i] + ": " + scores[i] + "\n";

        }

        
        for (int i = 0; i < usernames.Count; i++)
        {
            keyValuePairs.Add(usernames[i], scores[i]);
        }

        keyValuePairs = keyValuePairs.OrderByDescending(tvalue => tvalue.Value).ToDictionary(x => x.Key, x => x.Value);


        scoreboardText.text = "";
        foreach (KeyValuePair<string, float> entry in keyValuePairs)
        {
            scoreboardText.text += entry.Key + ": " + entry.Value.ToString("N2") + "seconden" + "\n";
        }
    }
}
