using System;
using System.Collections.Generic;

namespace MobileImgGen
{
    public class Config
    {
        public Config(decimal originalImageSize, string outputPath, string previewPath, string inputFile,
            List<string> unsupportedArgs, string helpMessage, bool showHelp)
        {
            OriginalImageSize = originalImageSize;
            OutputPath = outputPath;
            PreviewPath = previewPath;
            InputFile = inputFile;
            UnsupportedArgs = unsupportedArgs;
            HelpMessage = helpMessage;
            ShowHelp = showHelp;
            Created = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
        }

        public decimal OriginalImageSize { get; }
        public string OutputPath { get; }
        public string PreviewPath { get; }
        public string InputFile { get; }
        public List<string> UnsupportedArgs { get; }
        public string HelpMessage { get; }
        public bool ShowHelp { get; }
        public string Created { get; }

        public override string ToString()
        {
            return "Configuration: \n" +
                   $"OriginalImageSize: {OriginalImageSize}\n" +
                   $"OutputPath: {OutputPath}\n" +
                   $"PreviewPath: {PreviewPath}\n" +
                   $"InputFile: {InputFile}\n";
        }
    }
}