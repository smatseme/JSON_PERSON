using JSON_PERSON.Models;
using JSON_PERSON.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JSON_PERSON.Controllers
{
    public class PersonController : Controller
    {
        private readonly IJsonReadWriteService _jsonReadWriteService;

        public PersonController(IJsonReadWriteService jsonReadWriteService)
        {
            _jsonReadWriteService = jsonReadWriteService;
        }

        public IActionResult Index()
        {
            List<PersonModel> people = JsonConvert.DeserializeObject<List<PersonModel>>(_jsonReadWriteService.Read("people.json", "data"));
            return View(people);
        }

        [HttpPost]
        public IActionResult Index(PersonModel personModel)
        {
            List<PersonModel> people = JsonConvert.DeserializeObject<List<PersonModel>>(_jsonReadWriteService.Read("people.json", "data"));

            PersonModel person = people.FirstOrDefault(x => x.Id == personModel.Id);

            if (person == null)
            {
                people.Add(personModel);
            }
            else
            {
                int index = people.FindIndex(x => x.Id == personModel.Id);
                people[index] = personModel;
            }

            string jsonString = JsonConvert.SerializeObject(people);
            _jsonReadWriteService.Write("people.json", "data", jsonString);

            return View(people);
        }
    }
}
    //public class JSONReadWrite
    //{
    //    public JSONReadWrite() { }

    //    public string Read(string fileName, string location)
    //    {
    //        string root = "wwwroot";
    //        var path = Path.Combine(
    //            Directory.GetCurrentDirectory(),
    //            root,
    //            location,
    //            fileName);

    //        string jsonResult;

    //        using (StreamReader streamReader = new StreamReader(path))
    //        {
    //            jsonResult = streamReader.ReadToEnd();
    //        }
    //        return jsonResult;
    //    }

    //    public void Write(string fileName, string location, string jSONString)
    //    {
    //        string root = "wwwroot";
    //        var path = Path.Combine(
    //            Directory.GetCurrentDirectory(),
    //            root,
    //            location,
    //            fileName);

    //        using (var streamWriter = File.CreateText(path))
    //        {
    //            streamWriter.Write(jSONString);
    //        }
    //    }
    //}
//}
