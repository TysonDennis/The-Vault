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

    //displays Kaitlyn's stats
    private void Update()
    {
        HPDisplay.text = "Health: " + kaitlyn.HP + "/" + kaitlyn.maxHP;
    }
}
