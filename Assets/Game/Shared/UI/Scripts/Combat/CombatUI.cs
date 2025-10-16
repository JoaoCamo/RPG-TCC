using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Game.Static;
using Game.Character;
using Game.Controllers;
using Game.Character.Enemy;

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
        private EnemyInfoButton _selectedEnemyButton = null;

        public void LoadUI(CharacterBase[] characters)
        {
            foreach (EnemyInfoButton enemyInfoButton in _enemyInfoButtons)
                Destroy(enemyInfoButton.gameObject);

            _enemyInfoButtons.Clear();
            
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i].CharacterType == Character.Enum.CharacterType.Player) 
                    continue;
                
                EnemyInfoButton enemyInfoButton = Instantiate(enemyButtonPrefab, enemyInfoParent);
                enemyInfoButton.Initialize(characters[i] as EnemyController, i);
                _enemyInfoButtons.Add(enemyInfoButton);
            }

            foreach (Transform child in enemyInfoParent)
                child.SetAsFirstSibling();
            
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
            foreach (EnemyInfoButton enemyInfoButton in _enemyInfoButtons)
                enemyInfoButton.UpdateButtonAction(onClick);
        }

        public void UpdateContinueButton(UnityAction onClick)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(onClick);
        }

        public void UpdateInfoText(int totalArmor, int hitRoll, int totalDamage, bool isCrit, string characterName)
        {
            if (hitRoll < totalArmor)
            {
                combatInfoTextMesh.text = characterName + " missed the attack!";
            }
            else
            {
                string critText = isCrit ? " made a critical roll" : " rolled " + hitRoll;
                combatInfoTextMesh.text = characterName + critText + " and dealt " + totalDamage + "!";
            }
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
                enemyInfoButton.UpdateButtonAction( () =>  
                {
                    continueButton.interactable = true;
                    UpdatedSelectedButton(enemyInfoButton);
                    UpdateInfoText("You selected " + enemyInfoButton.EnemyController.Name);
                    UpdateContinueButton(() =>
                    {
                        combatController.PerformAttack(enemyInfoButton.Index);
                        RemoveSelectedOutline();
                    });
                });
            }
        }
        
        public void UpdateEnemyTurnOutline(int index)
        {
            foreach (EnemyInfoButton enemyInfoButton in _enemyInfoButtons)
                enemyInfoButton.ToggleEnemyTurnOutline(enemyInfoButton.Index == index);
        }

        private void UpdatedSelectedButton(EnemyInfoButton enemyInfoButton)
        {
            _selectedEnemyButton?.ToggleSelectedOutline();
            
            _selectedEnemyButton = enemyInfoButton;
            _selectedEnemyButton.ToggleSelectedOutline();
        }

        private void RemoveSelectedOutline()
        {
            _selectedEnemyButton?.ToggleSelectedOutline();
            _selectedEnemyButton = null;
        }
    }
}