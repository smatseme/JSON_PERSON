﻿namespace JSON_PERSON.Service
{
    public class JsonReadWriteService : IJsonReadWriteService
    {
        private readonly IWebHostEnvironment _env;

        public JsonReadWriteService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public string Read(string fileName, string location)
        {
            string root = _env.WebRootPath;
            var path = Path.Combine(root, location, fileName);

            if (!File.Exists(path) || new FileInfo(path).Length == 0)
            {
                throw new Exception("File is empty or does not exist.");
            }

            using (StreamReader streamReader = new StreamReader(path))
            {
                return streamReader.ReadToEnd();
            }
        }

        public void Write(string fileName, string location, string jsonString)
        {
            string root = _env.WebRootPath;
            var path = Path.Combine(root, location, fileName);

            using (var streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jsonString);
            }
        }
    }
}
