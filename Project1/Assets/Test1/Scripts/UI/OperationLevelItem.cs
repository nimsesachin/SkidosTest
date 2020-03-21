using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ListView
{
    public class OperationLevelItem : ListViewItem<OperationLevelItemData>
    {
        public Text label = default;
        public Button button = default;
        public Image imageViewState = default;
        public Sprite spriteExpanded = default;
        public Sprite spriteCollapsed = default;

        public override void Setup(OperationLevelItemData data)
        {
            base.Setup(data);
            label.text = data.text;
        }

        public void SetViewState(bool expanded)
        {
            imageViewState.sprite = expanded ? spriteExpanded : spriteCollapsed;

            if (data.children != null && data.children.Length > 0)
            {
                foreach (OperationLevelItemData child in data.children)
                    child.item.gameObject.SetActive(expanded);
            }
        }

    }

    [System.Serializable]
    public class OperationLevelItemData : ListViewItemNestedData<OperationLevelItemData>
    {
        public string text;

        public OperationLevelItemData(string textData , OperationLevelItemData[] children = null)
        {
            this.text = textData;
            this.children = children;
        }

        public void SetChildItemData(OperationLevelItemData[] children)
        {
            this.children = children;
        }
        public void ToggleExpand()
        {
            expanded = !expanded;
            (item as OperationLevelItem).SetViewState(expanded);
        }
        
    }
}