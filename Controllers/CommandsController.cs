using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    //Controller class should inherit ControllerBase class from Microsoft.AspNetCore.Mvc (Controller class support Views for MVC, ConrollerBase does not and can be used for API)
    // Add ApiController atribute to the controller class. The [ApiController] attribute enables a few features including
    // attribute routing requirement, automatic model validation and binding source parameter inference.
    // Atribute [Route] - route to the contoller methods
    [ApiController]
    [Route("api/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        // Direct way to use repository class without using interface (w/o Dependency Injection)

        //private readonly MockCommanderRepo _repository = new();
        // To use Dependency Injection patern needs to add a line to Service Builder (program.cs file - builder.Services.AddScoped<ICommanderRepo, MockCommanderRepo>();) 
        //interface and disired implementation (ICommanderRepo -> MockCommanderRepo)

        //public CommandsController(ICommanderRepo repository)  constructor without automapper
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper; 
        }
        // Get request like ../api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            //return Ok(commandItems);   w/o DTOs
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }


        // Get request like ../api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem!=null)
            {
                //return Ok(commandItem);  w/o DTOs
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        // POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto cmd)
        {
            var commandModel = _mapper.Map<Command>(cmd);   // convert Read-DTO model to Database model
            _repository.CreateCommand(commandModel);           // invoke method to create new DB entry. This method does not change real data in DB, just create changes wich has to be applied by invoking SaveChanges method

            _repository.SaveChanges();                              // Must be apply changes for DB - means realy sends data to DB which done in previous line. 

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);  

            //return Ok(commandModel);//  Just return Ok response (200). Best practice recommends to return Created response (201)
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto); //returns Created response with link to just created resource in responce Header. Mandatory!!! Get method passed to nameof() must be named (Name attribute for the method.)
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto) //id comes from link and commandUpdateDto comes from body(json)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            // It is important to have full model object to update in database. Next line do update DTO model from repository with Id==id with value from request body
            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);    // Invoke update for database
            _repository.SaveChanges();                                // Apply DB changes
            return NoContent();
       }
    }
}