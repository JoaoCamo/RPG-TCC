using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Character.Enemy;

namespace Game.UI
{
    public class EnemyInfoButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI nameTextMesh;
        [SerializeField] private TextMeshProUGUI infoTextMesh;

        public void Initialize(EnemyController enemyInfo)
        {
            nameTextMesh.text = enemyInfo.Name;
            infoTextMesh.text = "HP: " + enemyInfo.Health.MaxHealth + "/" + enemyInfo.Health.CurrentHealth 
                                + "\nArmor Class: " + enemyInfo.Equipment.GetTotalArmor() 
                                + "\nDamage: " + enemyInfo.Equipment.Weapon.WeaponData.dicesToRoll + "d" + enemyInfo.Equipment.Weapon.WeaponData.rawDamage;
        }

        public void UpdateInfo(EnemyController enemyInfo)
        {
            infoTextMesh.text = "HP: " + enemyInfo.Health.MaxHealth + "/" + enemyInfo.Health.CurrentHealth 
                                + "\nArmor Class: " + enemyInfo.Equipment.GetTotalArmor() 
                                + "\nDamage: " + enemyInfo.Equipment.Weapon.WeaponData.dicesToRoll + "d" + enemyInfo.Equipment.Weapon.WeaponData.rawDamage;
        }
    }
}