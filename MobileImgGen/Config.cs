using System;

namespace MobileImgGen
{
    public class Config
    {
        public Config(decimal originalImageSize, string outputPath, string previewPath, string inputFile)
        {
            OriginalImageSize = originalImageSize;
            OutputPath = outputPath;
            PreviewPath = previewPath;
            InputFile = inputFile;
            Created = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
        }

        public decimal OriginalImageSize { get; }
        public string OutputPath { get; }
        public string PreviewPath { get; }
        public string InputFile { get; }
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