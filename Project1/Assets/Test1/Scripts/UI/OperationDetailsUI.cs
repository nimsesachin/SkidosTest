using ListView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationDetailsUI : MonoBehaviour
{
    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private GameObject childItem = null;

    [SerializeField]
    private RectTransform content = null;

    private Dictionary<string, SubTopic[]> data = null;
    public void SetItemListData(Dictionary<string, SubTopic[]> data )
    {
        this.data = data;

        Populate();
    }

    public void Populate()
    {
        if (data == null)
        {    Debug.LogError("operation List data is null");
            return;
        }

        foreach( Transform t in SpawnPoint.transform)
        {
            Destroy(t.gameObject);
        }
       

        foreach (KeyValuePair<string, SubTopic[]> entry in data)
        {
            Vector3 pos = new Vector3(SpawnPoint.position.x, 0, SpawnPoint.position.z);
            GameObject SpawnedItem = Instantiate(item, Vector3.zero, SpawnPoint.rotation);
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            SpawnedItem.name = entry.Key;
            OperationLevelItem itemDetails = SpawnedItem.GetComponent<OperationLevelItem>();
           

            List<OperationLevelItemData> operationLevelItemsData = new List<OperationLevelItemData>();
            SubTopic[] subTopics = entry.Value;
            
            foreach(SubTopic subTopic in subTopics)
            {
                GameObject SpawnedChildItem = Instantiate(childItem, Vector3.zero, SpawnPoint.rotation);
                SpawnedChildItem.transform.SetParent(SpawnPoint, false);
                SpawnedChildItem.name = subTopic.SubtopicName;
                OperationLevelItem childItemDetails = SpawnedChildItem.GetComponent<OperationLevelItem>();

                OperationLevelItemData childItemData = new OperationLevelItemData(subTopic.SubtopicName);
                childItemDetails.Setup(childItemData);
                operationLevelItemsData.Add(childItemData);

                //itemDetails.button.onClick.AddListener(() => OnListItemClick(itemDetails));
            }
            //itemData.children = operationLevelItemsData.ToArray();

            OperationLevelItemData itemData = new OperationLevelItemData(entry.Key, operationLevelItemsData.ToArray());

            itemDetails.Setup(itemData);//, operationLevelItemsData.ToArray()));
            itemDetails.button.onClick.AddListener(() => OnListItemClick(itemDetails));
        }
    }

    void OnListItemClick(OperationLevelItem item)
    {
        item.data.ToggleExpand();
    }

    public void OnBack()
    {
        gameObject.SetActive(false);
    }
}
