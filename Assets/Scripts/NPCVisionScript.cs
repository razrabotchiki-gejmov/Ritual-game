using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCVisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject detectionRatingScale;
    public GameObject detectionRatingText;
    public GameObject NPC;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player.GetComponent<InteractWithNPC>().haveWeapon &&
            !NPC.GetComponent<NPCState>().isDead && !player.GetComponent<InteractWithNPC>().isInvisible)
        {
            IncreaseDetectionRating(20);
        }
    }

    public void IncreaseDetectionRating(int value)
    {
        var currentValue = int.Parse(detectionRatingText.GetComponent<TextMeshProUGUI>().text);
        var newValue = currentValue + value;
        if (newValue > 100)
        {
            newValue = 100;
        }

        detectionRatingText.GetComponent<TextMeshProUGUI>().text = newValue.ToString();
        detectionRatingScale.GetComponent<RectTransform>().sizeDelta = new Vector2(newValue * 4, 20);
    }
}