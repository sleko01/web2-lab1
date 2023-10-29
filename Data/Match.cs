namespace TournamentCreator.Data;

public class Match
{
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    public string Team1Score { get; set; }
    public string Team2Score { get; set; }

    public override string ToString()
    {
        return Team1 + " " + Team2 + " " + Team1Score + " " + Team2Score;
    }
}