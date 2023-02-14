using Leave.Mvc.Contracts;
using Leave.Mvc.Services.Base;

namespace Leave.Mvc.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IClient _httpclient;

        public LeaveAllocationService(IClient httpclient, ILocalStorageService localStorageService) : base(httpclient, localStorageService)
        {
            this._localStorageService = localStorageService;
            this._httpclient = httpclient;
        }

        public async Task<Response<int>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveAllocationDto createLeaveAllocation = new() { LeaveTypeId = leaveTypeId };
                AddBearerToken();
                var apiResponse = await _client.LeaveAllocationPOSTAsync(createLeaveAllocation);
                if (apiResponse.Success)
                {
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
