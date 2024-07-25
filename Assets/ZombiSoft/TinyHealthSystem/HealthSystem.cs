//==============================================================
// HealthSystem
// HealthSystem.Instance.TakeDamage (float Damage);
// HealthSystem.Instance.HealDamage (float Heal);
// HealthSystem.Instance.UseMana (float Mana);
// HealthSystem.Instance.RestoreMana (float Mana);
// Attach to the Hero.
//==============================================================

using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour, IUpgradeable
{
	public static HealthSystem Instance;

    [SerializeField] private GameObject regen1;
    [SerializeField] private GameObject regen2;
    [SerializeField] private GameObject regen3;

    public Image currentHealthBar;
	public Image currentHealthGlobe;
	public PlayerUpgradesManager playerUpgradesManager;
	public Text healthText;
	public float hitPoint = 100f;
	public float maxHitPoint = 100f;

	public Image currentManaBar;
	public Image currentManaGlobe;
	public Text manaText;
	public float exp = 0f;
	public float maxExp = 100f;
	private float playerLevel = 1;

	private bool upgradeIsRegen =true;
	private const string UPDRAGE_MAX_HEALTH = "Increase maximum health by ";
	private const string UPDRAGE_HEAlTH_REGEN = "Increase health regeneration by 0.5 hitpoints per second";
    private const int PERCENT_EXP_INCREASE_PER_LEVEL = 10;
    private const string UPGRADE_TITLE = "Health";
	private int upgradeHealthPercentage = 10;
	private int regenUpgradeLevel = 1;


    //==============================================================
    // Regenerate Health & Mana
    //==============================================================
    public bool Regenerate = true;
	public float regen = 0.1f;
	private float timeleft = 0.0f;	// Left time for current interval
	public float regenUpdateInterval = 1f;

	public bool GodMode;

    //==============================================================
    // Awake
    //==============================================================
    void Awake()
	{
		Instance = this;
	}
	
	//==============================================================
	// Awake
	//==============================================================
  	void Start()
	{
		UpdateGraphics();
		timeleft = regenUpdateInterval; 
	}

	//==============================================================
	// Update
	//==============================================================
	void Update ()
	{
		if (Regenerate)
			Regen();
	}

	//==============================================================
	// Regenerate Health & Mana
	//==============================================================
	private void Regen()
	{
		timeleft -= Time.deltaTime;

		if (timeleft <= 0.0) // Interval ended - update health & mana and start new interval
		{
			// Debug mode
			if (GodMode)
			{
				HealDamage(maxHitPoint);
				//GainExp(maxManaPoint);
			}
			else
			{
				HealDamage(regen);
				//GainExp(regen);				
			}

			UpdateGraphics();

			timeleft = regenUpdateInterval;
		}
	}

	//==============================================================
	// Health Logic
	//==============================================================
	private void UpdateHealthBar()
	{
		float ratio = hitPoint / maxHitPoint;
		currentHealthBar.rectTransform.localPosition = new Vector3(currentHealthBar.rectTransform.rect.width * ratio - currentHealthBar.rectTransform.rect.width, 0, 0);
		healthText.text = hitPoint.ToString ("0") + "/" + maxHitPoint.ToString ("0");
	}

	private void UpdateHealthGlobe()
	{
		float ratio = hitPoint / maxHitPoint;
		currentHealthGlobe.rectTransform.localPosition = new Vector3(0, currentHealthGlobe.rectTransform.rect.height * ratio - currentHealthGlobe.rectTransform.rect.height, 0);
		healthText.text = hitPoint.ToString("0") + "/" + maxHitPoint.ToString("0");
	}

	public void TakeDamage(float Damage)
	{
		hitPoint -= Damage;
		if (hitPoint < 1)
			hitPoint = 0;

		UpdateGraphics();
		//StartCoroutine(PlayerHurts());
	}

	public void HealDamage(float Heal)
	{
		hitPoint += Heal;
		if (hitPoint > maxHitPoint) 
			hitPoint = maxHitPoint;

		UpdateGraphics();
	}
	public void SetMaxHealth(float max)
	{
		maxHitPoint += (int)(maxHitPoint * max / 100);

		UpdateGraphics();
	}

	//==============================================================
	// Mana Logic
	//==============================================================
	private void UpdateManaBar()
	{
		float ratio = exp / maxExp;
		currentManaBar.rectTransform.localPosition = new Vector3(currentManaBar.rectTransform.rect.width * ratio - currentManaBar.rectTransform.rect.width, 0, 0);
		//manaText.text = manaPoint.ToString ("0") + "/" + maxManaPoint.ToString ("0");
	}

	private void UpdateManaGlobe()
	{
		float ratio = exp / maxExp;
		currentManaGlobe.rectTransform.localPosition = new Vector3(0, currentManaGlobe.rectTransform.rect.height * ratio - currentManaGlobe.rectTransform.rect.height, 0);
		//manaText.text = manaPoint.ToString("0") + "/" + maxManaPoint.ToString("0");
	}

	public void LooseExp(float Exp)
	{
		exp -= Exp;
		if (exp < 1) // Mana is Zero!!
			exp = 0;

		UpdateGraphics();
	}

	public void GainExp(float Exp)
	{
		exp += Exp;
		if (exp > maxExp)
        {
			exp = 0;
			SetMaxExp(PERCENT_EXP_INCREASE_PER_LEVEL);
			LevelUp();
		}

		UpdateGraphics();
	}

	private void LevelUp()
    {
		playerLevel += 1;
		manaText.text = "lvl " + playerLevel.ToString();
		PanelManager.Instance.SetUpText(playerUpgradesManager.Upgradeables);
    }
	public void SetMaxExp(float max)
	{
		maxExp += (int)(maxExp * max / 100);
		
		UpdateGraphics();
	}

	//==============================================================
	// Update all Bars & Globes UI graphics
	//==============================================================
	private void UpdateGraphics()
	{
		UpdateHealthBar();
		UpdateHealthGlobe();
		UpdateManaBar();
		//UpdateManaGlobe();
	}

	//==============================================================
	// Coroutine Player Hurts
	//==============================================================
	IEnumerator PlayerHurts()
	{
		// Player gets hurt. Do stuff.. play anim, sound..

		PopupText.Instance.Popup("Ouch!", 1f, 1f); // Demo stuff!

		if (hitPoint < 1) // Health is Zero!!
		{
			yield return StartCoroutine(PlayerDied()); // Hero is Dead
		}

		else
			yield return null;
	}

	//==============================================================
	// Hero is dead
	//==============================================================
	IEnumerator PlayerDied()
	{
		// Player is dead. Do stuff.. play anim, sound..
		PopupText.Instance.Popup("You have died!", 1f, 1f); // Demo stuff!

		yield return null;
	}

    public void Upgrade()
    {
        if (upgradeIsRegen)
        {
            regenUpgradeLevel += 1;
            regen += 0.5f;
        }
        else
        {
			SetMaxHealth(upgradeHealthPercentage);
        }
		print("upgrade health");
    }

    public string GetUpgradeText()
    {
		float random = Random.Range(0,100);
        if (random <50)
        {
			upgradeIsRegen = false;
			return UPDRAGE_MAX_HEALTH + upgradeHealthPercentage.ToString()+ "%";
		}
        else
        {
			upgradeIsRegen = true;
			return UPDRAGE_HEAlTH_REGEN;
        }
    }

    public string GetUpgradeTitle()
    {
		return UPGRADE_TITLE;
    }

	public bool isActivated()
	{
		return true;
	}

    public Image GetIcon()
    {
		if (upgradeIsRegen)
		{
			if (regenUpgradeLevel == 1)
			{
                return regen1.GetComponent<Image>();
            }
            else if (regenUpgradeLevel == 2)
            {
                return regen2.GetComponent<Image>();
            }
            else if (regenUpgradeLevel == 3)
            {
                return regen3.GetComponent<Image>();
            }
            return regen3.GetComponent<Image>();
        }
		return regen1.GetComponent<Image>();
    }
}
