namespace BO;

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string? message, Exception innerException) :
    base(message, innerException)
    { }
}

[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}





/*



[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}
[Serializable]

public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}

[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
}

[Serializable]
public class InvalidTime : Exception
{
    public InvalidTime(string? message) : base(message) { }
}

*/