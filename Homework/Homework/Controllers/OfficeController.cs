using Homework.Common;
using Homework.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Homework.Controllers
{
    public class CreateOfficeDto
    {
        public string AcpdCname { get; set; }

        public string AcpdEname { get; set; }

        public string AcpdSname { get; set; }

        public string AcpdEmail { get; set; }

        public byte? AcpdStatus { get; set; }

        public bool? AcpdStop { get; set; }

        public string AcpdStopMemo { get; set; }

        public string AcpdLoginId { get; set; }

        public string AcpdLoginPwd { get; set; }

        public string AcpdMemo { get; set; }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly MercuryfireHomeworkContext _context;
        private readonly IMercuryfireHomeworkContextProcedures _sp;

        public OfficeController(MercuryfireHomeworkContext context)
        {
            _context = context;
            _sp = context.Procedures;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyOfficeAcpd>>> GetUsers()
        {
            return await _context.MyOfficeAcpds.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MyOfficeAcpd>> GetUser(string id)
        {
            var user = await _context.MyOfficeAcpds.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException("");
            }
            return user;
        }

      
        [HttpPost]
        public async Task CreateUser(CreateOfficeDto user)
        {
            var newUser = new MyOfficeAcpd
            {
                AcpdCname = user.AcpdCname,
                AcpdEname = user.AcpdEname,
                AcpdSname = user.AcpdSname,
                AcpdEmail = user.AcpdEmail,
                AcpdStatus = user.AcpdStatus,
                AcpdStop = user.AcpdStop,
                AcpdStopMemo = user.AcpdStopMemo,
                AcpdLoginId = user.AcpdLoginId,
                AcpdLoginPwd = user.AcpdLoginPwd,
                AcpdMemo = user.AcpdMemo,
                AcpdNowDateTime = DateTime.Now
            };
            throw new Exception("新增");
            var output = new OutputParameter<string>();
            await _sp.NEWSIDAsync(nameof(MyOfficeAcpd), output);
            newUser.AcpdSid = output.Value;
            newUser.AcpdNowId = "createUser";   
            _context.MyOfficeAcpds.Add(newUser);
            await _context.SaveChangesAsync();
  
        }

   
        [HttpPut("{id}")]
        public async Task UpdateUser(string id, CreateOfficeDto updateUser)
        {

            var user = await _context.MyOfficeAcpds.FindAsync(id);
            if(user==null)
            {
                throw new NotFoundException("");
            }
            user.AcpdSname= updateUser.AcpdSname;
            user.AcpdCname = updateUser.AcpdCname;
            user.AcpdEname = updateUser.AcpdEname;
            user.AcpdEmail = updateUser.AcpdEmail;
            user.AcpdStatus = updateUser.AcpdStatus;
            user.AcpdStop = updateUser.AcpdStop;
            user.AcpdStopMemo = user.AcpdStopMemo;
            user.AcpdLoginId = user.AcpdLoginId;
            user.AcpdLoginPwd = user.AcpdLoginPwd;
            user.AcpdMemo = user.AcpdMemo;

            user.AcpdUpdid = "updateUser";
            user.AcpdUpddateTime = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }

    
        [HttpDelete("{id}")]
        public async Task DeleteUser(string id)
        {
            var user = await _context.MyOfficeAcpds.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException("");
            }

            _context.MyOfficeAcpds.Remove(user);
            await _context.SaveChangesAsync();


        }
    }


}
