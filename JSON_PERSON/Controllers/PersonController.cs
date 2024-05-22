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
            try
            {
                List<PersonModel> people = JsonConvert.DeserializeObject<List<PersonModel>>(_jsonReadWriteService.Read("people.json", "data"));
                return View(people);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new List<PersonModel>());
            }
        }

        [HttpPost]
        public IActionResult Index(PersonModel personModel)
        {
            if (!ModelState.IsValid)
            {
                // If the model is invalid, return the view with the model to display validation errors.
                return View(new List<PersonModel>());
            }

            try
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
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new List<PersonModel>());
            }
        }
    }
    
}
    