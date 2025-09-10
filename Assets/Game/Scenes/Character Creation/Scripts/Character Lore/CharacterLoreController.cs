using Game.Character.Enum;
using UnityEngine;
using TMPro;
using Game.Character.Player;

namespace Game.Scenes.Character_Creation.Scripts.Character_Lore
{
    public class CharacterLoreController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_InputField characterNameInputField;
        [SerializeField] private TMP_Dropdown classDropdown;
        [SerializeField] private TMP_InputField characterHistoryInputField;

        public string CharacterName => characterNameInputField.text;
        public ClassType SelectedClass => (ClassType)classDropdown.value;
        public string CharacterHistory => characterHistoryInputField.text;
        
        public CanvasGroup CanvasGroup => canvasGroup;
        
        public void SetCharacterLore(PlayerController player)
        {
            
        }

        private ClassType GetClass(int characterClass)
        {
            return (ClassType)characterClass;
        }
    }
}