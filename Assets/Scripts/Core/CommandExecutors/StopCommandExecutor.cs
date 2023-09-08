using Domination.Abstractions;
using System.Threading;
using System.Threading.Tasks;


namespace Domination.Core.CommandExecutor
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {

        #region Properties

        public CancellationTokenSource CancellationTokenSource { get; set; }

        #endregion


        #region ClassLifeCycle

        public override async Task ExecuteSpecificCommand(IStopCommand command)
        {
            await Task.CompletedTask;
            CancellationTokenSource?.Cancel();
        }

        #endregion

    }
}