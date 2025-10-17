using System;

namespace Game.Character
{
    public class CharacterHealth
    {
        private int _maxHealth = 0;
        protected int currentHealth = 0;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => currentHealth;

        public void CalculateHealth(int hitDice, int level, int characterConstitution)
        {
            _maxHealth = hitDice;

            for (int i = 0; i < level; i++)
                _maxHealth += UnityEngine.Random.Range(1, hitDice + 1) + Math.Max(0, (characterConstitution - 10) / 2);
            
            currentHealth = _maxHealth;
        }

        public virtual void ReceiveDamage(int armorPoints, int hitRoll, int totalDamage, bool isCrit)
        {
            if(!isCrit && hitRoll < armorPoints)
                return;

            currentHealth -= totalDamage;
        }
    }
}