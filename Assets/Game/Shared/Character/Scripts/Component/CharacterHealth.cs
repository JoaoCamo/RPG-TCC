using UnityEngine;

namespace Game.Character
{
    public class CharacterHealth : MonoBehaviour
    {
        protected int _maxHealth = 0;
        protected int _currentHealth = 0;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;

        public void CalculateHealth(int hitDice, int level, int characterConstitution)
        {
            int totalHealth = hitDice;

            for (int i = 1; i < level; i++)
                totalHealth += Random.Range(1, hitDice + 1) + ((characterConstitution - 10) / 2);
            
            _maxHealth = totalHealth;
        }

        public virtual void ReceiveDamage(int armorPoints, int hitRoll, int rawDamage, int dicesToRoll, int characterStrength, bool isCrit) {}
    }
}