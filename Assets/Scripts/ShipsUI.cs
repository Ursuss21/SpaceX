using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipsUI : MonoBehaviour
{
    public static ShipsUI instance { get; set; }

    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private GameObject item = null;
    [SerializeField] private RectTransform content = null;
    private int numberOfItems;

    private List<ShipData> shipData;

    private int fieldHeight;

    private GameObject listItem;
    private ShipsRecordData listItemData;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        numberOfItems = 0;
        fieldHeight = 220;
    }

    public void SpawnListRecords(){
        SetNumberOfItems();
        SetContentBoxHeight();

        for(int i = 0; i < numberOfItems; ++i){
            SpawnRecord(i);
        }
    }
    
    private void SpawnRecord(int i){
        listItem = SpawnGameObject(i);
        listItemData = listItem.GetComponent<ShipsRecordData>();

        SetListItemName(i);
        SetListItemParent();

        UpdateListItemData(i);
        AddListItemOnClickEvent(i);
    }

    private void SetNumberOfItems(){
        numberOfItems = shipData.Count;
    }
    
    private void SetContentBoxHeight(){
        content.sizeDelta = new Vector2(0, numberOfItems * fieldHeight);
    }

    private GameObject SpawnGameObject(int i){
        float spawnY = i * fieldHeight;
        Vector3 pos = new Vector3(0, -spawnY, spawnPoint.position.z);
        return Instantiate(item, pos, spawnPoint.rotation);
    }

    private void SetListItemName(int i){
        listItem.name = "ListItem" + i;
    }

    private void SetListItemParent(){
        listItem.transform.SetParent(spawnPoint, false);
    }

    private void UpdateListItemData(int i){
        listItemData.UpdateItemData(i);
    }

    private void AddListItemOnClickEvent(int i){
        listItem.GetComponent<Button>().onClick.AddListener(() => OpenImageURL(shipData[i].ImageURL));
    }

    private void OpenImageURL(string url){
        Application.OpenURL(url);
    }

    public void ClearListRecords(){
        foreach(Transform child in spawnPoint){
            GameObject.Destroy(child.gameObject);
        }
    }

    public ShipData GetShipData(int i){
        return shipData[i];
    }

    public void SetShipsData(List<ShipData> x){
        shipData = x;
    }
}
