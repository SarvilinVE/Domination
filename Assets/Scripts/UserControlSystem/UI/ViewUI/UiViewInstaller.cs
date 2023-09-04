using Zenject;

namespace Domination.UIView
{
    public class UiViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BottomCenterView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}
