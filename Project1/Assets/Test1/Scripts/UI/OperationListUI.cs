using ListView;
using Newtonsoft.Json;
using SN.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SN.UIBase
{
    public class OperationListUI : MonoBehaviour
    {
        [SerializeField]
        private OperationDetailsUI detailsUI = null;

        [SerializeField]
        private Transform SpawnPoint = null;

        [SerializeField]
        private GameObject item = null;

        [SerializeField]
        private RectTransform content = null;

        [SerializeField]
        private int itemHeight = 100;

        [SerializeField]
        private string[] operations = default;

        private MockAPIData mockData = null;

        public void OnClickFetch()
        {
            MockAPIService.FetchData(new ServiceEventHandler(FetchEventHandler), null);
        }
        protected virtual void FetchEventHandler(string type, ServiceEvent inEvent, float inProgress, object inObject, object inUserData)
        {
            if (inEvent == ServiceEvent.COMPLETE)
            {
                mockData = inObject as MockAPIData;
            }
            else
                Debug.LogError("Mock API Data fetch failed");

            PopulateList();

        }


        private void Start()
        {           
        }

        // Use this for initialization
        void PopulateList()
        {
            if (mockData == null)
            {
                Debug.LogError("Mock Data not loaded");
                return;
            }
            if (operations.Length <= 0)
            {
                Debug.LogError("Operation list is empty");
                return;
            }

            content.sizeDelta = new Vector2(0, operations.Length * itemHeight);

            for (int i = 0; i < operations.Length; i++)
            {
                float spawnY = i * itemHeight;
                Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
                GameObject SpawnedItem = Instantiate(item, Vector3.zero, SpawnPoint.rotation);
                SpawnedItem.transform.SetParent(SpawnPoint, false);
                SpawnedItem.name = operations[i];
                OperationItem itemDetails = SpawnedItem.GetComponent<OperationItem>();
                itemDetails.Setup(new OperationItemData(operations[i]));
                itemDetails.button.onClick.AddListener(() => OnListItemClick(itemDetails.data.text));
            }
        }


        void OnListItemClick(string operationName)
        {
            detailsUI.SetItemListData(mockData.GetOperationGata(operationName));
            detailsUI.gameObject.SetActive(true);
        }

        public void OnClickTest2()
        {
            SceneManager.LoadScene("Scene2");
        }
    }
}