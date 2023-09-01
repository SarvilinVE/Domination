namespace Domination.Abstractions
{
    public interface IAttackCommand : ICommand
    {
        IAttackable Target { get; }
    }
}
