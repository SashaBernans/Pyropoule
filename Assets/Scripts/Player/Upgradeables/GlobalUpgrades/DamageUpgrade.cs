using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Scripts.Player.Upgradeables.GlobalUpgrades
{
    public class DamageUpgrade : IUpgradeable
    {
        private List<Weapon> weapons;
        private const string UPGRADE_DAMAGE = "Increase damage of all weapons by 10 %";
        private const string UPGRADE_TITLE = "Berzerker level ";
        private int level = 1;

        public DamageUpgrade(List<Weapon> weapons)
        {
            this.weapons = weapons;
        }

        public string GetUpgradeText()
        {
            return UPGRADE_DAMAGE;
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
                weapon.UpgradeDamage(10);
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
