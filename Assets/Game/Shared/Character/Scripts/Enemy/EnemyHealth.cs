using Game.Static;

namespace Game.Character.Enemy
{
    public class EnemyHealth : CharacterHealth
    {
        private readonly EnemyController _controller;

        public EnemyHealth(EnemyController controller)
        {
            _controller = controller;
        }

        public override void ReceiveDamage(int armorPoints, int hitRoll, int totalDamage, bool isCrit)
        {
            base.ReceiveDamage(armorPoints, hitRoll, totalDamage, isCrit);
            
            if (currentHealth <= 0)
                StaticVariables.PlayerController.Experience.AddXp(StaticFunctions.GetXpValue(1));
        }
    }
}