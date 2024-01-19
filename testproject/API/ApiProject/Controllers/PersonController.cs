using Api.ViewModels;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.enities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonDeletter _personDeletter;
        private readonly ICSVUploader _csvUploader;
        private readonly IPersonGetter _personGetter;
        private readonly IPersonEditor _personEditor;
        private readonly IMapper _mapper;


        public PersonController(IPersonDeletter personDeletter, ICSVUploader csvUploader,
            IPersonGetter personGetter, IPersonEditor personEditor
            ,IMapper mapper
            )
        {
             _personDeletter = personDeletter;
            _csvUploader = csvUploader;
            _personGetter = personGetter;
            _personEditor = personEditor;
            _mapper = mapper;
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson([FromQuery] int id)
        {
            await _personDeletter.DeletePersonAsync(id);
            return Ok();
        }

        [HttpPost("UploadCSV")]
        public async Task<IActionResult> UploadCSV([FromForm] IFormFileCollection files)
        {
            await _csvUploader.UploadScv<Person,PersonMap>(files.First());

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
           var people = await _personGetter.GetPeople();
           var  result = _mapper.Map<List<PersonViewModel>>(people);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditPerson([FromQuery]int id, [FromBody] PersonModel personModel)
        {
            await _personEditor.EditInfo(id, personModel);
            return Ok();
        }
    }
}
