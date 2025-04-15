using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Game.Character.Enemy;
using Game.Static;
using Game.Controllers;
using Game.Character;

namespace Game.UI
{
    public class CombatUI : MonoBehaviour
    {
        [SerializeField] private EnemyInfoButton enemyButtonPrefab;
        [SerializeField] private Transform enemyInfoParent;
        [SerializeField] private TextMeshProUGUI combatInfoTextMesh;
        [SerializeField] private Button continueButton;
        [SerializeField] private CanvasGroup canvasGroup;

        private readonly List<EnemyInfoButton> _enemyInfoButtons = new List<EnemyInfoButton>();

        public void LoadUI(CharacterBase[] characters)
        {
            foreach (EnemyInfoButton enemyInfoButton in _enemyInfoButtons)
                Destroy(enemyInfoButton.gameObject);

            _enemyInfoButtons.Clear();

            for (int i = 0; i < characters.Length; i++)
            {
                if(characters[i].CharacterType != Character.Enum.CharacterType.Player)
                {
                    EnemyInfoButton enemyInfoButton = Instantiate(enemyButtonPrefab, enemyInfoParent);
                    enemyInfoButton.Initialize(characters[i] as EnemyController, i);
                    _enemyInfoButtons.Add(enemyInfoButton);
                }
            }

            StaticFunctions.ChangeCurrentUI(canvasGroup);
        }

        public void UpdateEnemies(CharacterBase[] characters)
        {
            int i = 0;

            foreach (CharacterBase character in characters)
            {
                if(character.CharacterType != Character.Enum.CharacterType.Player)
                {
                    _enemyInfoButtons[i].UpdateInfo(character as EnemyController);
                    i++;
                }
            }
        }

        public void UpdateEnemyButtons(UnityAction onClick)
        {
            for (int i = 0; i < _enemyInfoButtons.Count; i++)
                _enemyInfoButtons[i].UpdateButtonAction(onClick);
        }

        public void UpdateContinueButton(UnityAction onClick)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(onClick);
        }

        public void UpdateInfoText(int hitroll, int totalDamage, bool isCrit, string characterName)
        {
            string critText = isCrit ? " made a critical roll" : " rolled " + hitroll;
            combatInfoTextMesh.text = characterName + critText + " and dealt " + totalDamage + "!";
        }

        public void UpdateInfoText(string text)
        {
            combatInfoTextMesh.text = text;
        }

        public void GetClickedEnemy(CombatController combatController)
        {
            continueButton.interactable = false;

            foreach (EnemyInfoButton enemyInfoButton in _enemyInfoButtons)
            {
                enemyInfoButton.UpdateButtonAction( () =>  { continueButton.interactable = true;
                                                             UpdateInfoText("You selected " + enemyInfoButton.EnemyInfo.Name);
                                                             UpdateContinueButton( () => combatController.PerformAttack(enemyInfoButton.Index) ); 
                                                             } );
            }
        }
    }
}