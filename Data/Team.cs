namespace TournamentCreator.Data;

public class Team
{
    public string Name { get; set; }
    public double Points { get; set; }

    public override string ToString()
    {
        return Name;
    }
}