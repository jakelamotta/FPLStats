using Unity;

namespace ServiceInstance
{
    public class ServiceInstance
    {
        public void Run()
        {
            var container = Bootstrapper.CreateUnityContainer();

            var instance = container.Resolve<FplStats>();
            //instance.UpdateData();
            instance.Calculate();
        }
    }
}
