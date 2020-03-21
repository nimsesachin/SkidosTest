using ListView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ListView
{
    public class OperationItem : ListViewItem<OperationItemData>
    {
        public Text label;
        public Button button;

        public override void Setup(OperationItemData data)
        {
            base.Setup(data);
            label.text = data.text;
        }
    }

    [System.Serializable]
    public class OperationItemData : ListViewItemData
    {
        public string text;

        public OperationItemData(string textData)
        {
            text = textData;
        }
    }
}