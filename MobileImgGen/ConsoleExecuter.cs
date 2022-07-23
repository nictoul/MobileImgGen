using System;
using System.Collections.Generic;

namespace MobileImgGen
{
    public interface IConsoleExecuter
    {
        void Execute(Config config);
    }
    
    public class ConsoleExecuter: IConsoleExecuter
    {
        private readonly IImgConverter _imgConverter;
        private Config _config;

        public ConsoleExecuter(IImgConverter imgConverter)
        {
            _imgConverter = imgConverter;
        }

        public void Execute(Config config)
        {
            _config = config;
            if (_config.UnsupportedArgs.Count != 0)
            {
                PrintUnknownArgs(config.UnsupportedArgs);
                Exit();
                return;
            }
            if (_config.ShowHelp)
            {
                PrintHelp();
                Exit();
                return;
            }

            if (_config.OriginalImageSize == 0)
            {
                Console.WriteLine("Original image size factor must be specified (-s 1.5) see help for all size supported");
                Exit();
                return;
            }

            if (_config.InputFile == null)
            {
                Console.WriteLine("Input image file to convert must be specified with -i");
                Exit();
                return;
            }
            
            _imgConverter.SetConfig(_config);
            _imgConverter.ProcessConversions();
        }
        
        private void PrintHelp()
        {
            Console.WriteLine ("Options:");
            Console.WriteLine(_config.HelpMessage);
        }

        private void PrintUnknownArgs(List<string> unknownArgs)
        {
            foreach (var unknownArg in unknownArgs)
            {
                Console.WriteLine($"[{unknownArg}] is not support");
            }
        }

        private void Exit()
        {
            Console.WriteLine("Exit!");
        }
    }
}