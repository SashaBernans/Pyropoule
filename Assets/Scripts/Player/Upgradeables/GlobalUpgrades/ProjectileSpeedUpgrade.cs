using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Scripts.Player.Upgradeables
{
    public class ProjectileSpeedUpgrade : IUpgradeable
    {
        private List<Weapon> weapons;
        private const string UPGRADE_PROJECTILE_SPEED = "Increase projectile speed by 10 %";
        private const string UPGRADE_TITLE = "Projectile go brrrr level ";
        private int level = 1;

        public ProjectileSpeedUpgrade(List<Weapon> weapons)
        {
            this.weapons = weapons;
        }

        public string GetUpgradeText()
        {
            return UPGRADE_PROJECTILE_SPEED;
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
                weapon.UpgradeProjectileSpeed(10);
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
