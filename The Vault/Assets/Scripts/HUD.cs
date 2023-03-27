using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    //gets the HP display
    [SerializeField]
    private TextMeshProUGUI HPDisplay;
    //gets the scriptable object that holds Kaitlyn's stats
    [SerializeField]
    private KaitlynSO kaitlyn;
    //stores Kaitlyn's icon
    [SerializeField]
    private GameObject icon;
    //stores the other character's icon
    [SerializeField]
    private GameObject otherIcon;
    //stores the text for the speech
    [SerializeField]
    private TextMeshProUGUI speech;
    //gets the player script
    [SerializeField]
    private Player player;
    //stores the text that displays if the game is paused
    [SerializeField]
    private TextMeshProUGUI pause;

    //is called at the start
    private void Awake()
    {
        pause.gameObject.SetActive(false);
    }

    //displays Kaitlyn's stats
    private void Update()
    {
        HPDisplay.text = "Health: " + kaitlyn.HP + "/" + kaitlyn.maxHP;
        if(player.IsPaused == true)
        {
            pause.gameObject.SetActive(true);
        }
        else
        {
            pause.gameObject.SetActive(false);
        }
    }
}
