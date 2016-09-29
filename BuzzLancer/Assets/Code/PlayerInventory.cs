using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public class PlayerInventory : MonoBehaviour
    {
        private readonly Player _player;
        private readonly Camera _camera;

        public IEnumerable<Item> _myItems;

        public bool InventoryOpen { get; set; }

        public PlayerInventory(IEnumerable<Item> myItems)
        {
            myItems = _myItems;
        }

        public void Load(Item itemPrefab)
        {
            var item = (Item)Instantiate(itemPrefab);
        }

        public void Update()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                ToggleInventory();
            }
        }

        public void ToggleInventory()
        {
            InventoryOpen = !InventoryOpen;
        }

        public void OnGUI()
        {
            if (InventoryOpen)
            {
                GUI.DrawTexture(new Rect(Screen.width - 200, Screen.height - 200, 10, 10), GameResources.OffScreenIndicator);    
            }
            
        }
        
    }
}