using App_data.IRepositories;
using App_data.Models;
using App_data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatLieuController : ControllerBase
    {
        private readonly IAllRepositories<ChatLieu> chatlieus;
        AppDbcontext context= new AppDbcontext();
        public ChatLieuController()
        {
            chatlieus= new AllRepositories<ChatLieu>(context,context.ChatLieus);
        }
        // GET: api/<ChatLieuController>
        [HttpGet]
        public List<ChatLieu> Get()
        {
            return chatlieus.GetAll();
        }

        // GET api/<ChatLieuController>/5
        [HttpGet("{id}")]
        public ChatLieu Get(Guid id)
        {
            return chatlieus.GetAll().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<ChatLieuController>
        [HttpPost]
        public bool Post(string Ten, int TrangThai)
        {
            ChatLieu chatlieu = new ChatLieu();
            chatlieu.Id = Guid.NewGuid();
            chatlieu.Ten = Ten;
            chatlieu.TrangThai = TrangThai;
            return chatlieus.Add(chatlieu);
        }

        // PUT api/<ChatLieuController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string Ten, int TrangThai)
        {
            var chatlieu = chatlieus.GetAll().FirstOrDefault(c => c.Id == id);
            if (chatlieu != null)
            {
                chatlieu.Ten = Ten;
                chatlieu.TrangThai = TrangThai;
                return chatlieus.Update(chatlieu);
            }
            else
            {
                return false;

            }
        }

        // DELETE api/<ChatLieuController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var chatlieu = chatlieus.GetAll().FirstOrDefault(c => c.Id == id);
            if (chatlieu != null)
            {

                return chatlieus.Delete(chatlieu);
            }
            else
            {
                return false;
            }
        }
    }
}
