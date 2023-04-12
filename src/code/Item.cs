interface Item {
    string Name { get; }
    string Category { get; }
}

readonly record struct FoodItem (
    string Name,
    string Category,
    string TextureUID
)
: Item;

readonly record struct InventoryItem(
    string Name, 
    string Category
)
: Item;

//ItemSet
interface ReadonlyItemSet {
    public IEnumerable<FoodItem> Food { get; }
    public IEnumerable<InventoryItem> Inventory { get; }
    public IEnumerable<string> FoodNames { get; }
    public IEnumerable<string> InventoryNames { get; }
}

class ItemSet : ReadonlyItemSet {
    public List<FoodItem> FoodList;
    public List<InventoryItem> InventoryList;

    public IEnumerable<FoodItem> Food => FoodList;
    public IEnumerable<InventoryItem> Inventory => InventoryList;
    public IEnumerable<string> FoodNames => FoodList.Select(x => x.Name);
    public IEnumerable<string> InventoryNames => InventoryList.Select(x => x.Name);

    public ItemSet(List<FoodItem> foodList, List<InventoryItem> inventoryList) 
    {
        this.FoodList = foodList;
        this.InventoryList = inventoryList;
    }

    public ItemSet() : this(new(), new())
    {}
}