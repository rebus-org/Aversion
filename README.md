# Aversion

An `AssemblyInfo.cs` patcher that creates a .NET-compatible version out of a [SemVer](http://semver.org/).

Go to the command line:

    > aversion.exe -help patch

    Generates a .cs file with version attributes with an acceptable .NET version number

    Type

        Aversion.exe patch <args>

    where <args> can consist of the following parameters:

        -ver
            SemVer 2.0-compatible version string

            Examples:
              -ver 3.0.0
              -ver 3.0.1
              -ver 3.1.0-alpha012

        -in
            Path to input file that will be used as a template

        -out
            Path to output file that will be used to store the resulting text

        -token
            Replacement token to substitute for a .NET-compatible version number

            Examples:
              -token $version$


OK, so we call the tool with the `patch` command, specifying

* `-ver` - a [semantic version string](http://semver.org/) with the version to use - e.g. "2.0.4-beta1"
* `-in` - path to a template file to use - e.g. `AssemblyInfo_Template.cs`
* `-out` - path to the file to generate - e.g. `AssemblyInfo.cs`
* `-token` - replacement token, which will be replaced with the .NET-compatible version