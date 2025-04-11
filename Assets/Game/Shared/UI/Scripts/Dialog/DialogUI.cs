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

        public void SetNewDialog(DialogData dialogData)
        {
            objectiveTextMesh.text = dialogData.currentObjective;
            dialogTextMesh.text = dialogData.text;

            ClearPreviousDialog();

            foreach (DialogOptionData dialogOption in dialogData.dialogOptions)
            {
                DialogButton dialogButton = Instantiate(dialogButtonPrefab, dialogButtonParent);
                dialogButton.Initialize(() => DialogButtonOnClick(dialogOption.newGameState), dialogOption.text);
            }
        }

        private void DialogButtonOnClick(GameState newGameState)
        {
            StaticVariables.CurrentGameState = newGameState;
        }

        private void ClearPreviousDialog()
        {
            for (int i = dialogButtonParent.childCount - 1; i >= 0; i--)
                Destroy(dialogButtonParent.GetChild(i).gameObject);
        }
    }
}