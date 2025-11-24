using UnityEngine.SceneManagement;
using Game.Static;
using Game.UI.Data;

namespace Game.Character
{
    public class PlayerHealth : CharacterHealth
    {
        public override void ReceiveDamage(int armorPoints, int hitRoll, int totalDamage, bool isCrit)
        {
            base.ReceiveDamage(armorPoints, hitRoll, totalDamage, isCrit);

            if (currentHealth > 0)
                return;

            StaticEvents.RequestMessageBoxUIWithOptions?.Invoke("You have been defeated",
                new MessageBoxButtonData(() => SceneManager.LoadSceneAsync(0, LoadSceneMode.Single), "Return to main menu"),
                new MessageBoxButtonData(null, string.Empty));
        }

        public void AddHealth(int amount)
        {
            currentHealth = currentHealth + amount > MaxHealth ? MaxHealth : currentHealth + amount;
        }
    }
}