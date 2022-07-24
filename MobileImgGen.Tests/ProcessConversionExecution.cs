using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace MobileImgGen.Tests
{
    public class ProcessConversionExecution
    {
        [Fact]
        public void ProcessConversionNotExecuteIfShowHelp()
        {
            var mockMagickImgConverter = new Mock<IImgConverter>();
            var consoleExecuter = new ConsoleExecuter(mockMagickImgConverter.Object);

            var showHelpAsked = true;
            var config = new Config(1, "", "", "dummyInputFile", new List<string>(),
                "", showHelpAsked);
            
            consoleExecuter.Execute(config);
            
            mockMagickImgConverter.Verify(x =>x.ProcessConversions(), Times.Never);
        }
        
        [Fact]
        public void ProcessConversionNotExecuteIfNoInputFile()
        {
            var mockMagickImgConverter = new Mock<IImgConverter>();
            var consoleExecuter = new ConsoleExecuter(mockMagickImgConverter.Object);

            string noInputFile = null;
            var config = new Config(1, "", "", noInputFile, new List<string>(),
                "", false);
            
            consoleExecuter.Execute(config);
            
            mockMagickImgConverter.Verify(x =>x.ProcessConversions(), Times.Never);
        }
        
        [Fact]
        public void ProcessConversionNotExecuteIfNoOriginalImgSize()
        {
            var mockMagickImgConverter = new Mock<IImgConverter>();
            var consoleExecuter = new ConsoleExecuter(mockMagickImgConverter.Object);

            var defaultOriginalImgSize = 0;
            var config = new Config(defaultOriginalImgSize, "", "", "dummyInputFile", new List<string>(),
                "", false);
            
            consoleExecuter.Execute(config);
            
            mockMagickImgConverter.Verify(x =>x.ProcessConversions(), Times.Never);
        }
        
        [Fact]
        public void ProcessConversionNotExecuteIfUnsupportedArgs()
        {
            var mockMagickImgConverter = new Mock<IImgConverter>();
            var consoleExecuter = new ConsoleExecuter(mockMagickImgConverter.Object);

            var unsupportedArgsList = new List<string>() { "--dummy" };
            var config = new Config(1, "", "", "dummyInputFile", unsupportedArgsList,
                "", false);
            
            consoleExecuter.Execute(config);
            
            mockMagickImgConverter.Verify(x =>x.ProcessConversions(), Times.Never);
        }
        
        [Fact]
        public void ProcessConversionExecuteIfAllMandatoryArgsOk()
        {
            var mockMagickImgConverter = new Mock<IImgConverter>();
            var consoleExecuter = new ConsoleExecuter(mockMagickImgConverter.Object);

            var config = new Config(1, "", "", "dummyInputFile", new List<string>(),
                "", false);
            
            consoleExecuter.Execute(config);
            
            mockMagickImgConverter.Verify(x =>x.ProcessConversions(), Times.Once);
        }
    }
}