using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MobileImgGen
{
    public interface IImgConverter
    {
        void ProcessConversions();
        void SetConfig(Config config);
    }
    
    
    public class MagickImgConverter: IImgConverter
    {
        private Config _config;
        private readonly Dictionary<string, decimal> _imageSize;


        public MagickImgConverter()
        {
            
            _imageSize = new Dictionary<string, decimal>()
            {
                {"LDPI", 0.75m},
                {"MDPI", 1m},
                {"HDPI", 1.5m},
                {"XHDPI", 2.0m},
                {"XXHDPI", 3.0m},
                {"XXXHDPI", 4.0m},
            };
        }

        public void ProcessConversions()
        {
            var commands = new List<string>();
            foreach (var imgSize in _imageSize)
            {
                if (_config.OriginalImageSize == imgSize.Value)
                {
                    continue;
                }
                var percentage = GetConversionPercentage(_config.OriginalImageSize, imgSize.Value);
                //TODO: create Preview folder if not exist
                var outputFile = _config.PreviewPath + "\\" + GetInputFileName() + imgSize.Key + "." + GetInputFileExtension();
                var magickCommand = $"magick {_config.InputFile} -resize {percentage}% {outputFile}";
                commands.Add(magickCommand);
            }
           //TODO copy images in the output folder as android project are organised (in subfolder LDPI, MDPI ...)
            ProcessCommand(commands);
        }

        public void SetConfig(Config config)
        {
            _config = config;
        }

        private void ProcessCommand(List<string> commands)
        {
            var cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            foreach (var command in commands)
            {
                cmd.StandardInput.WriteLine(command);    
            }
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        private decimal GetConversionPercentage(decimal originalSize, decimal newSize)
        {
            var percentage = newSize / originalSize * 100;
            return decimal.Round(percentage, 2);
        }

        private string GetInputFileName()
        {
            var fileWithExtension = _config.InputFile.Split('\\').Last();
            var fileName = fileWithExtension.Split('.').First();
            return fileName;
        }

        private string GetInputFileExtension()
        {
            var fileWithExtension = _config.InputFile.Split('\\').Last();
            var fileExtension = fileWithExtension.Split('.').Last();
            return fileExtension;
        }
    }
}