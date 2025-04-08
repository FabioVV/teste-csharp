public class GoalHelper : IGoalHelper
{
    public int SumGoals(List<Match> data, int teamNumber)
    {
        int goals = 0;

        foreach (var item in data)
        {
            if (teamNumber == 1)
            {
                goals += int.Parse(item.Team1Goals);
            }
            else
            {
                goals += int.Parse(item.Team2Goals);
            }
        }

        return goals;
    }
}
