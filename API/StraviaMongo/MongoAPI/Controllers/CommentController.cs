using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoAPI.Models;
using MongoAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commService) =>
            _commentService = commService;


        [HttpGet]
        public async Task<List<Comment>> Get() =>
        await _commentService.GetAsync();


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Comment>> Get(string athid, string actid)
        {
            var comment = await _commentService.GetAsync(athid, actid);

            if (comment is null)
            {
                return NotFound();
            }

            return comment;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Comment comment)
        {
            await _commentService.CreateAsync(comment);

            return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string athid, string actid, Comment updatedComment)
        {
            var comment = await _commentService.GetAsync(athid, actid);

            if (comment is null)
            {
                return NotFound();
            }

            updatedComment.Text = comment.Text;
            

            await _commentService.UpdateAsync(athid, actid, updatedComment);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string athid, string actid)
        {
            var comment = await _commentService.GetAsync(athid, actid);

            if (comment is null)
            {
                return NotFound();
            }

            await _commentService.RemoveAsync(athid, actid);

            return NoContent();
        }


    }
}
