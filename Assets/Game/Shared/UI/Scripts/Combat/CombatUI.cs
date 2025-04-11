using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Character.Enemy;

namespace Game.UI
{
    public class CombatUI : MonoBehaviour
    {
        [SerializeField] private EnemyInfoButton enemyButtonPrefab;
        [SerializeField] private Transform enemyInfoParent;
        [SerializeField] private TextMeshProUGUI combatInfoTextMesh;
        [SerializeField] private Button continueButton;

        private readonly List<EnemyInfoButton> enemyInfoButtons = new List<EnemyInfoButton>();

        public void LoadEnemies(EnemyController[] enemies)
        {
            foreach (EnemyInfoButton enemyInfoButton in enemyInfoButtons)
                Destroy(enemyInfoButton.gameObject);

            enemyInfoButtons.Clear();

            foreach (EnemyController enemy in enemies)
            {
                EnemyInfoButton enemyInfoButton = Instantiate(enemyButtonPrefab, enemyInfoParent);
                enemyInfoButton.Initialize(enemy);
                enemyInfoButtons.Add(enemyInfoButton);
            }
        }

        public void UpdateEnemies(EnemyController[] enemies)
        {
            for (int i = 0; i < enemies.Length; i++)
                enemyInfoButtons[i].UpdateInfo(enemies[i]);
        }
    }
}