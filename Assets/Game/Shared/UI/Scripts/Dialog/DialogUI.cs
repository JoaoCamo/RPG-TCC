using UnityEngine;
using TMPro;
using Game.Static;
using Game.UI.Data;
using Game.Static.Enum;
using Game.Controllers;
using Game.Shared.UI.Data;
using UnityEngine.Events;

namespace Game.UI
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private DialogButton dialogButtonPrefab;
        [SerializeField] private Transform dialogButtonParent;
        [SerializeField] private TextMeshProUGUI titleTextMesh;
        [SerializeField] private TextMeshProUGUI dialogTextMesh;
        [SerializeField] private CanvasGroup canvasGroup;

        public void SetCampaignStartText(CampaignStartInfo campaignStartInfo, DialogController dialogController)
        {
            titleTextMesh.text = campaignStartInfo.title;
            dialogTextMesh.text = $"{campaignStartInfo.introduction}\n{campaignStartInfo.dungeon.description}";

            DialogOptionData data = new DialogOptionData() { text = "Begin Adventure", game_state = GameState.Dialogue};
            DialogButton dialogButton = Instantiate(dialogButtonPrefab, dialogButtonParent);
            dialogButton.Initialize(() => DialogButtonOnClick(dialogController, data.text, data.game_state), data.text);
        }

        public void SetNewDialog(DialogData dialogData, DialogController dialogController)
        {
            ClearPreviousDialog();
            
            titleTextMesh.text = dialogData.name;
            dialogTextMesh.text = dialogData.dialogue;

            for (int i = 0; i < dialogData.options.Length; i++)
            {
                DialogOptionData dialogOption = dialogData.options[i];
                DialogButton dialogButton = Instantiate(dialogButtonPrefab, dialogButtonParent);
                dialogButton.Initialize(() => DialogButtonOnClick(dialogController, dialogOption.text, dialogOption.game_state), dialogOption.text);
            }
            
            StaticFunctions.ChangeCurrentUI(canvasGroup);
        }

        public void SetNewDialog(ArcData arcData, DialogController dialogController, bool isEnding)
        {
            ClearPreviousDialog();

            titleTextMesh.text = arcData.title;
            dialogTextMesh.text = arcData.arcIntroduction;

            DialogOptionData data = isEnding ? new DialogOptionData("Return to Main Menu", GameState.Dialogue) : new DialogOptionData("Continue Adventure", GameState.Dialogue);
            UnityAction onClick = isEnding ? dialogController.ShowEndingMessage : () => DialogButtonOnClick(dialogController, data.text, data.game_state);
            
            DialogButton dialogButton = Instantiate(dialogButtonPrefab, dialogButtonParent);
            dialogButton.Initialize(isEnding ? dialogController.ShowEndingMessage : onClick, data.text);
            
            StaticFunctions.ChangeCurrentUI(canvasGroup);
        }

        private void DialogButtonOnClick(DialogController dialogController, string text, GameState newGameState)
        {
            if (newGameState == GameState.Dungeon)
                dialogController.ShowDungeonMessage();
            else
                StartCoroutine(dialogController.SendDialogToAI(text));
        }

        private void ClearPreviousDialog()
        {
            for (int i = dialogButtonParent.childCount - 1; i >= 0; i--)
                Destroy(dialogButtonParent.GetChild(i).gameObject);
        }
    }
}