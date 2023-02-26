using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Data;
using ToDoListApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListApiController : ControllerBase
    {
        // GET: api/<ToDoListApiController>

        //Create context instans
        private readonly AppDbContext context;
        List<ToDoListModel> tasks = new();

        //Create Constructor
        public ToDoListApiController(AppDbContext context)
        {
            this.context = context;
            tasks = context.ToDoList.ToList();
        }


        [HttpGet]
        public ActionResult<IEnumerable<ToDoListModel>> Get()
        {
            return Ok(context.ToDoList.ToList());
        }

        // GET api/<ToDoListApiController>/5
        [HttpGet("{id}")]
        public ToDoListModel? Get(int id)
        {
            return context.ToDoList.FirstOrDefault(l => l.Id == id);
        }

        // POST api/<ToDoListApiController>
        [HttpPost]
        public IActionResult Post([FromBody] ToDoListModel task)
        {

            //List<ToDoListModel> tasks = context.ToDoList.ToList();
            ToDoListModel? foundtask = tasks.FirstOrDefault(t => t.TodoMessage == task.TodoMessage);

            if (foundtask == null)
            {
                context.ToDoList.Add(task);
                context.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest("The task already exists!");
            }
        }

        // PUT api/<ToDoListApiController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ToDoListModel task)
        {
            //List<ToDoListModel> tasks = context.ToDoList.ToList();
            var existingTask = tasks.FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.TodoMessage = task.TodoMessage;
            existingTask.Completed = task.Completed;
            context.SaveChanges();
            return Ok(task);

        }

        // DELETE api/<ToDoListApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //List<ToDoListModel> tasks = context.ToDoList.ToList();
            ToDoListModel? taskToDelete = tasks.FirstOrDefault(m => m.Id == id);

            if (taskToDelete == null)
            {
                return NotFound();
            }
          

            tasks.Remove(taskToDelete);
            context.SaveChanges();

            return Ok(taskToDelete);
        }
    }
}
