namespace HtmxProject.Infrastructure;

public class CurrentUser
{
    private string _name;

    public string Name => _name;

    public CurrentUser()
    {
        _name = "Defaultka";
    }

    public void SetName(string name)
    {
        _name = name;
    }
}
