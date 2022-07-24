using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace MobileImgGen.Tests
{
    public class ArgsParsing
    {
        [Fact]
        public void ShowHelpIsSetIfInArgs()
        {
            var args = new[] { "-h" };
            var configParser = new ConfigParser();
            configParser.parse(args);
            
            Assert.True(configParser.Config.ShowHelp);
        }
        
        [Fact]
        public void InputFileIsSetIfInArgs()
        {
            var args = new[] { "-i", "toto" };
            var configParser = new ConfigParser();
            configParser.parse(args);
            
            Assert.Equal("toto",configParser.Config.InputFile);
        }
        
        [Fact]
        public void OutputPathIsSetIfInArgs()
        {
            var args = new[] { "-o", "toto" };
            var configParser = new ConfigParser();
            configParser.parse(args);
            
            Assert.Equal("toto",configParser.Config.OutputPath);
        }
        
        [Fact]
        public void PreviewPathIsSetIfInArgs()
        {
            var args = new[] { "-p", "toto" };
            var configParser = new ConfigParser();
            configParser.parse(args);
            
            Assert.Equal("toto",configParser.Config.PreviewPath);
        }
        
        [Fact]
        public void UnsupportedArgIsSetIfInArgs()
        {
            var args = new[] { "-z"};
            var configParser = new ConfigParser();
            configParser.parse(args);
            
            Assert.NotEmpty(configParser.Config.UnsupportedArgs);
        }
        
        [Fact]
        public void OriginalImgSizeIsSetIfInArgs()
        {
            var args = new[] { "-s", "3.5" };
            var configParser = new ConfigParser();
            configParser.parse(args);
            
            Assert.Equal(3.5m,configParser.Config.OriginalImageSize);
        }
        
        [Fact]
        public void HelpMessageIsNotNull()
        {
            var args = new[] { ""};
            var configParser = new ConfigParser();
            configParser.parse(args);
            
            Assert.NotNull(configParser.Config.HelpMessage);
            Assert.NotEmpty(configParser.Config.HelpMessage);
        }
        
    }
}