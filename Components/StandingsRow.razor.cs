using Microsoft.AspNetCore.Components;

namespace TournamentCreator.Components
{
    public partial class StandingsRow
    {
        [Parameter]
        public int Position { get; set; }
        [Parameter]
        public string TeamName { get; set; }
        [Parameter]
        public double Points { get; set; }
    }
}