namespace JSON_PERSON.Service
{
    public interface IJsonReadWriteService
    {
        string Read(string fileName, string location);
        void Write(string fileName, string location, string jsonString);
    }
}
