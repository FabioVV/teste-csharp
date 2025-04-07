
public interface IGoalService
{
    Task<int> RequestScoredGoals(string team, int year);
}