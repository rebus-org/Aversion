﻿using System;
using System.IO;
using System.Text;
using GoCommando;
using Semver;

namespace Aversion.Commands
{
    [Command("patch")]
    [Description("Generates a .cs file with version attributes with an acceptable .NET version number")]
    public class PatchCommand : ICommand
    {
        [Parameter("ver")]
        [Description("SemVer 2.0-compatible version string")]
        [Example("3.0.0")]
        [Example("3.0.1")]
        [Example("3.1.0-alpha012")]
        public string VersionString { get; set; }

        [Parameter("in")]
        [Description("Path to input file that will be used as a template")]
        public string InputFile { get; set; }

        [Parameter("out")]
        [Description("Path to output file that will be used to store the resulting text")]
        public string OutputFile { get; set; }

        [Parameter("token")]
        [Description("Replacement token to substitute for a .NET-compatible version number")]
        [Example("$version$")]
        public string Token { get; set; }

        public void Run()
        {
            var version = GetVersion();
            var inputFileText = ReadInputFile();

            if (!inputFileText.Contains(Token))
            {
                Print($"WARNING: File contents read from '{InputFile}' did not contain replacement token '{Token}'");
            }

            var dotnetVersion = GetDotnetVersion(version);

            var outputFileText = inputFileText.Replace(Token, dotnetVersion.ToString());

            WriteOutputFile(outputFileText);
        }

        void WriteOutputFile(string outputFileText)
        {
            try
            {
                File.WriteAllText(OutputFile, outputFileText);
            }
            catch (Exception exception)
            {
                throw new GoCommandoException($@"Could not write the following contents to '{OutputFile}':

{outputFileText}

Got the following exception: {exception}");
            }
        }

        static void Print(string text)
        {
            Console.WriteLine(text);
        }

        string ReadInputFile()
        {
            try
            {
                return File.ReadAllText(InputFile, Encoding.UTF8);
            }
            catch (FileNotFoundException)
            {
                throw new GoCommandoException($"Could not find file '{InputFile}'");
            }
        }

        static Version GetDotnetVersion(SemVersion version)
        {
            int revision;

            if (!int.TryParse(version.Build, out revision))
            {
                revision = 0;
            }

            return new Version(version.Major, version.Minor, version.Patch, revision);
        }

        SemVersion GetVersion()
        {
            try
            {
                return SemVersion.Parse(VersionString);
            }
            catch (Exception exception)
            {
                throw new GoCommandoException($"Could not parse version string '{VersionString}': {exception.Message}");
            }
        }
    }
}