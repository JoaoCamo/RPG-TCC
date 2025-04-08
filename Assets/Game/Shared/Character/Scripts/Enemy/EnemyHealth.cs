using UnityEngine;

namespace Game.Character.Enemy
{
    public class EnemyHealth : CharacterHealth
    {   
        public override void ReceiveDamage(int armorPoints, int hitRoll, int rawDamage, int dicesToRoll, int characterStrength, bool isCrit)
        {
            if(!isCrit && hitRoll < armorPoints)
                return;

            dicesToRoll *= isCrit ? 2 : 1;

            int totalDamage = 0;

            for (int i = 0; i < dicesToRoll; i++)
                totalDamage += Random.Range(1, rawDamage + 1);

            totalDamage += characterStrength > 10 ? (characterStrength - 10) / 2 : 0;

            _currentHealth -= totalDamage;
        }
    }
}