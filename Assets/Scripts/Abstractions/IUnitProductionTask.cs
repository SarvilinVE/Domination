namespace Domination.Abstractions
{
    public interface IUnitProductionTask : IIconHolder
    {
        public string UnitName { get; }
        float TimeLeft { get; }
        float ProductionTime { get; }
    }
}
