using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject detectionRatingScale;
    public TextMeshProUGUI detectionRatingText;
    public int day;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseDetectionRating(int value)
    {
        var currentValue = int.Parse(detectionRatingText.text);
        var newValue = currentValue + value;
        if (newValue <= 33)
        {
            detectionRatingScale.GetComponent<Image>().color = Color.green;
        }
        else if (newValue <= 66)
        {
            detectionRatingScale.GetComponent<Image>().color = Color.yellow;
        }
        else if (newValue <=99)
        {
            detectionRatingScale.GetComponent<Image>().color = new Color(1, 0.4f, 0);
        }
        else
        {
            detectionRatingScale.GetComponent<Image>().color = Color.red;
            newValue = 100;
        }

        detectionRatingText.text = newValue.ToString();
        detectionRatingScale.GetComponent<RectTransform>().sizeDelta = new Vector2(newValue * 4, 20);
    }
}
