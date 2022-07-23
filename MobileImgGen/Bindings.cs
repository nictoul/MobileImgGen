
using Ninject;

namespace MobileImgGen
{
    public static class Bindings
    {
        public static void MakeBindings()
        {
            Resolver.Kernel.Bind<IImgConverter>().To<MagickImgConverter>();
            Resolver.Kernel.Bind<IConfigParser>().To<ConfigParser>();
            Resolver.Kernel.Bind<IConsoleExecuter>().To<ConsoleExecuter>();
        }
    }

    public static class Resolver
    {
        public static readonly IKernel Kernel = new StandardKernel();
    }
}
