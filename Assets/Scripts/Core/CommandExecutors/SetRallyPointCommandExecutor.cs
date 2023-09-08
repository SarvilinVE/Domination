using Domination.Abstractions;
using System.Threading.Tasks;


namespace Domination.Core
{
    public class SetRallyPointCommandExecutor : CommandExecutorBase<ISetRallyPointCommand>
    {

        #region Methods

        public override Task ExecuteSpecificCommand(ISetRallyPointCommand command)
        {
            GetComponent<MainBuilding>().RallyPoint = command.RallyPoint;
            return Task.CompletedTask;
        }

        #endregion

    }
}
