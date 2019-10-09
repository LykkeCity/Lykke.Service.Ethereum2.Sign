using Autofac;
using Lykke.Service.Ethereum2.Sign.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.Ethereum2.Sign.Modules
{
    public class ServiceModule : Module
    {
        public ServiceModule(IReloadingManager<AppSettings> settings)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
        }
    }
}
