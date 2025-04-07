namespace Questao2.Domain.Interface
{
    public interface IHttpHelper
    {
        Task<ApiResponse> ApiCallAsync(string team, int year, int teamNumber, int page);
    }
}