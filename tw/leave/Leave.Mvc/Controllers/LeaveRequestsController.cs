using AutoMapper;
using Leave.Mvc.Contracts;
using Leave.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Leave.Mvc.Controllers
{
    [Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IMapper _mapper;

        public LeaveRequestsController(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService,
            IMapper mapper)
        {
            this._leaveTypeService = leaveTypeService;
            this._leaveRequestService = leaveRequestService;
            this._mapper = mapper;
        }

        // GET: LeaveRequest/Create
        public async Task<ActionResult> Create()
        {
            var leaveTypes = await _leaveTypeService.GetLeaveTypes();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypeItems
            };
            return View(model);
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestVM leaveRequest)
        {
            if (!ModelState["StartDate"].Errors.Any() &&
                !ModelState["EndDate"].Errors.Any() &&
                !ModelState["LeaveTypeId"].Errors.Any() &&
                !ModelState["RequestComments"].Errors.Any()
               )
            //if (ModelState.IsValid)
            {
                var response = await _leaveRequestService.CreateLeaveRequest(leaveRequest);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                    //return RedirectToAction(nameof(Details), new { Id = response..Message. });
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }

            var leaveTypes = await _leaveTypeService.GetLeaveTypes();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
            leaveRequest.LeaveTypes = leaveTypeItems;

            return View(leaveRequest);
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveRequest
        public async Task<ActionResult> Index()
        {
            var model = await _leaveRequestService.GetAdminLeaveRequestList();
            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            //var leaveRequest = await _leaveRequestService.GetLeaveRequest(id);
            //var model = _mapper.Map<LeaveRequestVM>(leaveRequest);
            var model = await _leaveRequestService.GetLeaveRequest(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
                await _leaveRequestService.ApproveLeaveRequest(id, approved);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
