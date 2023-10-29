using TournamentCreator.Data;

namespace TournamentCreator.Components
{
    public partial class Standings
    {
        private List<Team> _standings;

        protected override void OnInitialized()
        {
            _standings = Tournament.CurrentTournament.Standings;
            _standings.Sort((x, y) => y.Points.CompareTo(x.Points));
        }
    }
}