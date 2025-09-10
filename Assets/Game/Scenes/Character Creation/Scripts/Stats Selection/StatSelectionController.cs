using System;
using UnityEngine;
using TMPro;
using Game.Character.Player;

namespace Game.Scenes.Character_Creation.Scripts.Stats_Selection
{
    public class StatSelectionController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private int pointsRemaining;
        [SerializeField] private TextMeshProUGUI pointsRemainingTextMesh;
        [SerializeField] private StatSelectionGroup[] groups;
        
        public CanvasGroup CanvasGroup => canvasGroup;
        public int PointsRemaining { get => pointsRemaining; set => pointsRemaining = value; }

        private void Awake()
        {
            LoadButtons();
        }
        
        private void LoadButtons()
        {
            foreach (StatSelectionGroup group in groups)
                group.Initialize(this);
        }
        
        public void SetCharacterStats(PlayerController player)
        {
            foreach (StatSelectionGroup group in groups)
                player.Stats.AddStat(group.StatsType, group.TotalPoints);
        }

        public void UpdatePointsRemainingText()
        {
            pointsRemainingTextMesh.text = $"{pointsRemaining} Points Left";
        }
    }
}