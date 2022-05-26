using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StraviaMongo.Models;
using StraviaMongo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public ActionResult<List<Comment>> Get()
        {
            return _commentService.Get();
        }

        [HttpPost]
        public ActionResult<Comment> Create(Comment comment)
        {
            _commentService.Create(comment);
            return Ok(comment);
        }

        [HttpPut]
        public  ActionResult Update(Comment comment)
        {
            _commentService.Update(comment.Id, comment);
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            _commentService.Delete(id);
            return Ok();
        }

    }
}
