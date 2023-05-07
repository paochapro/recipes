interface CreateForm<T>
{
    T CreateObject();
    event Action<string> ErrorOccured; 
}