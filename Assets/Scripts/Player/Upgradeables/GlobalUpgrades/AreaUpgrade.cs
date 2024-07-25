using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Scripts.Player.Upgradeables.GlobalUpgrades
{
    public class AreaUpgrade : IUpgradeable
    {
        private List<Weapon> weapons;
        private const string UPGRADE_AREA = "Increase area of effect of all weapons by ";
        private const string UPGRADE_TITLE = "Fatso level ";
        private const int increasePercent = 8;
        private int level = 1;

        public AreaUpgrade(List<Weapon> weapons)
        {
            this.weapons = weapons;
        }

        public string GetUpgradeText()
        {
            return UPGRADE_AREA + increasePercent.ToString()+"%";
        }
        public string GetUpgradeTitle()
        {
            return UPGRADE_TITLE + level;
        }

        public void Upgrade()
        {
            level += 1;
            foreach (Weapon weapon in weapons)
            {
                weapon.UpgradeArea(increasePercent);
            }
        }

        public bool isActivated()
        {
            return true;
        }

        public Image GetIcon()
        {
            return null;
        }
    }
}
