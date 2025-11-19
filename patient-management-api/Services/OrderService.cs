using patient_management_api.Models.Api;
using patient_management_api.Models.Entities;
using patient_management_api.Repositories;
using Serilog;

namespace patient_management_api.Services;

public class OrderService(IOrderRepository orderRepository, IPatientRepository patientRepository) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IPatientRepository _patientRepository = patientRepository;

    public async Task<OrderApiResponse> GetOrdersAsync(Guid patientId)
    {
        var order = await _orderRepository.GetByPatientAsync(patientId);
        if (order == null)
            throw new Exception("Order not found, patientId: " + patientId);

        return new OrderApiResponse
        {
            Id = order.Id,
            Message = order.Message,
            PatientId = patientId
        };
    }

    public async Task<bool> UpdateAsync(OrderApiRequest request)
    {
        var newOrderId = Guid.NewGuid();
        var newOrder = new PatientOrder
        {
            Id = newOrderId,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow
        };

        await _orderRepository.AddAsync(newOrder);

        var patient = await _patientRepository.GetByIdAsync(request.PatientId);
        if (patient == null)
        {
            Log.Error("Patient not found, PatientId: {RequestPatientId}", request.PatientId);
            return false;
        }

        patient.OrderId = newOrderId;
        await _patientRepository.UpdateAsync(patient);

        return true;
    }
}