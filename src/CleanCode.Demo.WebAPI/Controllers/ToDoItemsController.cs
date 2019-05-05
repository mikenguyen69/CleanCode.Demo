using AutoMapper;
using CleanCode.Demo.Core.Entities;
using CleanCode.Demo.Core.Interfaces;
using CleanCode.Demo.WebAPI.DTO;
using CleanCode.Demo.WebAPI.Filters;
using System.Linq;
using System.Web.Http;

namespace CleanCode.Demo.WebAPI.Controllers
{
    [ValidateModel]
    public class ToDoItemsController : ApiController
    {
        private IRepository<ToDoItem> _repository;
        private IMapper _mapper;

        public ToDoItemsController(IRepository<ToDoItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var items = _repository.List()
                .Select(item => _mapper.Map<ToDoItem, ToDoItemDTO>(item));
            return Ok(items);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var item = _repository.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(_mapper.Map<ToDoItem, ToDoItemDTO>(item));
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]ToDoItemDTO item)
        {
            var toDoItem = new ToDoItem()
            {
                Title = item.Title,
                Description = item.Description
            };

            if (item.Id > 0)
            {
                toDoItem.Id = item.Id;
            }

            _repository.Add(toDoItem);

            return Ok(_mapper.Map<ToDoItem, ToDoItemDTO>(toDoItem));
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]ToDoItemDTO item)
        {
            var toDoItem = _mapper.Map<ToDoItemDTO, ToDoItem>(item);

            _repository.Update(toDoItem);

            return Ok(_mapper.Map<ToDoItem, ToDoItemDTO>(toDoItem));
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var item = _repository.GetById(id);

            if (item == null)
                return NotFound();

            _repository.Delete(item);

            return Ok(_mapper.Map<ToDoItem, ToDoItemDTO>(item));
        }
    }
}
