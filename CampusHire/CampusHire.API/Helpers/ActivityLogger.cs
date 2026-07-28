using CampusHire.API.Services.Interfaces;

namespace CampusHire.API.Helpers
{
    public class ActivityLogger
    {
        private readonly IAdminActivityService _service;
        public ActivityLogger(
            IAdminActivityService service)
        {
            _service = service;
        }
        public async Task Log(
            int adminId,
            string action,
            string description)
        {
            await _service.AddAsync(
                adminId,
                action,
                description
            );
        }
    }
}