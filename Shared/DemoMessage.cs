namespace Shared;

public class DemoMessage
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public DateTime CreatAt { get; set; }

    public DemoMessage()
    {
        Id = Guid.NewGuid();
    }
}
