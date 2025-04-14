using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Game.Character.Enemy;

namespace Game.UI
{
    public class EnemyInfoButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI nameTextMesh;
        [SerializeField] private TextMeshProUGUI infoTextMesh;

        private EnemyController _enemyInfo;
        private int _index;

        public EnemyController EnemyInfo => _enemyInfo;
        public int Index => _index;

        public void Initialize(EnemyController enemyInfo, int index)
        {
            _enemyInfo = enemyInfo;
            _index = index;
            nameTextMesh.text = enemyInfo.Name;
            infoTextMesh.text = "HP: " + enemyInfo.Health.CurrentHealth + "/" + enemyInfo.Health.MaxHealth 
                                + "\nArmor Class: " + enemyInfo.Equipment.GetTotalArmor() 
                                + "\nDamage: " + enemyInfo.Equipment.Weapon.WeaponData.dicesToRoll + "d" + enemyInfo.Equipment.Weapon.WeaponData.rawDamage;
        }

        public void UpdateInfo(EnemyController enemyInfo)
        {
            infoTextMesh.text = "HP: " + enemyInfo.Health.CurrentHealth + "/" + enemyInfo.Health.MaxHealth 
                                + "\nArmor Class: " + enemyInfo.Equipment.GetTotalArmor() 
                                + "\nDamage: " + enemyInfo.Equipment.Weapon.WeaponData.dicesToRoll + "d" + enemyInfo.Equipment.Weapon.WeaponData.rawDamage;
        }

        public void UpdateButtonAction(UnityAction onClick)
        {
            button.onClick.RemoveAllListeners();

            if(onClick != null)
                button.onClick.AddListener(onClick);
        }
    }
}