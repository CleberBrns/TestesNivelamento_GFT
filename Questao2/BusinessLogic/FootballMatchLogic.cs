using Questao2.Models.Result;
using Questao2.Service;

namespace Questao2.BusinessLogic
{
    public class FootballMatchLogic
    {
        public static int GetGoalsFromFootballMatches(string team, int year)
        {
            try
            {
                List<FootballMatchResult> listAsTeam1 = FootballMatchService.GetFootballMatches(team, 1, year).Result;
                List<FootballMatchResult> listAsTeam2 = FootballMatchService.GetFootballMatches(team, 2, year).Result;

                int goalsAsTeam1 = GetGoalsFromMatchListAsTeam1(listAsTeam1, team);
                int goalsAsTeam2 = GetGoalsFromMatchListAsTeam2(listAsTeam2, team);

                return goalsAsTeam1 + goalsAsTeam2;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        static int GetGoalsFromMatchListAsTeam1(List<FootballMatchResult> listFootBallMatch, string team)
        {
            if (listFootBallMatch != null && listFootBallMatch.Any())
            {
                return listFootBallMatch.Where(x => x.team1.Equals(team)).Select(y => y.team1Goals).Sum();
            }

            return 0;
        }

        static int GetGoalsFromMatchListAsTeam2(List<FootballMatchResult> listFootBallMatch, string team)
        {
            if (listFootBallMatch != null && listFootBallMatch.Any())
            {
                return listFootBallMatch.Where(x => x.team2.Equals(team)).Select(y => y.team2Goals).Sum();
            }

            return 0;
        }
    }
}
