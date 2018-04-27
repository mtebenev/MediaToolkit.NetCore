using System;
using System.IO;
using MediaToolkit;
using MediaToolkit.Model;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Note: assuming that launching from VS debug and current directory is MediaToolkit\MediaToolkit src\SampleApp\bin\Debug\netcoreapp2.0
            var videoPath = Path.GetFullPath(@"..\..\..\..\MediaToolkit.Test\TestVideo\BigBunny.m4v");
            var inputFile = new MediaFile {Filename = videoPath};

            using(var engine = new Engine(@"C:\ffmpeg\FFmpeg.exe"))
            {
                engine.GetMetadata(inputFile);
            }

            Console.WriteLine(inputFile.Metadata.Duration);
        }
    }
}
