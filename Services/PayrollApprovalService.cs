using qwikhr.Interfaces;
using qwikhr.Models;
using qwikhr.Models.Payroll;

namespace qwikhr.Services
{
    public class PayrollApprovalService
    {
        private readonly IPayrollApprovalRepository _approvalRepository;
        private readonly IPayrollApprovalHistoryRepository _historyRepository;

        public PayrollApprovalService(
            IPayrollApprovalRepository approvalRepository,
            IPayrollApprovalHistoryRepository historyRepository)
        {
            _approvalRepository = approvalRepository;
            _historyRepository = historyRepository;
        }

        public async Task<bool> StartApprovalWorkflowAsync(Guid payrollRunId)
        {
            var approval = await _approvalRepository.GetByPayrollRunIdAsync(payrollRunId)
                ?? throw new Exception("Approval workflow not found.");

            if (approval.CurrentPayrollApprovalLevel == null)
                throw new Exception("Current approval level is not configured.");

            // Ensure the current approval level has a valid approver
            if (approval.CurrentPayrollApprovalLevel.ApproverId == Guid.Empty)
                throw new Exception("No approver is configured for the current approval level.");

            approval.Status = PayrollApprovalStatus.Pending;
            await _approvalRepository.UpdateAsync(approval);

            return true;
        }

        public async Task<bool> ApproveAsync(Guid approvalId, Guid approverId, string comments)
        {
            var approval = await _approvalRepository.GetByIdAsync(approvalId)
                ?? throw new Exception("Approval not found.");

            if (approval.Status == PayrollApprovalStatus.Approved)
                throw new Exception("This payroll has already been approved.");
            if (approval.Status == PayrollApprovalStatus.Rejected)
                throw new Exception("This payroll has already been rejected.");

            if (approval.CurrentPayrollApprovalLevel == null)
                throw new Exception("Current approval level is not configured.");

            if (approval.CurrentPayrollApprovalLevel.ApproverId != approverId)
                throw new Exception("You are not authorized to approve this payroll.");

            var history = new PayrollApprovalHistory
            {
                PayrollApprovalId = approval.Id,
                ApproverId = approverId,
                ApprovalLevelId = approval.CurrentApprovalLevelId,
                Status = ApprovalStatus.Approved,
                Comments = comments,
                ApprovedAt = DateTime.UtcNow,
                PayrollApproval = approval,
                Approver = approval.CurrentPayrollApprovalLevel.Approver,
                ApprovalLevel = approval.CurrentPayrollApprovalLevel
            };
            await _historyRepository.AddAsync(history);

            var nextLevel = GetNextApprovalLevel(approval);
            if (nextLevel != null)
            {
                approval.CurrentApprovalLevelId = nextLevel.Id;
            }
            else
            {
                approval.Status = PayrollApprovalStatus.Approved;
            }

            await _approvalRepository.UpdateAsync(approval);
            return true;
        }

        public async Task<bool> RejectAsync(Guid approvalId, Guid approverId, string comments)
        {
            var approval = await _approvalRepository.GetByIdAsync(approvalId)
                ?? throw new Exception("Approval not found.");

            if (approval.Status == PayrollApprovalStatus.Approved)
                throw new Exception("This payroll has already been approved.");
            if (approval.Status == PayrollApprovalStatus.Rejected)
                throw new Exception("This payroll has already been rejected.");

            if (approval.CurrentPayrollApprovalLevel == null)
                throw new Exception("Current approval level is not configured.");

            if (approval.CurrentPayrollApprovalLevel.ApproverId != approverId)
                throw new Exception("You are not authorized to reject this payroll.");

            var history = new PayrollApprovalHistory
            {
                PayrollApprovalId = approval.Id,
                ApproverId = approverId,
                ApprovalLevelId = approval.CurrentApprovalLevelId,
                Status = ApprovalStatus.Rejected,
                Comments = comments,
                ApprovedAt = DateTime.UtcNow,
                PayrollApproval = approval,
                Approver = approval.CurrentPayrollApprovalLevel.Approver,
                ApprovalLevel = approval.CurrentPayrollApprovalLevel
            };
            await _historyRepository.AddAsync(history);

            approval.Status = PayrollApprovalStatus.Rejected;
            await _approvalRepository.UpdateAsync(approval);

            return true;
        }

        public async Task<List<PayrollApprovalHistory>> GetApprovalHistoryAsync(Guid approvalId)
        {
            var history = await _historyRepository.GetByApprovalIdAsync(approvalId);

            if (history == null || !history.Any())
                throw new Exception("No approval history found for the specified approval.");

            return history;
        }

        private CompanyPayrollApprovalLevel? GetNextApprovalLevel(PayrollApproval approval)
        {
            var allLevels = approval.CurrentPayrollApprovalLevel.Workflow.ApprovalLevels
                .OrderBy(level => level.Order)
                .ToList();

            var currentLevelIndex = allLevels.FindIndex(level => level.Id == approval.CurrentApprovalLevelId);

            if (currentLevelIndex >= 0 && currentLevelIndex < allLevels.Count - 1)
            {
                return allLevels[currentLevelIndex + 1];
            }

            return null;
        }
    }
}