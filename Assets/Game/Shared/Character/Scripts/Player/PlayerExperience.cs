using Game.Character;
using Game.Static;
using Game.UI.Data;
using UnityEngine.SceneManagement;

namespace Game.Shared.Character.Scripts.Player
{
    public class PlayerExperience : CharacterExperience
    {
        private int _skillPoints = 0;
        public ref int SkillPoints => ref _skillPoints;
        
        public PlayerExperience(int currentLevel) : base(currentLevel) { }

        public override void AddXp(int value)
        {
            currentXp += value;

            if (currentXp < xpTable[currentLevel - 1])
                return;
            
            currentLevel++;
            _skillPoints++;

            StaticEvents.RequestMessageBoxUIWithOptions?.Invoke("Level Up!",
                new MessageBoxButtonData(() => SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive), "Proceed"),
                new MessageBoxButtonData(null, string.Empty));
        }
    }
}