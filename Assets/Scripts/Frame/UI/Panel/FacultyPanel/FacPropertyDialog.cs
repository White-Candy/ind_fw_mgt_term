
public class FacPropertyDialog : BasePanel
{
    public static FacPropertyDialog instance;

    public override void Awake()
    {
        base.Awake();

        instance = this;
        Active(false);
    }
}