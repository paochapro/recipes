class CustomErrorException : Exception 
{
    public CustomErrorException(string message)
        : base(message)
    {
    }
}