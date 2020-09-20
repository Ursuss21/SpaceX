using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LaunchesUI : MonoBehaviour
{
    public static LaunchesUI instance { get; set; }

    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private GameObject item = null;
    [SerializeField] private RectTransform content = null;
    private int numberOfItems;
    
    private int renderedNumberOfItems;
    private int currentMinItem;
    private int currentMaxItem;

    private float fieldHeight;
    private float offsetX;

    private GameObject listItem;
    private LaunchesRecordData listItemData;

    private LinkedList<GameObject> listPool;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        numberOfItems = 0;
        renderedNumberOfItems = 9;
        currentMinItem = 0;
        currentMaxItem = 8;
        fieldHeight = 210.0f;
        offsetX = 0.0f;
        listPool = new LinkedList<GameObject>();
    }

    public void SpawnListRecords(){
        SetNumberOfItems();
        SetContentBoxHeight();

        for(int i = 0; i < renderedNumberOfItems; ++i){
            SpawnRecord(i);
            listPool.AddLast(listItem);
        }
        
        LoadSpaceXLaunchesData.instance.UpdateProgress(0.1f);
    }

    private void SetNumberOfItems(){
        numberOfItems = LoadSpaceXLaunchesData.instance.GetLaunchesDataSize();
    }

    private void SetContentBoxHeight(){
        content.sizeDelta = new Vector2(0, numberOfItems * fieldHeight);
    }

    private void SpawnRecord(int i){
        listItem = SpawnGameObject(i);
        listItemData = listItem.GetComponent<LaunchesRecordData>();

        SetListItemName(i);
        SetListItemParent();

        UpdateListItemData(i);
        AddListItemOnClickEvent();
    }

    private GameObject SpawnGameObject(int i){
        float spawnY = i * fieldHeight;
        Vector3 pos = new Vector3(offsetX, -spawnY, spawnPoint.position.z);
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

    private void AddListItemOnClickEvent(){
        listItem.GetComponent<Button>().onClick.AddListener(() => Popup.instance.OpenPopup(listItemData.GetIndex()));
    }

    public void OnDragDetectionPositionChange()
    {
        if(CheckIfTopItemOutOfViewport()){
            MoveTopItem();
        }
        if(CheckIfBottomItemOutOfViewport()){
            MoveBottomItem();
        }
    }

    private bool CheckIfTopItemOutOfViewport(){
        bool checkYAnchorPosition = -content.anchoredPosition.y - listPool.First.Value.GetComponent<RectTransform>().anchoredPosition.y < -fieldHeight;
        bool checkItemsRange = currentMaxItem < numberOfItems - 1;
        return (checkYAnchorPosition && checkItemsRange);
    }
    
    private bool CheckIfBottomItemOutOfViewport(){
        bool checkYAnchorPosition = -content.anchoredPosition.y - listPool.Last.Value.GetComponent<RectTransform>().anchoredPosition.y > ((renderedNumberOfItems - 1) * fieldHeight);
        bool checkItemsRange = currentMinItem > 0;
        return (checkYAnchorPosition && checkItemsRange);
    }

    private void MoveTopItem(){
        listItem = listPool.First.Value;
        listItemData = listItem.GetComponent<LaunchesRecordData>();

        SetNewItemPosition(-1);
        UpdateItemRange(1);
        UpdateListItemData(currentMaxItem);
        
        listPool.RemoveFirst();
        listPool.AddLast(listItem);
    }

    private void MoveBottomItem(){
        listItem = listPool.Last.Value;
        listItemData = listItem.GetComponent<LaunchesRecordData>();

        SetNewItemPosition(1);
        UpdateItemRange(-1);
        UpdateListItemData(currentMinItem);

        listPool.RemoveLast();
        listPool.AddFirst(listItem);
    }

    private void SetNewItemPosition(int modifier){
        Vector2 newPos = new Vector2(offsetX, listItem.GetComponent<RectTransform>().anchoredPosition.y + (modifier * renderedNumberOfItems * fieldHeight));
        listItem.GetComponent<RectTransform>().anchoredPosition = newPos;
    }

    private void UpdateItemRange(int modifier){
        currentMinItem += modifier;
        currentMaxItem += modifier;
    }
}
