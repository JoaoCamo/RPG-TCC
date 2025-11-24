using UnityEngine;
using TMPro;

namespace Game.Scenes.Character_Creation.Scripts.History_Context
{
    public class HistoryContextController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_InputField  contextInputField;
        
        public CanvasGroup CanvasGroup => canvasGroup;
        public string Context => contextInputField.text;
    }
}