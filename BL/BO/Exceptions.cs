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


[Serializable]
public class BlInvalidDateException : Exception
{
    public BlInvalidDateException(string? message) : base(message) { }
    public BlInvalidDateException(string? message, Exception innerException) :
    base(message, innerException)
    { }
}


[Serializable]
public class BlInvalidTaskCreation : Exception
{
    public BlInvalidTaskCreation(string? message) : base(message) { }
    public BlInvalidTaskCreation(string? message, Exception innerException) :
    base(message, innerException)
    { }
}


[Serializable]
public class BlEngineerCantTakeTask : Exception
{
    public BlEngineerCantTakeTask(string? message) : base(message) { }
    public BlEngineerCantTakeTask(string? message, Exception innerException) :
    base(message, innerException)
    { }
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