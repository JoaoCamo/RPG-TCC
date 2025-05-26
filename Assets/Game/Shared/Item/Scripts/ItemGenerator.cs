using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Game.UI;
using Game.UI.Data;

namespace Game.Item
{
    public class ItemGenerator
    {
        private readonly MessageBoxUI messageUI;

        public ItemGenerator(MessageBoxUI messageBoxUI)
        {
            messageUI = messageBoxUI;
        }

        public IEnumerator GenerateItem(ArmorBase armorBase, string character)
        {
            string url = "http://127.0.0.1:5000/generate/item/";

            string dataJson = JsonUtility.ToJson(character);
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
            }
        }

        public IEnumerator GenerateItem(WeaponBase weaponBase, string character)
        {
            string url = "http://127.0.0.1:5000/generate/item/";
        
            string dataJson = JsonUtility.ToJson(character);
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
            }
        }
    }
}