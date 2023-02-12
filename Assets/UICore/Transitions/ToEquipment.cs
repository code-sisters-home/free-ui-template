public class ToEquipment : Transition
{
    public override WindowFrame Enter()
    {
        base.Enter();
        //LayersController.Instance.LoadEquipment();
        return LayersController.Instance.Show<Equipment>();
    }

    public override void Exit()
    {
        base.Exit();
        //LayersController.Instance.UnloadEquipment();
        TransitionsController.Instance.GoTo_Menu();
    }
}
