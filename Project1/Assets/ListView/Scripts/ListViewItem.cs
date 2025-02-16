﻿using UnityEngine;
using System.Collections.Generic;

namespace ListView
{
    public class ListViewItem : ListViewItem<ListViewItemData>
    {
    }

    [System.Serializable]
    public class ListViewItemData
    {
        public MonoBehaviour item;
    }

    
    public class ListViewItemBase : MonoBehaviour
    {
    }

    public class ListViewItem<DataType> : ListViewItemBase where DataType : ListViewItemData
    {
        public DataType data;

        public virtual void Setup(DataType data)
        {
            this.data = data;
            data.item = this;
        }
    }

   public class ListViewItemNestedData<ChildType> : ListViewItemData
    {
        public bool expanded;
        public ChildType[] children;
    }

    /*
    public class ListViewItemTemplate
    {
        public readonly GameObject prefab;
        public readonly List<MonoBehaviour> pool = new List<MonoBehaviour>();

        public ListViewItemTemplate(GameObject prefab)
        {
            if (prefab == null)
                Debug.LogError("Template prefab cannot be null");
            this.prefab = prefab;
        }
    }*/
}