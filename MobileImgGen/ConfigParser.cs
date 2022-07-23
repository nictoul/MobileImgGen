using System;
using System.Collections.Generic;
using System.IO;
using Mono.Options;

namespace MobileImgGen
{
    public interface IConfigParser
    {
        void parse(string[] args);
        Config Config { get; }
    }
    
    public class ConfigParser: IConfigParser
    {
        private bool _showHelp = false;
        private decimal _originalImageSize = 0;
        private string _outputPath = @"C:\temp\MobileImgGenOutput";
        private string _previewPath = @"C:\temp\MobileImgGen";
        private string _inputFile = null;


        private OptionSet Options;

        public ConfigParser()
        {
            Options = new OptionSet () {
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
        }

        public Config Config { get; private set; }
        
        public void parse(string[] args)
        {
            try {
                var extra = Options.Parse (args);

                StringWriter helpMessage = new StringWriter();
                Options.WriteOptionDescriptions(helpMessage);
                
                Config = new Config(_originalImageSize, _outputPath, _previewPath, _inputFile, extra,helpMessage.ToString(), _showHelp);
                Console.WriteLine(Config);
            }
            catch (OptionException e) {
                Console.WriteLine (e.Message);
                Console.WriteLine ("Try `MobileImgConverter --help' for more information.");
            }
            Console.WriteLine("bye World!");
        }
        
        
    }
}