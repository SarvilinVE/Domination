using Domination.Abstractions;
using System.Threading;


namespace Domination.Core.CommandExecutor
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {

        #region Properties

        public CancellationTokenSource CancellationTokenSource { get; set; }

        #endregion


        #region ClassLifeCycle

        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }

        #endregion

    }
}