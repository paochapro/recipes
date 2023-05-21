partial class FiltersForm : Form 
{
	public SearchInfo GetSearchInfo()
	{
		int time = (int)GetNode<FormSpinBoxComponent>("Time").GetValue;
		int dishTypeIndex = GetNode<FormOptionsComponent>("DishType").GetValue;
		DishType dishType = (DishType)dishTypeIndex;

		ItemSet localItems = new();
		ItemSet filterItems = new();

		SearchInfo searchInfo = new SearchInfo("", localItems, filterItems, dishType, time);
		return searchInfo;
	}
}