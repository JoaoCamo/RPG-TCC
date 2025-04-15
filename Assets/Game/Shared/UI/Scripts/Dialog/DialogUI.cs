using Game.Controllers;
using Game.Static;
using Game.Static.Enum;
using Game.UI.Data;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private DialogButton dialogButtonPrefab;
        [SerializeField] private Transform dialogButtonParent;
        [SerializeField] private TextMeshProUGUI objectiveTextMesh;
        [SerializeField] private TextMeshProUGUI dialogTextMesh;
        [SerializeField] private CanvasGroup canvasGroup;

        public void SetNewDialog(DialogData dialogData, DialogController dialogController)
        {
            objectiveTextMesh.text = dialogData.currentObjective;
            dialogTextMesh.text = dialogData.text;

            ClearPreviousDialog();

            foreach (DialogOptionData dialogOption in dialogData.dialogOptions)
            {
                DialogButton dialogButton = Instantiate(dialogButtonPrefab, dialogButtonParent);
                dialogButton.Initialize(() => DialogButtonOnClick(dialogController, dialogOption.text, dialogOption.newGameState), dialogOption.text);
            }
        }

        private void DialogButtonOnClick(DialogController dialogController, string text, GameState newGameState)
        {
            if(newGameState != StaticVariables.CurrentGameState)
                StaticFunctions.LoadGameState(newGameState);
            else
            {
                dialogController.SendDialogToAI(text);
            }
        }

        private void ClearPreviousDialog()
        {
            for (int i = dialogButtonParent.childCount - 1; i >= 0; i--)
                Destroy(dialogButtonParent.GetChild(i).gameObject);
        }
    }
}