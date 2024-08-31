namespace Shared;

public abstract class Entity
{
    public Guid? Code { get; set; }

    protected Entity()
    {
        Code = Guid.NewGuid();
    }

}
