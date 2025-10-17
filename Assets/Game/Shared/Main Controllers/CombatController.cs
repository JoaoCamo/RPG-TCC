using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using Game.Item;
using Game.Static;
using Game.Character;
using Game.Character.Enum;
using Game.Character.Enemy;

namespace Game.Controllers
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] private MapController mapController;
        [SerializeField] private CombatUI combatUI;
        [SerializeField] private PlayerStatsUI playerStatsUI;

        private List<CharacterBase> _charactersInCombat;
        private int _currentIndex = 0;

        private bool _isAtObjective = false;

        public void StartCombat(CharacterBase[] characterBases, bool isObjective)
        {
            _isAtObjective = isObjective;
            _charactersInCombat = OrderCharacterList(characterBases);
            combatUI.LoadUI(characterBases);
            combatUI.UpdateContinueButton(ContinueCombat);
            combatUI.UpdateInfoText("Click continue to start combat");
        }

        private void ContinueCombat()
        {
            if(CheckForCombatEnd())
            {
                foreach (CharacterBase character in _charactersInCombat)
                {
                    if (character.CharacterType != CharacterType.Player)
                    {
                        for (int i = 0; i < character.Inventory.Items.Count; i++)
                        {
                            ItemBase item = character.Inventory.Items[i];
                            mapController.GetCurrentSection().SectionItems.Add(item);
                            character.Inventory.RemoveItem(item);
                            i--;
                        }
                    }
                }

                mapController.LoadMap();

                if (_isAtObjective)
                    mapController.ShowDungeonExitOption();

                return;
            }

            _currentIndex = _currentIndex >= _charactersInCombat.Count ? 0 : _currentIndex;
            bool isPlayer = _charactersInCombat[_currentIndex].CharacterType == CharacterType.Player;
            
            if(isPlayer)
            {
                combatUI.UpdateInfoText("Select an enemy to attack");
                combatUI.GetClickedEnemy(this); 
                combatUI.UpdateEnemyTurnOutline(-1);
            }
            else
            {
                combatUI.UpdateEnemyButtons(null);
                combatUI.UpdateEnemyTurnOutline(_currentIndex);

                if(_charactersInCombat[_currentIndex] is EnemyController enemy && enemy.Health.CurrentHealth > 0)
                    PerformAttack(_charactersInCombat[_currentIndex] as EnemyController);
                else
                {
                    _currentIndex = _currentIndex == (_charactersInCombat.Count - 1) ? 0 : _currentIndex + 1;
                    ContinueCombat();
                }
            }
        }

        private void PerformAttack(EnemyController enemyController)
        {
            PlayerController player = StaticVariables.PlayerController;
            int totalArmorPoints = player.Equipment.GetTotalArmor();

            int hitRoll = UnityEngine.Random.Range(0, 21);
            int statModifier = Math.Max(0, (enemyController.Stats.GetStat(enemyController.Equipment.Weapon.WeaponData.modifierStat) - 10) / 2);

            bool isCrit = hitRoll == 20;
            hitRoll += isCrit ? 0 : statModifier;

            int dicesToRoll = enemyController.Equipment.Weapon.WeaponData.dicesToRoll;
            dicesToRoll *= isCrit ? 2 : 1;
            
            int totalDamage = 0;

            for (int i = 0; i < dicesToRoll; i++)
                totalDamage += UnityEngine.Random.Range(1,enemyController.Equipment.Weapon.WeaponData.rawDamage+1) + statModifier;

            player.Health.ReceiveDamage(totalArmorPoints, hitRoll, totalDamage, isCrit);

            _currentIndex = _currentIndex == (_charactersInCombat.Count - 1) ? 0 : _currentIndex + 1;
            combatUI.UpdateInfoText(totalArmorPoints, hitRoll, totalDamage, isCrit, enemyController.Name);
            playerStatsUI.UpdateHealth(player.Health);
            combatUI.UpdateContinueButton(ContinueCombat);
        }

        public void PerformAttack(int enemyIndex)
        {
            PlayerController player = StaticVariables.PlayerController;
            
            if (_charactersInCombat[enemyIndex] is EnemyController selectedEnemy)
            {
                int totalArmorPoints = selectedEnemy.Equipment.GetTotalArmor();

                int hitRoll = UnityEngine.Random.Range(0, 21);
                int statModifier = Math.Max(0 ,(player.Stats.GetStat(player.Equipment.Weapon.WeaponData.modifierStat) - 10) / 2);

                bool isCrit = hitRoll == 20;
                hitRoll += isCrit ? 0 : statModifier;

                int dicesToRoll = player.Equipment.Weapon.WeaponData.dicesToRoll;
                dicesToRoll *= isCrit ? 2 : 1;
            
                int totalDamage = 0;

                for (int i = 0; i < dicesToRoll; i++)
                    totalDamage += UnityEngine.Random.Range(1, player.Equipment.Weapon.WeaponData.rawDamage+1) + statModifier;

                selectedEnemy.Health.ReceiveDamage(totalArmorPoints, hitRoll, totalDamage, isCrit);

                _currentIndex = _currentIndex == (_charactersInCombat.Count - 1) ? 0 : _currentIndex + 1;
                combatUI.UpdateInfoText(totalArmorPoints, hitRoll, totalDamage, isCrit, player.Name);
            }

            combatUI.UpdateEnemies(_charactersInCombat.ToArray());
            combatUI.UpdateContinueButton(ContinueCombat);
        }

        private bool CheckForCombatEnd()
        {
            foreach (CharacterBase character in _charactersInCombat.Where(character => character.CharacterType == CharacterType.Enemy))
            {
                if(character is EnemyController enemyController && enemyController.Health.CurrentHealth > 0)
                    return false;
            }

            return true;
        }

        private List<CharacterBase> OrderCharacterList(CharacterBase[] characters)
        {
            var map = characters.ToDictionary(c => c,c => UnityEngine.Random.Range(0, 21) + Math.Max(0, (c.Stats.dexterity - 10) / 2));
            Array.Sort(characters, (a, b) => map[a].CompareTo(map[b]));
            return characters.ToList();
        }
    }
}