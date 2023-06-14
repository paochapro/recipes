abstract partial class InvInspector : ItemsInspector<InventoryItem>
{
    protected sealed override void AddBankEvents(GlobalEvents events) {
        events.RemoveBankInv += BankItemRemoved;
        events.InvModified += BankItemModified;
    }

    protected sealed override void RemoveBankEvents(GlobalEvents events) {
        events.RemoveBankInv -= BankItemRemoved;
        events.InvModified -= BankItemModified;
    }
}