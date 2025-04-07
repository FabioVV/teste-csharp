using Newtonsoft.Json;
using Questao2.Domain.Interface;

public class GoalService : IGoalService
{
    private IHttpHelper _httpHelper { get; set; }
    private IGoalHelper _goalHelper { get; set; }

    public GoalService(IHttpHelper httpHelper, IGoalHelper goalHelper)
    {
        _httpHelper = httpHelper;
        _goalHelper = goalHelper;
    }

    public async Task<int> RequestScoredGoals(string team, int year)
    {
        int goals = 0;

        for (int i = 1; i <= 2; i++)
        {

            ApiResponse resp = await _httpHelper.ApiCallAsync(team, year, i, 1);

            goals += _goalHelper.SumGoals(resp.Data, i);
            int total_pages = resp.TotalPages;

            for (int j = 2; j <= total_pages; j++)
            {
                resp = await _httpHelper.ApiCallAsync(team, year, i, j);
                goals += _goalHelper.SumGoals(resp.Data, i);
            }

        }

        return goals;
    }

}