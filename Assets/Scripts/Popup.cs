using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public static Popup instance { get; set; }

    [SerializeField] private GameObject popupWindow = null;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void OpenPopup(int index){
        popupWindow.SetActive(true);
        UpdatePopupData(index);
    }

    private void UpdatePopupData(int index){
        ShipsUI.instance.SetShipsData(LoadSpaceXLaunchesData.instance.GetLaunch(index).ShipData);
        ShipsUI.instance.SpawnListRecords();
    }

    public void ClosePopup(){
        popupWindow.SetActive(false);
        ShipsUI.instance.ClearListRecords();
    }
}
