using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Game.Static;
using Game.Character;

namespace Game.LevelUp
{
    public class LevelUpController : MonoBehaviour
    {
        [SerializeField] private LevelUpStatGroup[] statGroups;
        [SerializeField] private TextMeshProUGUI pointsRemainingTextMesh;
        [SerializeField] private Button confirmButton;

        private void Awake()
        {
            confirmButton.onClick.AddListener(ConfirmButtonAction);
            LoadStats();
        }

        private void LoadStats()
        {
            PlayerController playerController = StaticVariables.PlayerController;
            pointsRemainingTextMesh.text = playerController.Experience.SkillPoints.ToString();
            statGroups[0].Initialize(playerController.Stats.strength, () => StatButtonActionUp(statGroups[0], playerController), () => StatButtonActionDown(statGroups[0], playerController));
            statGroups[1].Initialize(playerController.Stats.dexterity, () => StatButtonActionUp(statGroups[1], playerController), () => StatButtonActionDown(statGroups[1], playerController));
            statGroups[2].Initialize(playerController.Stats.constitution, () => StatButtonActionUp(statGroups[2], playerController), () => StatButtonActionDown(statGroups[2], playerController));
            statGroups[3].Initialize(playerController.Stats.intelligence, () => StatButtonActionUp(statGroups[3], playerController), () => StatButtonActionDown(statGroups[3], playerController));
            statGroups[4].Initialize(playerController.Stats.wisdom, () => StatButtonActionUp(statGroups[4], playerController), () => StatButtonActionDown(statGroups[4], playerController));
            statGroups[5].Initialize(playerController.Stats.charisma, () => StatButtonActionUp(statGroups[5], playerController), () => StatButtonActionDown(statGroups[5], playerController));
        }

        private void StatButtonActionUp(LevelUpStatGroup levelUpStatGroup, PlayerController playerController)
        {
            ref int totalPoints = ref playerController.Experience.SkillPoints;

            if (totalPoints - (levelUpStatGroup.CurrentStatPoints + levelUpStatGroup.OriginalStatPoints >= 13 ? 2 : 1) < 0)
                return;

            levelUpStatGroup.CurrentStatPoints++;
            totalPoints -= levelUpStatGroup.CurrentStatPoints + levelUpStatGroup.OriginalStatPoints > 13 ? 2 : 1;
            levelUpStatGroup.UpdateText();
            UpdatePointsLeftText(totalPoints);
        }

        private void StatButtonActionDown(LevelUpStatGroup levelUpStatGroup, PlayerController playerController)
        {
            ref int totalPoints = ref playerController.Experience.SkillPoints;

            if ((levelUpStatGroup.CurrentStatPoints - 1) < 0)
                return;

            levelUpStatGroup.CurrentStatPoints--;
            totalPoints += levelUpStatGroup.CurrentStatPoints + levelUpStatGroup.OriginalStatPoints >= 13 ? 2 : 1;
            levelUpStatGroup.UpdateText();
            UpdatePointsLeftText(totalPoints);
        }

        private void ConfirmButtonAction()
        {
            foreach (LevelUpStatGroup levelUpStatGroup in statGroups)
                StaticVariables.PlayerController.Stats.AddStat(levelUpStatGroup.StatType, levelUpStatGroup.CurrentStatPoints);

            StaticEvents.OnLevelUp();
            StaticVariables.PlayerController.LevelUp();
            SceneManager.UnloadSceneAsync(4);
        }

        private void UpdatePointsLeftText(int pointsLeft)
        {
            pointsRemainingTextMesh.text = $"{pointsLeft} Points Left";
        }
    }
}