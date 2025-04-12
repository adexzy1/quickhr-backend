using Microsoft.AspNetCore.Mvc;
using qwikhr.Services;

namespace qwikhr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollApprovalController : ControllerBase
    {
        private readonly PayrollApprovalService _approvalService;
        private readonly ILogger<PayrollApprovalController> _logger;

        public PayrollApprovalController(PayrollApprovalService approvalService, ILogger<PayrollApprovalController> logger)
        {
            _approvalService = approvalService;
            _logger = logger;
        }

        // Start the approval workflow
        [HttpPost("{payrollRunId}/start")]
        public async Task<IActionResult> StartApprovalWorkflow(Guid payrollRunId)
        {
            try
            {
                var result = await _approvalService.StartApprovalWorkflowAsync(payrollRunId);
                return Ok(new { Success = result, Message = "Approval workflow started successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting approval workflow for PayrollRunId: {PayrollRunId}", payrollRunId);
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // Approve a payroll
        [HttpPost("{approvalId}/approve")]
        public async Task<IActionResult> Approve(Guid approvalId, [FromQuery] Guid approverId, [FromBody] string comments)
        {
            try
            {
                var result = await _approvalService.ApproveAsync(approvalId, approverId, comments);
                return Ok(new { Success = result, Message = "Payroll approved successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving payroll for ApprovalId: {ApprovalId}", approvalId);
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // Reject a payroll
        [HttpPost("{approvalId}/reject")]
        public async Task<IActionResult> Reject(Guid approvalId, [FromQuery] Guid approverId, [FromBody] string comments)
        {
            try
            {
                var result = await _approvalService.RejectAsync(approvalId, approverId, comments);
                return Ok(new { Success = result, Message = "Payroll rejected successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rejecting payroll for ApprovalId: {ApprovalId}", approvalId);
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // Get approval history
        [HttpGet("{approvalId}/history")]
        public async Task<IActionResult> GetApprovalHistory(Guid approvalId)
        {
            try
            {
                var history = await _approvalService.GetApprovalHistoryAsync(approvalId);
                return Ok(new { Success = true, Data = history });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving approval history for ApprovalId: {ApprovalId}", approvalId);
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }
    }
}