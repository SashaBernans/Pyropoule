using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player.Upgradeables.GlobalUpgrades
{
    public class AttackSpeedUpgrade : IUpgradeable
    {
        private List<Weapon> weapons;
        private const string UPGRADE_ATTACK_SPEED = "Increase attack speed of all weapons by 8 %";
        private const string UPGRADE_TITLE = "Ninja level ";
        private int level = 1;
        private Image icon;

        public AttackSpeedUpgrade(List<Weapon> weapons)
        {
            this.weapons = weapons;
        }

        public string GetUpgradeText()
        {
            return UPGRADE_ATTACK_SPEED;
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
                weapon.UpgradeAttackSpeed(8);
            }
        }
        public bool isActivated()
        {
            return true;
        }

        public Image GetIcon()
        {
            return GameManager.Instance.AttackSpeedUpgradeIcon.GetComponent<Image>();
        }
    }
}
