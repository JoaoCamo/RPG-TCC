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
        [SerializeField] private Image outline;

        private EnemyController _enemyController;
        private int _index;

        private readonly Color _selectedColor = Color.cyan;
        private readonly Color _unselectedColor = new Color32(42,42,42,255);
        private readonly Color _turnColor = Color.red;

        public EnemyController EnemyController => _enemyController;
        public int Index => _index;

        public void Initialize(EnemyController enemyController, int index)
        {
            _enemyController = enemyController;
            _index = index;
            nameTextMesh.text = enemyController.Name;
            UpdateInfo(enemyController);
        }

        public void UpdateInfo(EnemyController enemyInfo)
        {
            int currentHealth = enemyInfo.Health.CurrentHealth > 0 ? enemyInfo.Health.CurrentHealth : 0;

            infoTextMesh.text = "HP: " + currentHealth + "/" + enemyInfo.Health.MaxHealth 
                                + "\nArmor Class: " + enemyInfo.Equipment.GetTotalArmor() 
                                + "\nDamage: " + enemyInfo.Equipment.Weapon.WeaponData.dicesToRoll + "d" + enemyInfo.Equipment.Weapon.WeaponData.rawDamage;
        }

        public void UpdateButtonAction(UnityAction onClick)
        {
            button.onClick.RemoveAllListeners();

            if(onClick != null)
                button.onClick.AddListener(onClick);
        }

        public void ToggleSelectedOutline()
        {
            outline.color = outline.color == _selectedColor ? _unselectedColor : _selectedColor;
        }

        public void ToggleEnemyTurnOutline(bool mode)
        {
            outline.color = mode ? _turnColor :  _unselectedColor;
        }

        public void ToggleButton(bool mode)
        {
            button.interactable = mode;
        }
    }
}