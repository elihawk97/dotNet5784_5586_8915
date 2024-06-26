﻿namespace BO;

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string? message, Exception innerException) :
    base(message, innerException)
    { }
}

[Serializable]

public class BLInvalidDateException : Exception
{
    public BLInvalidDateException(string? message) : base(message) { }
    public BLInvalidDateException(string? message, Exception innerException) :
    base(message, innerException)
    { }
}

[Serializable]
public class BLNoEngineerException : Exception
{
    public BLNoEngineerException(string? message) : base(message) { }
    public BLNoEngineerException(string? message, Exception innerException) :
    base(message, innerException)
    { }
}


[Serializable]
public class BlCircularDependency : Exception
{
    public BlCircularDependency(string? message) : base(message) { }
    public BlCircularDependency(string? message, Exception innerException) :
    base(message, innerException)
    { }
}


[Serializable]
public class BlTasksCanNotBeScheduled : Exception
{
    public BlTasksCanNotBeScheduled(string? message) : base(message) { }
    public BlTasksCanNotBeScheduled(string? message, Exception innerException) :
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



