using HR_Payroll.Core.Entities;
using HR_Payroll.Core.Interfaces.Services;
using HR_Payroll.Shared.DTOs.PayrollDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayrollRecord>>> GetAllPayrollRecords([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var payrollRecords = await _payrollService.GetAllPayrollRecordsAsync(page, pageSize);
            return Ok(payrollRecords);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PayrollRecord>> GetPayrollRecordById(int id)
        {
            try
            {
                var payrollRecord = await _payrollService.GetPayrollRecordByIdAsync(id);
                return Ok(payrollRecord);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PayrollRecord>> CreatePayrollRecord(PayrollRecord payrollRecord)
        {
            var createdPayrollRecord = await _payrollService.CreatePayrollRecordAsync(payrollRecord);
            return CreatedAtAction(nameof(GetPayrollRecordById), new { id = createdPayrollRecord.Id }, createdPayrollRecord);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayrollRecord(int id, PayrollRecord payrollRecord)
        {
            if (id != payrollRecord.Id)
            {
                return BadRequest("Payroll Record ID mismatch");
            }

            try
            {
                await _payrollService.UpdatePayrollRecordAsync(payrollRecord);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayrollRecord(int id)
        {
            try
            {
                await _payrollService.DeletePayrollRecordAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("generate-payslip")]
        public async Task<ActionResult<PayslipDto>> GeneratePayslip(GeneratePayslipDto generatePayslipDto)
        {
            try
            {
                var payslip = await _payrollService.GeneratePayslipAsync(generatePayslipDto);
                return Ok(payslip);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("process-payroll")]
        public async Task<ActionResult<PayrollProcessingResultDto>> ProcessPayroll(ProcessPayrollDto processPayrollDto)
        {
            var result = await _payrollService.ProcessPayrollAsync(processPayrollDto);
            return Ok(result);
        }

        [HttpPost("apply-adjustment")]
        public async Task<ActionResult<PayrollRecord>> ApplyPayrollAdjustment(PayrollAdjustmentDto payrollAdjustmentDto)
        {
            try
            {
                var updatedPayrollRecord = await _payrollService.ApplyPayrollAdjustmentAsync(payrollAdjustmentDto);
                return Ok(updatedPayrollRecord);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("summary")]
        public async Task<ActionResult<PayrollSummaryDto>> GetPayrollSummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var summary = await _payrollService.GetPayrollSummaryAsync(startDate, endDate);
            return Ok(summary);
        }
    }
}