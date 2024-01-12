using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class AbilityInfo : MonoBehaviour
{
    [SerializeField] private GameObject ConvictionIcon;
    [SerializeField] private GameObject ConvictionInfo;
    [SerializeField] private GameObject InvisibilityIcon;
    [SerializeField] private GameObject InvisibilityInfo;
    [SerializeField] private GameObject SuperpowerIcon;
    [SerializeField] private GameObject SuperpowerInfo;
    // Start is called before the first frame update
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.canUseConviction)
            ActivateInfo(ConvictionIcon,ConvictionInfo);
        if (gameManager.canUseInvisibility)
            ActivateInfo(InvisibilityIcon,InvisibilityInfo);
        if (gameManager.canUseSuperpower)
            ActivateInfo(SuperpowerIcon,SuperpowerInfo);
    }

    private void ActivateInfo(GameObject ability, GameObject info)
    {
        var mousePos = Input.mousePosition;
        if (mousePos.x >= ability.transform.position.x - 50 && mousePos.x <= ability.transform.position.x - 50 + 100 && mousePos.y >= ability.transform.position.y - 50 && mousePos.y <= ability.transform.position.y - 50 + 100)
        {
            info.SetActive(true);
        }
        else
            info.SetActive(false);
    }
}
