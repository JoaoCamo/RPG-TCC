using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Game.UI;
using Game.UI.Data;
using Game.Backend.Data;
using Game.Character.Enum;
using Game.Static;
using Game.Static.Enum;

namespace Game.Controllers
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private MapController mapController;
        [SerializeField] private MessageBoxUI messageUI;
        [SerializeField] private DialogUI dialogUI;
        [SerializeField] private PlayerStatsUI playerStatsUI;

        private void Awake()
        {
            StartCoroutine(StartCampaign(StaticVariables.PlayerController.Name, StaticVariables.PlayerController.Class));
        }

        private IEnumerator StartCampaign(string playerName, ClassType classType)
        {
            messageUI.RequestMessageBox("Waiting for Response");

            string url = "http://127.0.0.1:5000/generate/main_story/";

            StartCampaignData startCampaignData = new StartCampaignData() { name = playerName, type = classType.ToString() };
            string dataJson = JsonUtility.ToJson(startCampaignData);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(dataJson);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                messageUI.RequestMessageBox(request.error, new MessageBoxButtonData(messageUI.CloseMessageBox, "Close"), new MessageBoxButtonData());
            else
            {
                string response = request.downloadHandler.text;
                CampaignStartWrapper wrapper = JsonUtility.FromJson<CampaignStartWrapper>(response);
                CampaignStartInfo campaignStartInfo = JsonUtility.FromJson<CampaignStartInfo>(wrapper.main_story);
                dialogUI.SetCampaignStartText(campaignStartInfo, this);
                playerStatsUI.SetLocation(campaignStartInfo.kingdom.name);
                messageUI.CloseMessageBox();
            }
        }

        public IEnumerator SendDialogToAI(string dialog)
        {
            messageUI.RequestMessageBox("Waiting for Response");

            string url = "http://127.0.0.1:5000/generate/story/";

            DialogSendData data = new DialogSendData() { choice = dialog };
            string dataJson = JsonUtility.ToJson(data);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(dataJson);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                messageUI.RequestMessageBox(request.error, new MessageBoxButtonData(messageUI.CloseMessageBox, "Close"), new MessageBoxButtonData());
            else
            {
                string response = request.downloadHandler.text;
                DialogDataWrapper wrapper = JsonUtility.FromJson<DialogDataWrapper>(response);
                DialogData dialogData = JsonUtility.FromJson<DialogData>(wrapper.story);
                dialogUI.SetNewDialog(dialogData, this);
                messageUI.CloseMessageBox();
            }
        }

        public void ShowDungeonMessage()
        {
            string message = "Selecting this dialogue will lead you into the dungeon. Do you wish to proceed?";
            MessageBoxButtonData boxButtonDataExit = new MessageBoxButtonData(messageUI.CloseMessageBox, "Close");
            MessageBoxButtonData boxButtonDataContinue = new MessageBoxButtonData(() => 
            {   
                StaticVariables.CurrentGameState = GameState.Dungeon;
                messageUI.CloseMessageBox();
                mapController.LoadNewMap(); 
            }, "Enter Dungeon");

            messageUI.RequestMessageBox(message, boxButtonDataExit, boxButtonDataContinue);
        }
    }
}