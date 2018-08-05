using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;

namespace SampleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Note: assuming that launching from VS debug and current directory is MediaToolkit\MediaToolkit src\SampleApp\bin\Debug\netcoreapp2.0
            var videoPath = Path.GetFullPath(@"..\..\..\..\MediaToolkit.Test\TestVideo\BigBunny.m4v");
            FfprobeType metadata;

            using(var engine = new Engine(@"C:\ffmpeg\FFmpeg.exe", new FileSystem()))
            {
                metadata = await engine.GetMetadataAsync(videoPath);
            }

            Console.WriteLine(metadata.Format.Duration);
        }
    }
}
