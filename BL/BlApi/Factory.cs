namespace BlApi;
/// <summary>
/// Factory class to create a Bl object
/// </summary>
    public static class Factory
    {
        public static IBl Get() => new BlImplementation.Bl();
    }

