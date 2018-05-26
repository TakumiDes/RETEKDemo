using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RETEKDemo.DataProvider;
using RETEKDemo.Models;
using RETEKDemo.Models.Requesst;

namespace RETEKDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {

        private IMessageDataProvider _messageDataProvider;
        private IMapper _mapper;

        public MessagesController(IMessageDataProvider messagesDataProvider, IMapper mapper)
        {
            _messageDataProvider = messagesDataProvider;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new message
        /// </summary>
        /// <param name="message"> New message </param>
        /// <returns></returns>
        [HttpPost]
        [Route("message")]
        public async Task<IActionResult> Message([FromBody]MessageRequestDto message) {

            if (message.ParentId.HasValue) {
                bool isParentIdCorrect = await _messageDataProvider.IsCorrectParentId(message.ParentId.Value);
                if (!isParentIdCorrect) {
                    return BadRequest($"Invalid ParentId: {message.ParentId.Value}");
                }
            }
            var inseredMessage = await _messageDataProvider.AddMessage(_mapper.Map<MessageRequestDto, Messages>(message));
            return Ok(_mapper.Map<Messages, MessageResponseDto>(inseredMessage));
        }

        /// <summary>
        /// Get messages tree
        /// </summary>
        /// <param name="id"> Three id </param>
        /// <returns></returns>
        [HttpGet]
        [Route("tree/{id}")]
        public async Task<IActionResult> GetMessages([FromRoute]int id)
        {
            var messages = await _messageDataProvider.GetMessages(id);
            return Ok(_mapper.Map<List<MessageResponseDto>>(messages));
        }
    }
}
