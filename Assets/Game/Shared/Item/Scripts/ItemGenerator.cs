using Game.Backend.Data;
using Game.Item.Data;
using Game.Item.Enum;
using Game.UI;
using Game.UI.Data;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Game.Item
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private MessageBoxUI messageUI;

        public ItemGenerator(MessageBoxUI messageBoxUI)
        {
            messageUI = messageBoxUI;
        }

        public IEnumerator GenerateItem(ArmorBase armorBase, string character)
        {
            string url = "http://127.0.0.1:5000/generate/item/";

            ItemRequestData itemRequestData = new ItemRequestData(character, ItemType.Armor.ToString());
            string dataJson = JsonUtility.ToJson(itemRequestData);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(dataJson);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                messageUI.RequestMessageBox(request.error, new MessageBoxButtonData(() => SceneManager.LoadSceneAsync(0, LoadSceneMode.Single), "Return to main menu"), new MessageBoxButtonData());
            else
            {
                string response = request.downloadHandler.text;
                ItemCreationData itemData = JsonUtility.FromJson<ItemCreationData>(response);
                GenerateItem(armorBase, itemData);
            }
        }

        public IEnumerator GenerateItem(WeaponBase weaponBase, string character)
        {
            string url = "http://127.0.0.1:5000/generate/item/";

            ItemRequestData itemRequestData = new ItemRequestData(character, ItemType.Weapon.ToString());
            string dataJson = JsonUtility.ToJson(itemRequestData);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(dataJson);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
        
            yield return request.SendWebRequest();
        
            if (request.result != UnityWebRequest.Result.Success)
                messageUI.RequestMessageBox(request.error, new MessageBoxButtonData(() => SceneManager.LoadSceneAsync(0, LoadSceneMode.Single), "Return to main menu"), new MessageBoxButtonData());
            else
            {
                string response = request.downloadHandler.text;
                ItemCreationData itemData = JsonUtility.FromJson<ItemCreationData>(response);
                GenerateItem(weaponBase, itemData);
            }
        }

        public void GenerateItem(ArmorBase armorBase, ItemCreationData data)
        {
            ItemData itemData = new ItemData()
            {
                itemType = ItemType.Armor,
                itemName = data.itemName,
                description = data.itemDescription,
                weight = data.itemWeight,
                value = data.itemValue,
                rarity = data.itemRarity,
            };

            ArmorData armorData = new ArmorData()
            {
                armorClass = ArmorClass.Cloth,
                armorValue = data.itemData.armorValue,
                type = data.itemData.armorType,
            };

            armorBase.ItemData = itemData;
            armorBase.SetInfoItem(armorData);
        }

        public void GenerateItem(WeaponBase weaponBase, ItemCreationData data)
        {
            ItemData itemData = new ItemData()
            {
                itemType = ItemType.Weapon,
                itemName = data.itemName,
                description = data.itemDescription,
                weight = data.itemWeight,
                value = data.itemValue,
                rarity = data.itemRarity,
            };

            WeaponData weaponData = new WeaponData()
            {
                rawDamage = data.itemData.rawDamage,
                dicesToRoll = data.itemData.dicesToRoll,
                itemBonus = new ItemBonus[0]
            };

            weaponBase.ItemData = itemData;
            weaponBase.SetInfoItem(weaponData);
        }
    }
}