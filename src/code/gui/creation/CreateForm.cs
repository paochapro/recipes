interface CreateForm<T>
{
    void AddToBank();
    event Action<string> ErrorOccured; 
}