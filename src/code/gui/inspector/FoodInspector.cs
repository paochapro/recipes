abstract partial class FoodInspector : ItemsInspector<FoodItem>
{
    protected sealed override void AddBankEvents(GlobalEvents events) {
        events.RemoveBankFood += BankItemRemoved;
        events.FoodModified += BankItemModified;
    }

    protected sealed override void RemoveBankEvents(GlobalEvents events) {
        events.RemoveBankFood -= BankItemRemoved;
        events.FoodModified -= BankItemModified;
    }
}