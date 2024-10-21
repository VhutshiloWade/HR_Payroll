using HR_Payroll.Core.Entities;
using HR_Payroll.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeOffController : ControllerBase
    {
        private readonly ITimeOffService _timeOffService;

        public TimeOffController(ITimeOffService timeOffService)
        {
            _timeOffService = timeOffService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeOffRequest>>> GetAllTimeOffRequests()
        {
            var timeOffRequests = await _timeOffService.GetAllTimeOffRequestsAsync();
            return Ok(timeOffRequests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeOffRequest>> GetTimeOffRequestById(int id)
        {
            try
            {
                var timeOffRequest = await _timeOffService.GetTimeOffRequestByIdAsync(id);
                return Ok(timeOffRequest);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TimeOffRequest>> CreateTimeOffRequest(TimeOffRequest timeOffRequest)
        {
            try
            {
                var createdTimeOffRequest = await _timeOffService.CreateTimeOffRequestAsync(timeOffRequest);
                return CreatedAtAction(nameof(GetTimeOffRequestById), new { id = createdTimeOffRequest.Id }, createdTimeOffRequest);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeOffRequest(int id, TimeOffRequest timeOffRequest)
        {
            if (id != timeOffRequest.Id)
            {
                return BadRequest("Time Off Request ID mismatch");
            }

            try
            {
                await _timeOffService.UpdateTimeOffRequestAsync(timeOffRequest);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeOffRequest(int id)
        {
            try
            {
                await _timeOffService.DeleteTimeOffRequestAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<TimeOffRequest>>> GetTimeOffRequestsByEmployee(int employeeId)
        {
            var timeOffRequests = await _timeOffService.GetTimeOffRequestsByEmployeeIdAsync(employeeId);
            return Ok(timeOffRequests);
        }

        [HttpGet("balance/{employeeId}")]
        public async Task<ActionResult<TimeOffBalance>> GetTimeOffBalance(int employeeId)
        {
            try
            {
                var timeOffBalance = await _timeOffService.GetTimeOffBalanceAsync(employeeId);
                return Ok(timeOffBalance);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("balance")]
        public async Task<IActionResult> UpdateTimeOffBalance(TimeOffBalance timeOffBalance)
        {
            try
            {
                await _timeOffService.UpdateTimeOffBalanceAsync(timeOffBalance);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
