
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Rank_Insert : MonoBehaviour
{

    public Text Rank;
    public Text Name;
    public Text Score;

    string[] Ranking = { "1st", "2nd", "3rd", "4th", "5th",
        "6th", "7th", "8th", "9th", "10th" };

    // Use this for initialization
    void Start()
    {

    }
    public void Rankin(int rank, string name, string score)
    {
        Rank.text = Ranking[rank].ToString();
        Name.text = name.ToString();
        Score.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
