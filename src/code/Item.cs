using Newtonsoft.Json;

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
[JsonObject(MemberSerialization.Fields)]
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

[JsonObject(MemberSerialization.Fields)]
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

    public ItemSet(ItemSet other)
    {
        this.FoodList = new();
        this.InventoryList = new(other.Inventory); 

        foreach(FoodWithCount food in other.Food)
            this.FoodList.Add(new FoodWithCount(food.Item, food.Count));
    }

    public override string ToString() { 
        string food = string.Join(",", Food.Select(f => f.Name + f.Count));
        string inv = string.Join(",", InventoryNames);
        return $"ItemSet(Food: {food}, Inv:{inv})";
    }

    public ItemSet() 
        : this(new List<FoodWithCount>(), new List<InventoryItem>())
    {}
}

interface ReadonlyItemBank {
    public IEnumerable<FoodItem> Food { get; }
    public IEnumerable<InventoryItem> Inventory { get; }
}

[JsonObject(MemberSerialization.Fields)]
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

    public override string ToString() {
        var foodList = string.Join(", ", FoodList.Select(x => x.Name));
        var invList = string.Join(", ", InventoryList.Select(x => x.Name));

        return
@$"ItemBank
    FoodList: { foodList }
    InvList: {invList}";
    }
}

class ItemNameEqualityComparer<TItem> : IEqualityComparer<TItem>
    where TItem : Item
{
    public bool Equals(TItem? x, TItem? y)
    {
        if(x == null || y == null)
        {
            if(x != null || y != null)
                return false;

            return true;
        }

        return x.Name.ToLower() == y.Name.ToLower();
    }

    public int GetHashCode(TItem obj) => obj.Name.GetHashCode();
}