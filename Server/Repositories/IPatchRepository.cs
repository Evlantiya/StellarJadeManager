using System.Text.Json.Nodes;

public interface IPatchRepository{
    public PatchDTO Get(string version);
    public IEnumerable<PatchDTO> GetAll();

    public IEnumerable<PatchDTO> GetRelevant();
}