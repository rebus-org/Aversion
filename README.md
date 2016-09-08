# Aversion

An `AssemblyInfo.cs` patcher that creates a .NET-compatible version out of a [SemVer](http://semver.org/).

# Help

Go to the command line:

    > aversion -help patch

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

And that is how it is done.

# Example

Let's say that we have an `AssemblyInfo.cs` residing in our project. You probably have that.

Now we have change the "Build action" of this file from "Compile" (the default for .cs files) to "None", which means that the template is not compiled into our DLL.

Then we change the two assembly-level attributes from the default

    [assembly: AssemblyVersion("1.0.0.0")]
    [assembly: AssemblyFileVersion("1.0.0.0")]

to

    [assembly: AssemblyVersion("$version$")]
    [assembly: AssemblyFileVersion("$version$")]

The `$version$` is where we would like a .NET-compatible version string to be injected.

The you go to the command line and do something like this:

    > aversion patch -token "$version$" -in Properties\AssemblyInfo.cs -out Properties\AssemblyInfo_Patch.cs -ver 1.4.5-alpha5

which will replace `$token$` with `1.4.5.0` and store the output in `AssemblyInfo_Patch.cs`. Last thing to do is to include `AssemblyInfo_Patch.cs` into your project,
so it gets compiled into the DLL instead of the original `AssemblyInfo.cs`.

