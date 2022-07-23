using Ninject;

namespace MobileImgGen
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Bindings.MakeBindings();
            
            var configParser = Resolver.Kernel.Get<IConfigParser>();
            configParser.parse(args);

            var consoleExecuter = Resolver.Kernel.Get<IConsoleExecuter>();
            consoleExecuter.Execute(configParser.Config);
            
            var magickImgConverter = Resolver.Kernel.Get<IImgConverter>();
            magickImgConverter.SetConfig(configParser.Config);
            magickImgConverter.ProcessConversions();
        }
    }
}