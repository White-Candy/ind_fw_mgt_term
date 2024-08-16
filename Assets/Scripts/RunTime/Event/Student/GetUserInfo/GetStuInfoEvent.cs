public class GetStuInfoEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        MessPackage mp = args[0] as MessPackage;

        StudentPanel panel = UITools.FindPanel<StudentPanel>();
        panel.Show(mp.ret);
    }
}