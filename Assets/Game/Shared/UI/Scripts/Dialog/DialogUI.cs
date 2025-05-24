using UnityEngine;
using Game.Controllers;
using Game.Static;
using Game.Static.Enum;
using Game.UI.Data;
using TMPro;


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
            if(StaticVariables.CurrentCanvasGroup != canvasGroup)
                StaticFunctions.ChangeCurrentUI(canvasGroup);

            objectiveTextMesh.text = dialogData.name;
            dialogTextMesh.text = dialogData.dialogue;

            ClearPreviousDialog();

            int i = 0;

            foreach (DialogOptionData dialogOption in dialogData.options)
            {
                i++;
            
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