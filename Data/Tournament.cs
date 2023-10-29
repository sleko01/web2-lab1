namespace TournamentCreator.Data;

public class Tournament
{
    public string Name { get; set; }
    public string PointSystem { get; set; }
    public string[] Teams { get; set; }
    public List<List<Match>> Schedule { get; set; }
    public List<Team> Standings { get; set; }
    
    public static Tournament CurrentTournament { get; set; }
    
}