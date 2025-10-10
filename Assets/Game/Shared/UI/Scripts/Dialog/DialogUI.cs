using UnityEngine;
using TMPro;
using Game.Static;
using Game.UI.Data;
using Game.Static.Enum;
using Game.Controllers;


namespace Game.UI
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private DialogButton dialogButtonPrefab;
        [SerializeField] private Transform dialogButtonParent;
        [SerializeField] private TextMeshProUGUI objectiveTextMesh;
        [SerializeField] private TextMeshProUGUI dialogTextMesh;
        [SerializeField] private CanvasGroup canvasGroup;

        public void SetCampaignStartText(CampaignStartInfo campaignStartInfo, DialogController dialogController)
        {
            objectiveTextMesh.text = campaignStartInfo.kingdom.name;
            string dialog = campaignStartInfo.kingdom.description + "\n";
            dialog += campaignStartInfo.dungeon.description + "\n";
            dialog += campaignStartInfo.introduction;
            dialogTextMesh.text = dialog;

            DialogOptionData data = new DialogOptionData() { text = "Proceed", game_state = GameState.Dialogue};
            DialogButton dialogButton = Instantiate(dialogButtonPrefab, dialogButtonParent);
            dialogButton.Initialize(() => DialogButtonOnClick(dialogController, data.text, data.game_state), data.text);
        }

        public void SetNewDialog(DialogData dialogData, DialogController dialogController)
        {
            StaticFunctions.ChangeCurrentUI(canvasGroup);

            objectiveTextMesh.text = dialogData.name;
            dialogTextMesh.text = dialogData.dialogue;

            ClearPreviousDialog();

            for (int i = 0; i < dialogData.options.Length; i++)
            {
                DialogOptionData dialogOption = dialogData.options[i];
                DialogButton dialogButton = Instantiate(dialogButtonPrefab, dialogButtonParent);
                dialogButton.Initialize(() => DialogButtonOnClick(dialogController, dialogOption.text, dialogOption.game_state), dialogOption.text);
            }
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