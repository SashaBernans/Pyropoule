using UnityEngine.UI;

public interface IUpgradeable
{
    public void Upgrade();
    public string GetUpgradeText();
    public string GetUpgradeTitle();
    public bool isActivated();
    public Image GetIcon();
}