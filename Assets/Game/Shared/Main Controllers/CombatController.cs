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
            }
            else
            {
                combatUI.UpdateEnemyButtons(null);

                EnemyController enemy = _charactersInCombat[_currentIndex] as EnemyController;

                if(enemy.Health.CurrentHealth > 0)
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
            int totalArmorPoints = StaticVariables.PlayerController.Equipment.GetTotalArmor();

            int hitRoll = UnityEngine.Random.Range(0, 21);
            int strengthModifier = Math.Max(0, (enemyController.Stats.strength - 10) / 2);

            bool isCrit = hitRoll == 20;
            hitRoll += isCrit ? 0 : strengthModifier;

            int dicesToRoll = enemyController.Equipment.Weapon.WeaponData.dicesToRoll;
            dicesToRoll *= isCrit ? 2 : 1;
            
            int totalDamage = 0;

            for (int i = 0; i < dicesToRoll; i++)
                totalDamage += UnityEngine.Random.Range(1,enemyController.Equipment.Weapon.WeaponData.rawDamage+1) + strengthModifier;

            StaticVariables.PlayerController.Health.ReceiveDamage(totalArmorPoints, hitRoll, totalDamage, isCrit);

            _currentIndex = _currentIndex == (_charactersInCombat.Count - 1) ? 0 : _currentIndex + 1;
            combatUI.UpdateInfoText(totalArmorPoints, hitRoll, totalDamage, isCrit, enemyController.Name);
            playerStatsUI.UpdateHealth(StaticVariables.PlayerController.Health);
            combatUI.UpdateContinueButton(ContinueCombat);
        }

        public void PerformAttack(int enemyIndex)
        {
            EnemyController selectedEnemy = _charactersInCombat[enemyIndex] as EnemyController;
            int totalArmorPoints = selectedEnemy.Equipment.GetTotalArmor();

            int hitRoll = UnityEngine.Random.Range(0, 21);
            int strengthModifier = Math.Max(0 ,(StaticVariables.PlayerController.Stats.strength - 10) / 2);

            bool isCrit = hitRoll == 20;
            hitRoll += isCrit ? 0 : strengthModifier;

            int dicesToRoll = StaticVariables.PlayerController.Equipment.Weapon.WeaponData.dicesToRoll;
            dicesToRoll *= isCrit ? 2 : 1;
            
            int totalDamage = 0;

            for (int i = 0; i < dicesToRoll; i++)
                totalDamage += UnityEngine.Random.Range(1,StaticVariables.PlayerController.Equipment.Weapon.WeaponData.rawDamage+1) + strengthModifier;

            selectedEnemy.Health.ReceiveDamage(totalArmorPoints, hitRoll, totalDamage, isCrit);

            _currentIndex = _currentIndex == (_charactersInCombat.Count - 1) ? 0 : _currentIndex + 1;
            combatUI.UpdateInfoText(totalArmorPoints, hitRoll, totalDamage, isCrit, StaticVariables.PlayerController.Name);
            combatUI.UpdateEnemies(_charactersInCombat.ToArray());
            combatUI.UpdateContinueButton(ContinueCombat);
        }

        private bool CheckForCombatEnd()
        {
            foreach (CharacterBase character in _charactersInCombat)
            {
                if(character.CharacterType == CharacterType.Enemy)
                {
                    EnemyController enemyController = character as EnemyController;
                    if(enemyController.Health.CurrentHealth > 0)
                        return false;
                }
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