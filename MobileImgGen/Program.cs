using System;
using System.Collections.Generic;
using Mono.Options;

namespace MobileImgGen
{
    class Program
    {
        private static bool _showHelp = false;
        private static decimal _originalImageSize = 0;
        private static string _outputPath = @"C:\temp\MobileImgGenOutput";
        private static string _previewPath = @"C:\temp\MobileImgGen";
        private static string _inputFile = null;
        
        
        private static readonly OptionSet Options = new OptionSet () {
            { "s|size=", "Original image size (ex: 1.5) \n"
                + "\tLDPI:\t 36x36\t 0.75\n"
                + "\tMDPI:\t 48x48\t 1.0\n"
                + "\tHDPI:\t 72x72\t 1.5\n"
                + "\tXHDPI:\t 96x96\t 2.0\n"
                + "\tXXHDPI:\t 144x144 3.0\n"
                + "\tXXXHDPI: 192x192 4.0\n",
                (decimal v) => _originalImageSize = v },
            { "p|previewPath=",  $"Path of all images converted for quick quality check (default: '{_previewPath}')", 
                arg => _previewPath = arg},
            { "o|outputPath=",  $"Output directory containing all image directories like hdpi, xhdpi... (default: '{_outputPath}')", 
                arg => _outputPath = arg },
            { "i|inputFile=",  "Input image file to convert", 
                arg => _inputFile = arg },
            { "h|help=",  "Show this message and exit", 
                arg => _showHelp = arg != null },
        };
        static void Main(string[] args)
        {
            try {
                var extra = Options.Parse (args);

                
                if (extra.Count != 0)
                {
                    PrintUnknownArgs(extra);
                    Exit();
                    return;
                }
                if (_showHelp)
                {
                    PrintHelp();
                    Exit();
                    return;
                }

                if (_originalImageSize == 0)
                {
                    Console.WriteLine("Original image size factor must be specified (-s 1.5) see help for all size supported");
                    Exit();
                    return;
                }

                if (_inputFile == null)
                {
                    Console.WriteLine("Input image file to convert must be specified with -i");
                    Exit();
                    return;
                }
                
                var options = new Config(_originalImageSize, _outputPath, _previewPath, _inputFile);
                Console.WriteLine(options);
                var magickImgConverter = new MagickImgConverter(options);
                magickImgConverter.ProcessConversions();
            }
            catch (OptionException e) {
                Console.WriteLine (e.Message);
                Console.WriteLine ("Try `MobileImgConverter --help' for more information.");
            }
            Console.WriteLine("bye World!");
        }
        
        private static void PrintHelp()
        {
            Console.WriteLine ("Options:");
            Options.WriteOptionDescriptions (Console.Out); 
        }

        private static void PrintUnknownArgs(List<string> unknownArgs)
        {
            foreach (var unknownArg in unknownArgs)
            {
                Console.WriteLine($"[{unknownArg}] is not support");
            }
        }

        private static void Exit()
        {
            Console.WriteLine("Exit!");
        }
    }
}