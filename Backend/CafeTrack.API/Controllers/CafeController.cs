﻿using CafeTrack.Application.DTOs;
using CafeTrack.Application.Features.Cafes.Commands;
using CafeTrack.Application.Features.Cafes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CafeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CafeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CafeDto>>> GetCafes([FromQuery] string? location)
        {
            var query = new GetCafesByLocationQuery { Location = location };
            var result = await _mediator.Send(query);

            // Sort by number of employees in descending order
            var sortedResult = result.OrderByDescending(cafe => cafe.Employees).ToList();

            return Ok(sortedResult);
        }

        [HttpPost("cafe")]
        public async Task<IActionResult> CreateCafe([FromForm] CreateCafeCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCafes), new { id = result.Id }, result);
        }

        [HttpPut("cafe")]
        public async Task<IActionResult> UpdateCafe([FromBody] UpdateCafeCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound(); // Return 404 if the café doesn't exist
            }
            return NoContent(); // Return 204 if update was successful
        }

        [HttpDelete("cafe/{id}")]
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
            var command = new DeleteCafeCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound(); // Return 404 if the café doesn't exist
            }

            return NoContent(); // Return 204 if deletion was successful
        }

    }
}
