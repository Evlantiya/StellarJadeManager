using System.Text.Json.Nodes;

public interface IPatchRepository{
    public Patch Get(string version);
    public IEnumerable<Patch> GetAll();

    public IEnumerable<Patch> GetRelevant();
}