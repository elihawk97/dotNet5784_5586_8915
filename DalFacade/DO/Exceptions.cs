
using System.Runtime.Serialization;

namespace DO;
[Serializable]
public class DalBadDependency : Exception
{
    public DalBadDependency(string? message) : base(message) { }
}

[Serializable]
public class DalCircularDependency : Exception
{
    public DalCircularDependency(string? message) : base(message) { }
    public DalCircularDependency(string? message, Exception innerException) :
    base(message, innerException)
    { }
}
[Serializable]

public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}
[Serializable]

public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException (string? message) : base(message) { }
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


    [Serializable]
    public class DalXMLFileLoadCreateException : Exception
    {
        public DalXMLFileLoadCreateException()
        {
        }

        public DalXMLFileLoadCreateException(string? message) : base(message)
        {
        }

        public DalXMLFileLoadCreateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DalXMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }

