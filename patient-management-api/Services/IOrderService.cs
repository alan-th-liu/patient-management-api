using patient_management_api.Models.Api;

namespace patient_management_api.Services;

public interface IOrderService
{
    Task<OrderApiResponse> GetOrdersAsync(Guid patientId);

    Task<bool> UpdateAsync(OrderApiRequest request);
}