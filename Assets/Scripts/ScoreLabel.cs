using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabel : MonoBehaviour
{
    private static int score = 0;
    private static Text label;


    public static int Score { 
        get => score; 
        set { 
            score = value;
            label.text = value.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        label = gameObject.GetComponent<Text>();
        label.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //label.text = score.ToString();
    }
}
