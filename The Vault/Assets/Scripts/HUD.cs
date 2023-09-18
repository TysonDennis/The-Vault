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
    public TextMeshProUGUI speech;
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
    //holds the oxygen display
    [SerializeField]
    private TextMeshProUGUI O2Display;
    //gets the aquatic script
    [SerializeField]
    private Aquatic aquatic;
    //stores the icon for Kaitlyn's ability
    public Image abilityIcon;
    //stores the sprite for Water Gun
    public Sprite WaterSprite;
    //stores the sprite for Lightningbolt
    public Sprite LightningSprite;
    //stores the sprite for Stretch Arm
    public Sprite StretchSprite;
    //stores the sprite for Frost Breath
    public Sprite FrostSprite;
    //stores the sprite for Flamethrower
    public Sprite FlameSprite;

    //is called at the start to set the sprites and set the pause menu inactive
    private void Awake()
    {
        //makes it universal
        DontDestroyOnLoad(this.gameObject);
        icon = GetComponentInChildren<Image>();
        pause.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);
        StartCoroutine(ResetSprite());
        O2Display.gameObject.SetActive(false);
    }

    //displays Kaitlyn's stats
    private void Update()
    {
        //icon.sprite = BaseSprite;
        HPDisplay.text = "Health: " + kaitlyn.HP + "/" + kaitlyn.maxHP;
        O2Display.text = "O2: " + aquatic.oxygen + "/" + aquatic.maxO2;
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
        //displays oxygen meter when Kaitlyn's underwater, or if her oxygen meter isn't full
        if(aquatic.isSubmerged == true)
        {
            O2Display.gameObject.SetActive(true);
        }
        else if(aquatic.oxygen < aquatic.maxO2)
        {
            O2Display.gameObject.SetActive(true);
        }
        else
        {
            O2Display.gameObject.SetActive(false);
        }
        //changes the sprite for Kaitlyn's selected ability
        if(player.AbilityNumber == 0)
        {
            if(kaitlyn.WaterGun > 0)
            {
                abilityIcon.sprite = WaterSprite;
            }
        }
        else if(player.AbilityNumber == 1)
        {
            if(kaitlyn.Lightningbolt > 0)
            {
                abilityIcon.sprite = LightningSprite;
            }
        }
        else if (player.AbilityNumber == 2)
        {
            if (kaitlyn.StretchArm > 0)
            {
                abilityIcon.sprite = StretchSprite;
            }
        }
        else if (player.AbilityNumber == 3)
        {
            if (kaitlyn.FrostBreath > 0)
            {
                abilityIcon.sprite = FrostSprite;
            }
        }
        else
        {
            if(kaitlyn.Flamethrower > 0)
            {
                abilityIcon.sprite = FlameSprite;
            }
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

    //shows the text to the player
    public void Speak(string text)
    {
        speech.text = text;
    }

    //clears the text
    public void Clear(string clearText)
    {
        speech.text = null;
    }
}
