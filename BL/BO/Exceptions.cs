namespace BO
{
    [Serializable]
    public class BlDoesNotExistException : Exception
    {
        public BlDoesNotExistException(string? message) : base(message) { }
        public BlDoesNotExistException(string? message, Exception innerEx) : base(message, innerEx) { }
    }
    [Serializable]
    public class BlAlreadyExistsException : Exception
    {
        public BlAlreadyExistsException(string? message) : base(message) { }
        public BlAlreadyExistsException(string? message, Exception innerEx) : base(message, innerEx) { }
    }
    [Serializable]
    public class BlNullPropertyException : Exception
    {
        public BlNullPropertyException(string? message) : base(message) { }
    }
    [Serializable]
    public class BlWrongCategoryException : Exception
    {
        public BlWrongCategoryException(string? message) : base(message) { }
    }
    [Serializable]
    public class BlCantSetValue : Exception
    {
        public BlCantSetValue(string? message) : base(message) { }
    }
    [Serializable]
    public class BlInvalidvalueException : Exception
    {
        public BlInvalidvalueException(string? message) : base(message) { }
    }

    [Serializable]
    public class BlDeletionImpossible : Exception
    {
        public BlDeletionImpossible(string? message) : base(message) { }
    }
    //public class BlXMLFileLoadCreateException : Exception
    //{
    //    public BlXMLFileLoadCreateException(string? message) : base(message) { }
    //}

}
