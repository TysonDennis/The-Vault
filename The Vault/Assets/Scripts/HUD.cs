using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Image icon;
    //stores the other character's icon
    [SerializeField]
    private Image otherIcon;
    //stores the text for the speech
    [SerializeField]
    private TextMeshProUGUI speech;
    //gets the player script
    [SerializeField]
    private Player player;
    //stores the text that displays if the game is paused
    [SerializeField]
    private TextMeshProUGUI pause;
    //stores the button for deleting saved data
    [SerializeField]
    private GameObject deleteButton;
    //stores Kaitlyn's base sprite
    [SerializeField]
    private Sprite BaseSprite;
    //stores Kaitlyn's hurt sprite
    [SerializeField]
    private Sprite HurtSprite;
    //stores Kaitlyn's killed sprite
    [SerializeField]
    private Sprite KilledSprite;

    //is called at the start to set the sprites and set the pause menu inactive
    private void Awake()
    {
        icon = GetComponentInChildren<Image>();
        pause.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);
        StartCoroutine(ResetSprite());
    }

    //displays Kaitlyn's stats
    private void Update()
    {
        //icon.sprite = BaseSprite;
        HPDisplay.text = "Health: " + kaitlyn.HP + "/" + kaitlyn.maxHP;
        //calls the pause menu
        if(player.IsPaused == true)
        {
            pause.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);
        }
        //closes the pause menu
        else
        {
            pause.gameObject.SetActive(false);
            deleteButton.gameObject.SetActive(false);
        }
    }

    //switches Kaitlyn's sprite to the hurt sprite if she takes damage, and the killed sprite if she dies
    public void hurtSprite()
    {
        //checks if Kaitlyn's health is above 0
        if(kaitlyn.HP > 0)
        {
            icon.sprite = HurtSprite;
            StartCoroutine(ResetSprite());
        }
        else
        {
            icon.sprite = KilledSprite;
            StartCoroutine(ResetSprite());
        }
    }

    //sets Kaitlyn's sprite back to neutral
    private IEnumerator ResetSprite()
    {
        yield return new WaitForSeconds(1f);
        icon.sprite = BaseSprite;
    }
}
