partial class FiltersForm : Form 
{
	public SearchInfo GetSearchInfo()
	{
		int time = (int)GetNode<FormSpinBoxComponent>("Time").GetValue;
		int dishTypeIndex = GetNode<FormOptionsComponent>("DishType").GetValue;
        bool useLocalItems = GetNode<FormCheckBoxComponent>("UseLocalItems").GetValue;
		DishType dishType = (DishType)dishTypeIndex;

		var program = GetNode<Program>("/root/Program");
		ReadonlyItemSet localItems = useLocalItems ? program.LocalItems : new ItemSet();
		ReadonlyItemSet filterItems = new ItemSet();

		SearchInfo searchInfo = new SearchInfo("", localItems, filterItems, dishType, time);
		return searchInfo;
	}
}