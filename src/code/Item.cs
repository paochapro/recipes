interface Item {
    string Name { get; }
    string Category { get; }
}

readonly record struct FoodItem (
    string Name,
    string Category,
    string TexturePath
) : Item;

readonly record struct InventoryItem(
    string Name, 
    string Category
) : Item;

//Food with count
class FoodWithCount : Item
{
    public string Name => Item.Name;
    public string Category => Item.Category;
    public FoodItem Item { get; private set; }
    public int Count { get; set; }

    public FoodWithCount(FoodItem item, int count)
    {
        Item = item;
        Count = count;
    }
}

//ItemSet
interface ReadonlyItemSet {
    public IEnumerable<FoodWithCount> Food { get; }
    public IEnumerable<InventoryItem> Inventory { get; }
    
    public IEnumerable<FoodItem> FoodItems { get; }
    public IEnumerable<string> FoodNames { get; }
    public IEnumerable<string> InventoryNames { get; }
}

class ItemSet : ReadonlyItemSet {
    public List<FoodWithCount> FoodList;
    public List<InventoryItem> InventoryList;

    public IEnumerable<FoodWithCount> Food => FoodList;
    public IEnumerable<InventoryItem> Inventory => InventoryList;

    public IEnumerable<FoodItem> FoodItems => FoodList.Select(x => x.Item);
    public IEnumerable<string> FoodNames => FoodList.Select(x => x.Item.Name);
    public IEnumerable<string> InventoryNames => InventoryList.Select(x => x.Name);

    public ItemSet(List<FoodWithCount> foodList, List<InventoryItem> inventoryList) 
    {
        this.FoodList = foodList;
        this.InventoryList = inventoryList;
    }

    public ItemSet() 
        : this(new List<FoodWithCount>(), new List<InventoryItem>())
    {}
}

interface ReadonlyItemBank {
    public IEnumerable<FoodItem> Food { get; }
    public IEnumerable<InventoryItem> Inventory { get; }
}

class ItemBank : ReadonlyItemBank  {
    public List<FoodItem> FoodList;
    public List<InventoryItem> InventoryList;

    public IEnumerable<FoodItem> Food => FoodList;
    public IEnumerable<InventoryItem> Inventory => InventoryList;

    public ItemBank(List<FoodItem> food, List<InventoryItem> inv)
    {
        FoodList = food;
        InventoryList = inv;
    }

    public ItemBank()
        : this(new(), new()) 
    {
    }
}