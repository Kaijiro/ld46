using System.Collections.Generic;

namespace InventoryItems
{

    [System.Serializable]
    public class InventoryItem
    {

        public int quantity = 0;
        public string name = "free";

        public InventoryItem(int newQuantity, string newName)
        {
            quantity = newQuantity;
            name = newName;
        }

    }
}
