using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Cake.Intellisense")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Cake.Intellisense")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

namespace Cake.Common
{
    public static class ArgumentAliasesMetadata
    {
        ///             <summary>
        ///             Gets an argument and throws if the argument is missing.
        ///             </summary>
        ///             <typeparam name="T">The argument type.</typeparam>
        ///             
        ///             <param name="name">The argument name.</param>
        ///             <returns>The value of the argument.</returns>
        ///             <example>
        ///             <code>
        ///             //Cake.exe .\argument.cake -myArgument="is valid" -loopCount = 5
        ///             Information("Argument {0}", Argument&lt;string&gt;("myArgument"));
        ///             var loopCount = Argument&lt;int&gt;("loopCount");
        ///             for(var index = 0;index&lt;loopCount; index++)
        ///             {
        ///                 Information("Index {0}", index);
        ///             }
        ///             </code>
        ///             </example>
        ///             <exception cref="T:Cake.Core.CakeException">Argument value is null.</exception>
        ///             <exception cref="T:System.ArgumentNullException"><paramref name="context" /> is null.</exception>
        ///         
        public static T Argument<T>(System.String name)
        {
            return default(T);
        }

        ///             <summary>
        ///             Gets an argument and returns the provided <paramref name="defaultValue" /> if the argument is missing.
        ///             </summary>
        ///             <typeparam name="T">The argument type.</typeparam>
        ///             
        ///             <param name="name">The argument name.</param>
        ///             <param name="defaultValue">The value to return if the argument is missing.</param>
        ///             <returns>The value of the argument if it exist; otherwise <paramref name="defaultValue" />.</returns>
        ///             <example>
        ///             <code>
        ///             //Cake.exe .\argument.cake -myArgument="is valid" -loopCount = 5
        ///             Information("Argument {0}", Argument&lt;string&gt;("myArgument", "is NOT valid"));
        ///             var loopCount = Argument&lt;int&gt;("loopCount", 10);
        ///             for(var index = 0;index&lt;loopCount; index++)
        ///             {
        ///                 Information("Index {0}", index);
        ///             }
        ///             </code>
        ///             </example>
        ///         
        public static T Argument<T>(System.String name, T defaultValue)
        {
            return default(T);
        }

        ///             <summary>
        ///             Determines whether or not the specified argument exist.
        ///             </summary>
        ///             
        ///             <param name="name">The argument name.</param>
        ///             <returns>Whether or not the specified argument exist.</returns>
        ///             <example>
        ///             This sample shows how to call the <see cref="M:Cake.Common.ArgumentAliases.HasArgument(Cake.Core.ICakeContext,System.String)" /> method.
        ///             <code>
        ///             var argumentName = "myArgument";
        ///             //Cake.exe .\hasargument.cake -myArgument="is specified"
        ///             if (HasArgument(argumentName))
        ///             {
        ///                 Information("{0} is specified", argumentName);
        ///             }
        ///             //Cake.exe .\hasargument.cake
        ///             else
        ///             {
        ///                 Warning("{0} not specified", argumentName);
        ///             }
        ///             </code>
        ///             </example>
        ///         
        public static System.Boolean HasArgument(System.String name)
        {
            return default(System.Boolean);
        }
    }

    public static class EnvironmentAliasesMetadata
    {
        ///             <summary>
        ///             Retrieves the value of the environment variable or <c>null</c> if the environment variable does not exist.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             Information(EnvironmentVariable("HOME") ?? "Unknown location");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="variable">The environment variable.</param>
        ///             <returns>The environment variable or <c>null</c> if the environment variable does not exist.</returns>
        ///         
        public static System.String EnvironmentVariable(System.String variable)
        {
            return default(System.String);
        }

        ///              <summary>
        ///              Retrieves all environment variables
        ///              </summary>
        ///              <example>
        ///              <code>
        ///              var envVars = EnvironmentVariables();
        ///             
        ///              string path;
        ///              if (envVars.TryGetValue("PATH", out path))
        ///              {
        ///                  Information("Path: {0}", path);
        ///              }
        ///             
        ///              foreach(var envVar in envVars)
        ///              {
        ///                  Information(
        ///                      "Key: {0}\tValue: \"{1}\"",
        ///                      envVar.Key,
        ///                      envVar.Value
        ///                      );
        ///              }
        ///              </code>
        ///              </example>
        ///              
        ///              <returns>The environment variables</returns>
        ///         
        public static global::System.Collections.Generic.IDictionary<System.String, System.String> EnvironmentVariables()
        {
            return default(global::System.Collections.Generic.IDictionary<System.String, System.String>);
        }

        ///             <summary>
        ///             Checks for the existence of a value for a given environment variable.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             if (HasEnvironmentVariable("SOME_ENVIRONMENT_VARIABLE"))
        ///             {
        ///                 Information("The environment variable was present.");
        ///             }
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="variable">The environment variable.</param>
        ///             <returns>
        ///               <c>true</c> if the environment variable exist; otherwise <c>false</c>.
        ///             </returns>
        ///         
        public static System.Boolean HasEnvironmentVariable(System.String variable)
        {
            return default(System.Boolean);
        }

        ///             <summary>
        ///             Determines whether the build script running on a Unix or Linux based system.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             if (IsRunningOnUnix())
        ///             {
        ///                 Information("Not Windows!");
        ///             }
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>
        ///               <c>true</c> if the build script running on a Unix or Linux based system; otherwise <c>false</c>.
        ///             </returns>
        ///         
        public static System.Boolean IsRunningOnUnix()
        {
            return default(System.Boolean);
        }

        ///             <summary>
        ///             Determines whether the build script is running on Windows.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             if (IsRunningOnWindows())
        ///             {
        ///                 Information("Windows!");
        ///             }
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>
        ///               <c>true</c> if the build script is running on Windows; otherwise <c>false</c>.
        ///             </returns>
        ///         
        public static System.Boolean IsRunningOnWindows()
        {
            return default(System.Boolean);
        }
    }

    public static class ProcessAliasesMetadata
    {
        ///             <summary>
        ///             Starts the process resource that is specified by the filename.
        ///             </summary>
        ///             
        ///             <param name="fileName">Name of the file.</param>
        ///             <returns>The newly started process.</returns>
        ///             <example>
        ///             <code>
        ///             using(var process = StartAndReturnProcess("ping"))
        ///             {
        ///                 process.WaitForExit();
        ///                 // This should output 0 as valid arguments supplied
        ///                 Information("Exit code: {0}", process.GetExitCode());
        ///             }
        ///             </code>
        ///             </example>
        ///             <exception cref="T:System.ArgumentNullException"><paramref name="context" />, <paramref name="fileName" /> is null.</exception>
        ///         
        public static global::Cake.Core.IO.IProcess StartAndReturnProcess(global::Cake.Core.IO.FilePath fileName)
        {
            return default(global::Cake.Core.IO.IProcess);
        }

        ///             <summary>
        ///             Starts the process resource that is specified by the filename and settings.
        ///             </summary>
        ///             
        ///             <param name="fileName">Name of the file.</param>
        ///             <param name="settings">The settings.</param>
        ///             <returns>The newly started process.</returns>
        ///             <example>
        ///             <code>
        ///             using(var process = StartAndReturnProcess("ping", new ProcessSettings{ Arguments = "localhost" }))
        ///             {
        ///                 process.WaitForExit();
        ///                 // This should output 0 as valid arguments supplied
        ///                 Information("Exit code: {0}", process.GetExitCode());
        ///             }
        ///             </code>
        ///             </example>
        ///             <exception cref="T:System.ArgumentNullException"><paramref name="context" />, <paramref name="fileName" />, or <paramref name="settings" />  is null.</exception>
        ///         
        public static global::Cake.Core.IO.IProcess StartAndReturnProcess(global::Cake.Core.IO.FilePath fileName, global::Cake.Core.IO.ProcessSettings settings)
        {
            return default(global::Cake.Core.IO.IProcess);
        }

        ///             <summary>
        ///             Starts the process resource that is specified by the filename.
        ///             </summary>
        ///             
        ///             <param name="fileName">The file name.</param>
        ///             <returns>The exit code that the started process specified when it terminated.</returns>
        ///             <example>
        ///             <code>
        ///             var exitCodeWithoutArguments = StartProcess("ping");
        ///             // This should output 1 as argument is missing
        ///             Information("Exit code: {0}", exitCodeWithoutArguments);
        ///             </code>
        ///             </example>
        ///         
        public static System.Int32 StartProcess(global::Cake.Core.IO.FilePath fileName)
        {
            return default(System.Int32);
        }

        ///             <summary>
        ///             Starts the process resource that is specified by the filename and settings.
        ///             </summary>
        ///             
        ///             <param name="fileName">Name of the file.</param>
        ///             <param name="settings">The settings.</param>
        ///             <returns>The exit code that the started process specified when it terminated.</returns>
        ///             <example>
        ///             <code>
        ///             var exitCodeWithArgument = StartProcess("ping", new ProcessSettings{ Arguments = "localhost" });
        ///             // This should output 0 as valid arguments supplied
        ///             Information("Exit code: {0}", exitCodeWithArgument);
        ///             </code>
        ///             </example>
        ///         
        public static System.Int32 StartProcess(global::Cake.Core.IO.FilePath fileName, global::Cake.Core.IO.ProcessSettings settings)
        {
            return default(System.Int32);
        }

        ///             <summary>
        ///             Starts the process resource that is specified by the filename and arguments
        ///             </summary>
        ///             
        ///             <param name="fileName">Name of the file.</param>
        ///             <param name="processArguments">The arguments used in the process settings.</param>
        ///             <returns>The exit code that the started process specified when it terminated.</returns>
        ///             <example>
        ///             <code>
        ///             var exitCodeWithArgument = StartProcess("ping", "localhost");
        ///             // This should output 0 as valid arguments supplied
        ///             Information("Exit code: {0}", exitCodeWithArgument);
        ///             </code>
        ///             </example>
        ///         
        public static System.Int32 StartProcess(global::Cake.Core.IO.FilePath fileName, System.String processArguments)
        {
            return default(System.Int32);
        }

        ///              <summary>
        ///              Starts the process resource that is specified by the filename and settings.
        ///              </summary>
        ///              
        ///              <param name="fileName">Name of the file.</param>
        ///              <param name="settings">The settings.</param>
        ///              <param name="redirectedOutput">outputs process output <see cref="P:Cake.Core.IO.ProcessSettings.RedirectStandardOutput">RedirectStandardOutput</see> is true</param>
        ///              <returns>The exit code that the started process specified when it terminated.</returns>
        ///              <example>
        ///              <code>
        ///              IEnumerable&lt;string&gt; redirectedOutput;
        ///              var exitCodeWithArgument = StartProcess("ping", new ProcessSettings{
        ///              Arguments = "localhost",
        ///              RedirectStandardOutput = true
        ///              },
        ///              out redirectedOutput
        ///              );
        ///              //Output last line of process output
        ///              Information("Last line of output: {0}", redirectedOutput.LastOrDefault());
        ///             
        ///              // This should output 0 as valid arguments supplied
        ///              Information("Exit code: {0}", exitCodeWithArgument);
        ///              </code>
        ///              </example>
        ///         
        public static System.Int32 StartProcess(global::Cake.Core.IO.FilePath fileName, global::Cake.Core.IO.ProcessSettings settings, out global::System.Collections.Generic.IEnumerable<System.String> redirectedOutput)
        {
            redirectedOutput = default(global::System.Collections.Generic.IEnumerable<System.String>);
            return default(System.Int32);
        }
    }

    public static class ReleaseNotesAliasesMetadata
    {
        ///             <summary>
        ///             Parses all release notes.
        ///             </summary>
        ///             
        ///             <param name="filePath">The file path.</param>
        ///             <returns>All release notes.</returns>
        ///             <example>
        ///             <code>
        ///             var releaseNotes = ParseAllReleaseNotes("./ReleaseNotes.md");
        ///             foreach(var releaseNote in releaseNotes)
        ///             {
        ///                 Information("Version: {0}", releaseNote.Version);
        ///                 foreach(var note in releaseNote.Notes)
        ///                 {
        ///                     Information("\t{0}", note);
        ///                 }
        ///             }
        ///             </code>
        ///             </example>
        ///         
        public static global::System.Collections.Generic.IReadOnlyList<global::Cake.Common.ReleaseNotes> ParseAllReleaseNotes(global::Cake.Core.IO.FilePath filePath)
        {
            return default(global::System.Collections.Generic.IReadOnlyList<global::Cake.Common.ReleaseNotes>);
        }

        ///             <summary>
        ///             Parses the latest release notes.
        ///             </summary>
        ///             
        ///             <param name="filePath">The file path.</param>
        ///             <returns>The latest release notes.</returns>
        ///             <example>
        ///             <code>
        ///             var releaseNote = ParseReleaseNotes("./ReleaseNotes.md");
        ///             Information("Version: {0}", releaseNote.Version);
        ///             foreach(var note in releaseNote.Notes)
        ///             {
        ///                 Information("\t{0}", note);
        ///             }
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.ReleaseNotes ParseReleaseNotes(global::Cake.Core.IO.FilePath filePath)
        {
            return default(global::Cake.Common.ReleaseNotes);
        }
    }
}

namespace Cake.Common.Build
{
    public static class BuildSystemAliasesMetadata
    {
        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.AppVeyor.AppVeyorProvider" /> instance that can
        ///             be used to manipulate the AppVeyor environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isAppVeyorBuild = AppVeyor.IsRunningOnAppVeyor;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.AppVeyor" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.AppVeyor.IAppVeyorProvider AppVeyor
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.Bamboo.BambooProvider" /> instance that can
        ///             be used to manipulate the Bamboo environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isBambooBuild = Bamboo.IsRunningOnBamboo;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.Bamboo" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.BitbucketPipelines.BitbucketPipelinesProvider" /> instance that can be used to
        ///             obtain information from the Bitbucket Pipelines environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isBitbucketPipelinesBuild = BitbucketPipelines.IsRunningOnBitbucketPipelines;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.BitbucketPipelines" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.BitbucketPipelines.IBitbucketPipelinesProvider BitbucketPipelines
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.Bitrise.BitriseProvider" /> instance that can be used to
        ///             obtain information from the Bitrise environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isBitriseBuild = Bitrise.IsRunningOnBitrise;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.Bitrise" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.Bitrise.IBitriseProvider Bitrise
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.BuildSystem" /> instance that can
        ///             be used to query for information about the current build system.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isLocal = BuildSystem.IsLocalBuild;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="T:Cake.Common.Build.BuildSystem" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.BuildSystem BuildSystem
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.ContinuaCI.ContinuaCIProvider" /> instance that can
        ///             be used to manipulate the Continua CI environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isContinuaCIBuild = ContinuaCI.IsRunningContinuaCI;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.ContinuaCI" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.ContinuaCI.IContinuaCIProvider ContinuaCI
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.GitLabCI.GitLabCIProvider" /> instance that can be used to
        ///             obtain information from the GitLab CI environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isGitLabCIBuild = GitLabCI.IsRunningOnGitLabCI;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.GitLabCI" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.GitLabCI.IGitLabCIProvider GitLabCI
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.GoCD.GoCDProvider" /> instance that can be used to
        ///             obtain information from the Go.CD environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isGoCDBuild = GoCD.IsRunningOnGoCD;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.GoCD" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.GoCD.IGoCDProvider GoCD
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.Jenkins.JenkinsProvider" /> instance that can be used to
        ///             obtain information from the Jenkins environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isJenkinsBuild = Jenkins.IsRunningOnJenkins;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.Jenkins" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.Jenkins.IJenkinsProvider Jenkins
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.MyGet.MyGetProvider" /> instance that can
        ///             be used to manipulate the MyGet environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isMyGetBuild = MyGet.IsRunningOnMyGet;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.MyGet" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.MyGet.IMyGetProvider MyGet
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.TeamCity.TeamCityProvider" /> instance that can
        ///             be used to manipulate the TeamCity environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isTeamCityBuild = TeamCity.IsRunningOnTeamCity;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.TeamCity" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.TeamCity.ITeamCityProvider TeamCity
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.TFBuild.TFBuildProvider" /> instance that can be used to
        ///             obtain information from the Team Foundation Build environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isTFSBuild = TFBuild.IsRunningOnTFS;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.TFBuild" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.TFBuild.ITFBuildProvider TFBuild
        {
            get;
        }

        ///             <summary>
        ///             Gets a <see cref="T:Cake.Common.Build.TravisCI.TravisCIProvider" /> instance that can be used to
        ///             obtain information from the Travis CI environment.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var isTravisCIBuild = TravisCI.IsRunningOnTravisCI;
        ///             </code>
        ///             </example>
        ///             
        ///             <returns>A <see cref="N:Cake.Common.Build.TravisCI" /> instance.</returns>
        ///         
        public static global::Cake.Common.Build.TravisCI.ITravisCIProvider TravisCI
        {
            get;
        }
    }
}

namespace Cake.Common.Diagnostics
{
    public static class LoggingAliasesMetadata
    {
        ///             <summary>
        ///             Writes a debug message to the log using the specified value.
        ///             </summary>
        ///             
        ///             <param name="value">The value.</param>
        ///             <example>
        ///             <code>
        ///             Debug(new {FirstName = "John", LastName="Doe"});
        ///             </code>
        ///             </example>
        ///         
        public static void Debug(System.Object value)
        {
        }

        ///             <summary>
        ///             Writes a debug message to the log using the specified string value.
        ///             </summary>
        ///             
        ///             <param name="value">The value.</param>
        ///             <example>
        ///             <code>
        ///             Debug("{string}");
        ///             </code>
        ///             </example>
        ///         
        public static void Debug(System.String value)
        {
        }

        ///             <summary>
        ///             Writes a debug message to the log using the specified log message action.
        ///             Evaluation message only if verbosity same or more verbose.
        ///             </summary>
        ///             
        ///             <param name="logAction">The function called for message when logging.</param>
        ///             <example>
        ///             <code>
        ///             Debug(logAction=&gt;logAction("Hello {0}! Today is an {1:dddd}", "World", DateTime.Now));
        ///             </code>
        ///             </example>
        ///         
        public static void Debug(global::Cake.Core.Diagnostics.LogAction logAction)
        {
        }

        ///             <summary>
        ///             Writes a debug message to the log using the specified format information.
        ///             </summary>
        ///             
        ///             <param name="format">The format.</param>
        ///             <param name="args">The arguments.</param>
        ///             <example>
        ///             <code>
        ///             Debug("Hello {0}! Today is an {1:dddd}", "World", DateTime.Now);
        ///             </code>
        ///             </example>
        ///         
        public static void Debug(System.String format, params System.Object[] args)
        {
        }

        ///             <summary>
        ///             Writes an error message to the log using the specified value.
        ///             </summary>
        ///             
        ///             <param name="value">The value.</param>
        ///             <example>
        ///             <code>
        ///             Error(new {FirstName = "John", LastName="Doe"});
        ///             </code>
        ///             </example>
        ///         
        public static void Error(System.Object value)
        {
        }

        ///             <summary>
        ///             Writes an error message to the log using the specified string value.
        ///             </summary>
        ///             
        ///             <param name="value">The value.</param>
        ///             <example>
        ///             <code>
        ///             Error("{string}");
        ///             </code>
        ///             </example>
        ///         
        public static void Error(System.String value)
        {
        }

        ///             <summary>
        ///             Writes an error message to the log using the specified log message action.
        ///             Evaluation message only if verbosity same or more verbose.
        ///             </summary>
        ///             
        ///             <param name="logAction">The function called for message when logging.</param>
        ///             <example>
        ///             <code>
        ///             Error(logAction=&gt;logAction("Hello {0}! Today is an {1:dddd}", "World", DateTime.Now));
        ///             </code>
        ///             </example>
        ///         
        public static void Error(global::Cake.Core.Diagnostics.LogAction logAction)
        {
        }

        ///             <summary>
        ///             Writes an error message to the log using the specified format information.
        ///             </summary>
        ///             
        ///             <param name="format">The format.</param>
        ///             <param name="args">The arguments.</param>
        ///             <example>
        ///             <code>
        ///             Error("Hello {0}! Today is an {1:dddd}", "World", DateTime.Now);
        ///             </code>
        ///             </example>
        ///         
        public static void Error(System.String format, params System.Object[] args)
        {
        }

        ///             <summary>
        ///             Writes an informational message to the log using the specified value.
        ///             </summary>
        ///             
        ///             <param name="value">The value.</param>
        ///             <example>
        ///             <code>
        ///             Information(new {FirstName = "John", LastName="Doe"});
        ///             </code>
        ///             </example>
        ///         
        public static void Information(System.Object value)
        {
        }

        ///             <summary>
        ///             Writes an informational message to the log using the specified string value.
        ///             </summary>
        ///             
        ///             <param name="value">The value.</param>
        ///             <example>
        ///             <code>
        ///             Information("{string}");
        ///             </code>
        ///             </example>
        ///         
        public static void Information(System.String value)
        {
        }

        ///             <summary>
        ///             Writes an informational message to the log using the specified log message action.
        ///             Evaluation message only if verbosity same or more verbose.
        ///             </summary>
        ///             
        ///             <param name="logAction">The function called for message when logging.</param>
        ///             <example>
        ///             <code>
        ///             Information(logAction=&gt;logAction("Hello {0}! Today is an {1:dddd}", "World", DateTime.Now));
        ///             </code>
        ///             </example>
        ///         
        public static void Information(global::Cake.Core.Diagnostics.LogAction logAction)
        {
        }

        ///             <summary>
        ///             Writes an informational message to the log using the specified format information.
        ///             </summary>
        ///             
        ///             <param name="format">The format.</param>
        ///             <param name="args">The arguments.</param>
        ///             <example>
        ///             <code>
        ///             Information("Hello {0}! Today is an {1:dddd}", "World", DateTime.Now);
        ///             </code>
        ///             </example>
        ///         
        public static void Information(System.String format, params System.Object[] args)
        {
        }

        ///             <summary>
        ///             Writes a verbose message to the log using the specified value.
        ///             </summary>
        ///             
        ///             <param name="value">The value.</param>
        ///             <example>
        ///             <code>
        ///             Verbose(new {FirstName = "John", LastName="Doe"});
        ///             </code>
        ///             </example>
        ///         
        public static void Verbose(System.Object value)
        {
        }

        ///             <summary>
        ///             Writes a verbose message to the log using the specified string value.
        ///             </summary>
        ///             
        ///             <param name="value">The value.</param>
        ///             <example>
        ///             <code>
        ///             Verbose("{string}");
        ///             </code>
        ///             </example>
        ///         
        public static void Verbose(System.String value)
        {
        }

        ///             <summary>
        ///             Writes a verbose message to the log using the specified log message action.
        ///             Evaluation message only if verbosity same or more verbose.
        ///             </summary>
        ///             
        ///             <param name="logAction">The function called for message when logging.</param>
        ///             <example>
        ///             <code>
        ///             Verbose(logAction=&gt;logAction("Hello {0}! Today is an {1:dddd}", "World", DateTime.Now));
        ///             </code>
        ///             </example>
        ///         
        public static void Verbose(global::Cake.Core.Diagnostics.LogAction logAction)
        {
        }

        ///             <summary>
        ///             Writes a verbose message to the log using the specified format information.
        ///             </summary>
        ///             
        ///             <param name="format">The format.</param>
        ///             <param name="args">The arguments.</param>
        ///             <example>
        ///             <code>
        ///             Verbose("Hello {0}! Today is an {1:dddd}", "World", DateTime.Now);
        ///             </code>
        ///             </example>
        ///         
        public static void Verbose(System.String format, params System.Object[] args)
        {
        }

        ///             <summary>
        ///             Writes an warning message to the log using the specified value.
        ///             </summary>
        ///             
        ///             <param name="value">The value.</param>
        ///             <example>
        ///             <code>
        ///             Warning(new {FirstName = "John", LastName="Doe"});
        ///             </code>
        ///             </example>
        ///         
        public static void Warning(System.Object value)
        {
        }

        ///             <summary>
        ///             Writes an warning message to the log using the specified string value.
        ///             </summary>
        ///             
        ///             <param name="value">The value.</param>
        ///             <example>
        ///             <code>
        ///             Warning("{string}");
        ///             </code>
        ///             </example>
        ///         
        public static void Warning(System.String value)
        {
        }

        ///             <summary>
        ///             Writes a warning message to the log using the specified log message action.
        ///             Evaluation message only if verbosity same or more verbose.
        ///             </summary>
        ///             
        ///             <param name="logAction">The function called for message when logging.</param>
        ///             <example>
        ///             <code>
        ///             Warning(logAction=&gt;logAction("Hello {0}! Today is an {1:dddd}", "World", DateTime.Now));
        ///             </code>
        ///             </example>
        ///         
        public static void Warning(global::Cake.Core.Diagnostics.LogAction logAction)
        {
        }

        ///             <summary>
        ///             Writes a warning message to the log using the specified format information.
        ///             </summary>
        ///             
        ///             <param name="format">The format.</param>
        ///             <param name="args">The arguments.</param>
        ///             <example>
        ///             <code>
        ///             Warning("Hello {0}! Today is an {1:dddd}", "World", DateTime.Now);
        ///             </code>
        ///             </example>
        ///         
        public static void Warning(System.String format, params System.Object[] args)
        {
        }
    }
}

namespace Cake.Common.IO
{
    public static class DirectoryAliasesMetadata
    {
        ///             <summary>
        ///             Cleans the specified directories.
        ///             Cleaning a directory will remove all its content but not the directory itself.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var directoriesToClean = GetDirectories("./src/**/bin/");
        ///             CleanDirectories(directoriesToClean);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="directories">The directory paths.</param>
        ///         
        public static void CleanDirectories(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.DirectoryPath> directories)
        {
        }

        ///             <summary>
        ///             Cleans the specified directories.
        ///             Cleaning a directory will remove all its content but not the directory itself.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var directoriesToClean = new []{
        ///                 "./src/Cake/obj",
        ///                 "./src/Cake.Common/obj"
        ///             };
        ///             CleanDirectories(directoriesToClean);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="directories">The directory paths.</param>
        ///         
        public static void CleanDirectories(global::System.Collections.Generic.IEnumerable<System.String> directories)
        {
        }

        ///             <summary>
        ///             Cleans the directories matching the specified pattern.
        ///             Cleaning the directory will remove all its content but not the directory itself.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             CleanDirectories("./src/**/bin/debug");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The pattern to match.</param>
        ///         
        public static void CleanDirectories(System.String pattern)
        {
        }

        ///             <summary>
        ///             Cleans the directories matching the specified pattern.
        ///             Cleaning the directory will remove all its content but not the directory itself.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             Func&lt;IFileSystemInfo, bool&gt; exclude_node_modules =
        ///             fileSystemInfo=&gt;!fileSystemInfo.Path.FullPath.EndsWith(
        ///                             "node_modules",
        ///                             StringComparison.OrdinalIgnoreCase);
        ///             CleanDirectories("./src/**/bin/debug", exclude_node_modules);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The pattern to match.</param>
        ///             <param name="predicate">The predicate used to filter directories based on file system information.</param>
        ///         
        public static void CleanDirectories(System.String pattern, global::System.Func<global::Cake.Core.IO.IFileSystemInfo, System.Boolean> predicate)
        {
        }

        ///             <summary>
        ///             Cleans the specified directory.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             CleanDirectory("./src/Cake.Common/obj");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="path">The directory path.</param>
        ///         
        public static void CleanDirectory(global::Cake.Core.IO.DirectoryPath path)
        {
        }

        ///             <summary>
        ///             Cleans the specified directory.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             CleanDirectory("./src/Cake.Common/obj", fileSystemInfo=&gt;!fileSystemInfo.Hidden);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="path">The directory path.</param>
        ///             <param name="predicate">Predicate used to determine which files/directories should get deleted.</param>
        ///         
        public static void CleanDirectory(global::Cake.Core.IO.DirectoryPath path, global::System.Func<global::Cake.Core.IO.IFileSystemInfo, System.Boolean> predicate)
        {
        }

        ///             <summary>
        ///             Copies the contents of a directory, including subdirectories to the specified location.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             CopyDirectory("source_path", "destination_path");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="source">The source directory path.</param>
        ///             <param name="destination">The destination directory path.</param>
        ///         
        public static void CopyDirectory(global::Cake.Core.IO.DirectoryPath source, global::Cake.Core.IO.DirectoryPath destination)
        {
        }

        ///             <summary>
        ///             Creates the specified directory.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             CreateDirectory("publish");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="path">The directory path.</param>
        ///         
        public static void CreateDirectory(global::Cake.Core.IO.DirectoryPath path)
        {
        }

        ///             <summary>
        ///             Deletes the specified directories.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var directoriesToDelete = new []{
        ///                 "be",
        ///                 "gone"
        ///             };
        ///             DeleteDirectories(directoriesToDelete, recursive:true);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="directories">The directory paths.</param>
        ///             <param name="recursive">Will perform a recursive delete if set to <c>true</c>.</param>
        ///         
        public static void DeleteDirectories(global::System.Collections.Generic.IEnumerable<System.String> directories, System.Boolean recursive = false)
        {
        }

        ///             <summary>
        ///             Deletes the specified directories.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var directoriesToDelete = new DirectoryPath[]{
        ///                 Directory("be"),
        ///                 Directory("gone")
        ///             };
        ///             DeleteDirectories(directoriesToDelete, recursive:true);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="directories">The directory paths.</param>
        ///             <param name="recursive">Will perform a recursive delete if set to <c>true</c>.</param>
        ///         
        public static void DeleteDirectories(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.DirectoryPath> directories, System.Boolean recursive = false)
        {
        }

        ///             <summary>
        ///             Deletes the specified directory.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             DeleteDirectory("./be/gone", recursive:true);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="path">The directory path.</param>
        ///             <param name="recursive">Will perform a recursive delete if set to <c>true</c>.</param>
        ///         
        public static void DeleteDirectory(global::Cake.Core.IO.DirectoryPath path, System.Boolean recursive = false)
        {
        }

        ///              <summary>
        ///              Gets a directory path from string.
        ///              </summary>
        ///              <example>
        ///              <code>
        ///              // Get the temp directory.
        ///              var root = Directory("./");
        ///              var temp = root + Directory("temp");
        ///             
        ///              // Clean the directory.
        ///              CleanDirectory(temp);
        ///              </code>
        ///              </example>
        ///              
        ///              <param name="path">The path.</param>
        ///              <returns>A directory path.</returns>
        ///         
        public static global::Cake.Common.IO.Paths.ConvertableDirectoryPath Directory(System.String path)
        {
            return default(global::Cake.Common.IO.Paths.ConvertableDirectoryPath);
        }

        ///             <summary>
        ///             Determines whether the given path refers to an existing directory.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var dir = "publish";
        ///             if (!DirectoryExists(dir))
        ///             {
        ///                 CreateDirectory(dir);
        ///             }
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="path">The <see cref="T:Cake.Core.IO.DirectoryPath" /> to check.</param>
        ///             <returns><c>true</c> if <paramref name="path" /> refers to an existing directory;
        ///             <c>false</c> if the directory does not exist or an error occurs when trying to
        ///             determine if the specified path exists.</returns>
        ///         
        public static System.Boolean DirectoryExists(global::Cake.Core.IO.DirectoryPath path)
        {
            return default(System.Boolean);
        }

        ///             <summary>
        ///             Creates the specified directory if it does not exist.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             EnsureDirectoryExists("publish");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="path">The directory path.</param>
        ///         
        public static void EnsureDirectoryExists(global::Cake.Core.IO.DirectoryPath path)
        {
        }

        ///             <summary>
        ///             Makes the path absolute (if relative) using the current working directory.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var path = MakeAbsolute(Directory("./resources"));
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="path">The path.</param>
        ///             <returns>An absolute directory path.</returns>
        ///         
        public static global::Cake.Core.IO.DirectoryPath MakeAbsolute(global::Cake.Core.IO.DirectoryPath path)
        {
            return default(global::Cake.Core.IO.DirectoryPath);
        }

        ///             <summary>
        ///             Moves an existing directory to a new location, providing the option to specify a new directory name.
        ///             </summary>
        ///             
        ///             <param name="directoryPath">The directory path.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <example>
        ///             <code>
        ///             MoveDirectory("mydir", "newparent/newdir");
        ///             </code>
        ///             </example>
        ///         
        public static void MoveDirectory(global::Cake.Core.IO.DirectoryPath directoryPath, global::Cake.Core.IO.DirectoryPath targetDirectoryPath)
        {
        }
    }

    public static class FileAliasesMetadata
    {
        ///             <summary>
        ///             Copies an existing file to a new file, providing the option to specify a new file name.
        ///             </summary>
        ///             
        ///             <param name="filePath">The file path.</param>
        ///             <param name="targetFilePath">The target file path.</param>
        ///             <example>
        ///             <code>
        ///             CopyFile("test.tmp", "test.txt");
        ///             </code>
        ///             </example>
        ///         
        public static void CopyFile(global::Cake.Core.IO.FilePath filePath, global::Cake.Core.IO.FilePath targetFilePath)
        {
        }

        ///             <summary>
        ///             Copies existing files to a new location.
        ///             </summary>
        ///             
        ///             <param name="filePaths">The file paths.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <example>
        ///             <code>
        ///             var files = GetFiles("./**/Cake.*");
        ///             CopyFiles(files, "destination");
        ///             </code>
        ///             </example>
        ///         
        public static void CopyFiles(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> filePaths, global::Cake.Core.IO.DirectoryPath targetDirectoryPath)
        {
        }

        ///             <summary>
        ///             Copies existing files to a new location.
        ///             </summary>
        ///             
        ///             <param name="filePaths">The file paths.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <example>
        ///             <code>
        ///             CreateDirectory("destination");
        ///             var files = new [] {
        ///                 "Cake.exe",
        ///                 "Cake.pdb"
        ///             };
        ///             CopyFiles(files, "destination");
        ///             </code>
        ///             </example>
        ///         
        public static void CopyFiles(global::System.Collections.Generic.IEnumerable<System.String> filePaths, global::Cake.Core.IO.DirectoryPath targetDirectoryPath)
        {
        }

        ///             <summary>
        ///             Copies all files matching the provided pattern to a new location.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <example>
        ///             <code>
        ///             CopyFiles("Cake.*", "./publish");
        ///             </code>
        ///             </example>
        ///         
        public static void CopyFiles(System.String pattern, global::Cake.Core.IO.DirectoryPath targetDirectoryPath)
        {
        }

        ///             <summary>
        ///             Copies existing files to a new location.
        ///             </summary>
        ///             
        ///             <param name="filePaths">The file paths.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <param name="preserveFolderStructure">Keep the folder structure.</param>
        ///             <example>
        ///             <code>
        ///             var files = GetFiles("./**/Cake.*");
        ///             CopyFiles(files, "destination");
        ///             </code>
        ///             </example>
        ///         
        public static void CopyFiles(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> filePaths, global::Cake.Core.IO.DirectoryPath targetDirectoryPath, System.Boolean preserveFolderStructure)
        {
        }

        ///             <summary>
        ///             Copies existing files to a new location.
        ///             </summary>
        ///             
        ///             <param name="filePaths">The file paths.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <param name="preserveFolderStructure">Keep the folder structure.</param>
        ///             <example>
        ///             <code>
        ///             CreateDirectory("destination");
        ///             var files = new [] {
        ///                 "Cake.exe",
        ///                 "Cake.pdb"
        ///             };
        ///             CopyFiles(files, "destination");
        ///             </code>
        ///             </example>
        ///         
        public static void CopyFiles(global::System.Collections.Generic.IEnumerable<System.String> filePaths, global::Cake.Core.IO.DirectoryPath targetDirectoryPath, System.Boolean preserveFolderStructure)
        {
        }

        ///             <summary>
        ///             Copies all files matching the provided pattern to a new location.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <param name="preserveFolderStructure">Keep the folder structure.</param>
        ///             <example>
        ///             <code>
        ///             CopyFiles("Cake.*", "./publish");
        ///             </code>
        ///             </example>
        ///         
        public static void CopyFiles(System.String pattern, global::Cake.Core.IO.DirectoryPath targetDirectoryPath, System.Boolean preserveFolderStructure)
        {
        }

        ///             <summary>
        ///             Copies an existing file to a new location.
        ///             </summary>
        ///             
        ///             <param name="filePath">The file path.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <example>
        ///             <code>
        ///             CopyFileToDirectory("test.txt", "./targetdir");
        ///             </code>
        ///             </example>
        ///         
        public static void CopyFileToDirectory(global::Cake.Core.IO.FilePath filePath, global::Cake.Core.IO.DirectoryPath targetDirectoryPath)
        {
        }

        ///             <summary>
        ///             Deletes the specified file.
        ///             </summary>
        ///             
        ///             <param name="filePath">The file path.</param>
        ///             <example>
        ///             <code>
        ///             DeleteFile("deleteme.txt");
        ///             </code>
        ///             </example>
        ///         
        public static void DeleteFile(global::Cake.Core.IO.FilePath filePath)
        {
        }

        ///             <summary>
        ///             Deletes the specified files.
        ///             </summary>
        ///             
        ///             <param name="filePaths">The file paths.</param>
        ///             <example>
        ///             <code>
        ///             var files = GetFiles("./destination/Cake.*");
        ///             DeleteFiles(files);
        ///             </code>
        ///             </example>
        ///         
        public static void DeleteFiles(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> filePaths)
        {
        }

        ///             <summary>
        ///             Deletes the specified files.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <example>
        ///             <code>
        ///             DeleteFiles("./publish/Cake.*");
        ///             </code>
        ///             </example>
        ///         
        public static void DeleteFiles(System.String pattern)
        {
        }

        ///              <summary>
        ///              Gets a file path from string.
        ///              </summary>
        ///              <example>
        ///              <code>
        ///              // Get the temp file.
        ///              var root = Directory("./");
        ///              var temp = root + File("temp");
        ///             
        ///              // Delete the file.
        ///              CleanDirectory(temp);
        ///              </code>
        ///              </example>
        ///              
        ///              <param name="path">The path.</param>
        ///              <returns>A file path.</returns>
        ///         
        public static global::Cake.Common.IO.Paths.ConvertableFilePath File(System.String path)
        {
            return default(global::Cake.Common.IO.Paths.ConvertableFilePath);
        }

        ///             <summary>
        ///             Determines whether the given path refers to an existing file.
        ///             </summary>
        ///             
        ///             <param name="filePath">The <see cref="T:Cake.Core.IO.FilePath" /> to check.</param>
        ///             <returns><c>true</c> if <paramref name="filePath" /> refers to an existing file;
        ///             <c>false</c> if the file does not exist or an error occurs when trying to
        ///             determine if the specified file exists.</returns>
        ///             <example>
        ///             <code>
        ///             if (FileExists("findme.txt"))
        ///             {
        ///                 Information("File exists!");
        ///             }
        ///             </code>
        ///             </example>
        ///         
        public static System.Boolean FileExists(global::Cake.Core.IO.FilePath filePath)
        {
            return default(System.Boolean);
        }

        ///             <summary>
        ///             Gets the size of a file in bytes.
        ///             </summary>
        ///             
        ///             <param name="filePath">The path.</param>
        ///             <returns>Size of file in bytes or -1 if file doesn't exist.</returns>
        ///             <example>
        ///             <code>
        ///             Information("File size: {0}", FileSize("./build.cake"));
        ///             </code>
        ///             </example>
        ///         
        public static System.Int64 FileSize(global::Cake.Core.IO.FilePath filePath)
        {
            return default(System.Int64);
        }

        ///             <summary>
        ///             Makes the path absolute (if relative) using the current working directory.
        ///             </summary>
        ///             
        ///             <param name="path">The path.</param>
        ///             <returns>An absolute file path.</returns>
        ///             <example>
        ///             <code>
        ///             var path = MakeAbsolute(File("./resources"));
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Core.IO.FilePath MakeAbsolute(global::Cake.Core.IO.FilePath path)
        {
            return default(global::Cake.Core.IO.FilePath);
        }

        ///             <summary>
        ///             Moves an existing file to a new location, providing the option to specify a new file name.
        ///             </summary>
        ///             
        ///             <param name="filePath">The file path.</param>
        ///             <param name="targetFilePath">The target file path.</param>
        ///             <example>
        ///             <code>
        ///             MoveFile("test.tmp", "test.txt");
        ///             </code>
        ///             </example>
        ///         
        public static void MoveFile(global::Cake.Core.IO.FilePath filePath, global::Cake.Core.IO.FilePath targetFilePath)
        {
        }

        ///             <summary>
        ///             Moves existing files to a new location.
        ///             </summary>
        ///             
        ///             <param name="filePaths">The file paths.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <example>
        ///             <code>
        ///             var files = GetFiles("./publish/Cake.*");
        ///             MoveFiles(files, "destination");
        ///             </code>
        ///             </example>
        ///         
        public static void MoveFiles(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> filePaths, global::Cake.Core.IO.DirectoryPath targetDirectoryPath)
        {
        }

        ///             <summary>
        ///             Moves existing files matching the specified pattern to a new location.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <example>
        ///             <code>
        ///             MoveFiles("./publish/Cake.*", "./destination");
        ///             </code>
        ///             </example>
        ///         
        public static void MoveFiles(System.String pattern, global::Cake.Core.IO.DirectoryPath targetDirectoryPath)
        {
        }

        ///             <summary>
        ///             Moves an existing file to a new location.
        ///             </summary>
        ///             
        ///             <param name="filePath">The file path.</param>
        ///             <param name="targetDirectoryPath">The target directory path.</param>
        ///             <example>
        ///             <code>
        ///             MoveFileToDirectory("test.txt", "./targetdir");
        ///             </code>
        ///             </example>
        ///         
        public static void MoveFileToDirectory(global::Cake.Core.IO.FilePath filePath, global::Cake.Core.IO.DirectoryPath targetDirectoryPath)
        {
        }
    }

    public static class GlobbingAliasesMetadata
    {
        ///             <summary>
        ///             Gets all directory matching the specified pattern.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var directories = GetDirectories("./src/**/obj/*");
        ///             foreach(var directory in directories)
        ///             {
        ///                 Information("Directory: {0}", directory);
        ///             }
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The glob pattern to match.</param>
        ///             <returns>A <see cref="T:Cake.Core.IO.DirectoryPathCollection" />.</returns>
        ///         
        public static global::Cake.Core.IO.DirectoryPathCollection GetDirectories(System.String pattern)
        {
            return default(global::Cake.Core.IO.DirectoryPathCollection);
        }

        ///              <summary>
        ///              Gets all directory matching the specified pattern.
        ///              </summary>
        ///              <example>
        ///              <code>
        ///              Func&lt;IFileSystemInfo, bool&gt; exclude_node_modules =
        ///                  fileSystemInfo =&gt; !fileSystemInfo.Path.FullPath.EndsWith(
        ///                      "node_modules", StringComparison.OrdinalIgnoreCase);
        ///             
        ///              var directories = GetDirectories("./src/**/obj/*", exclude_node_modules);
        ///              foreach(var directory in directories)
        ///              {
        ///                  Information("Directory: {0}", directory);
        ///              }
        ///              </code>
        ///              </example>
        ///              
        ///              <param name="pattern">The glob pattern to match.</param>
        ///              <param name="predicate">The predicate used to filter directories based on file system information.</param>
        ///              <returns>A <see cref="T:Cake.Core.IO.DirectoryPathCollection" />.</returns>
        ///         
        public static global::Cake.Core.IO.DirectoryPathCollection GetDirectories(System.String pattern, global::System.Func<global::Cake.Core.IO.IDirectory, System.Boolean> predicate)
        {
            return default(global::Cake.Core.IO.DirectoryPathCollection);
        }

        ///             <summary>
        ///             Gets all files matching the specified pattern.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var files = GetFiles("./**/Cake.*.dll");
        ///             foreach(var file in files)
        ///             {
        ///                 Information("File: {0}", file);
        ///             }
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The glob pattern to match.</param>
        ///             <returns>A <see cref="T:Cake.Core.IO.FilePathCollection" />.</returns>
        ///         
        public static global::Cake.Core.IO.FilePathCollection GetFiles(System.String pattern)
        {
            return default(global::Cake.Core.IO.FilePathCollection);
        }

        ///              <summary>
        ///              Gets all files matching the specified pattern.
        ///              </summary>
        ///              <example>
        ///              <code>
        ///              Func&lt;IFileSystemInfo, bool&gt; exclude_node_modules =
        ///                  fileSystemInfo =&gt; !fileSystemInfo.Path.FullPath.EndsWith(
        ///                      "node_modules", StringComparison.OrdinalIgnoreCase);
        ///             
        ///              var files = GetFiles("./**/Cake.*.dll", exclude_node_modules);
        ///              foreach(var file in files)
        ///              {
        ///                  Information("File: {0}", file);
        ///              }
        ///              </code>
        ///              </example>
        ///              
        ///              <param name="pattern">The glob pattern to match.</param>
        ///              <param name="predicate">The predicate used to filter directories based on file system information.</param>
        ///              <returns>A <see cref="T:Cake.Core.IO.FilePathCollection" />.</returns>
        ///         
        public static global::Cake.Core.IO.FilePathCollection GetFiles(System.String pattern, global::System.Func<global::Cake.Core.IO.IDirectory, System.Boolean> predicate)
        {
            return default(global::Cake.Core.IO.FilePathCollection);
        }
    }

    public static class ZipAliasesMetadata
    {
        ///             <summary>
        ///             Unzips the specified file
        ///             </summary>
        ///             
        ///             <param name="zipFile">Zip file to unzip.</param>
        ///             <param name="outputPath">Output path to unzip into.</param>
        ///             <example>
        ///             <code>
        ///             Unzip("Cake.zip", "./cake");
        ///             </code>
        ///             </example>
        ///         
        public static void Unzip(global::Cake.Core.IO.FilePath zipFile, global::Cake.Core.IO.DirectoryPath outputPath)
        {
        }

        ///             <summary>
        ///             Zips the specified directory.
        ///             </summary>
        ///             
        ///             <param name="rootPath">The root path.</param>
        ///             <param name="outputPath">The output path.</param>
        ///             <example>
        ///             <code>
        ///             Zip("./publish", "publish.zip");
        ///             </code>
        ///             </example>
        ///         
        public static void Zip(global::Cake.Core.IO.DirectoryPath rootPath, global::Cake.Core.IO.FilePath outputPath)
        {
        }

        ///             <summary>
        ///             Zips the specified files.
        ///             </summary>
        ///             
        ///             <param name="rootPath">The root path.</param>
        ///             <param name="outputPath">The output path.</param>
        ///             <param name="filePaths">The file paths.</param>
        ///             <example>
        ///             <code>
        ///             var files = new [] {
        ///                 "./src/Cake/bin/Debug/Autofac.dll",
        ///                 "./src/Cake/bin/Debug/Cake.Common.dll",
        ///                 "./src/Cake/bin/Debug/Cake.Core.dll",
        ///                 "./src/Cake/bin/Debug/Cake.exe"
        ///             };
        ///             Zip("./", "cakebinaries.zip", files);
        ///             </code>
        ///             </example>
        ///         
        public static void Zip(global::Cake.Core.IO.DirectoryPath rootPath, global::Cake.Core.IO.FilePath outputPath, global::System.Collections.Generic.IEnumerable<System.String> filePaths)
        {
        }

        ///             <summary>
        ///             Zips the specified files.
        ///             </summary>
        ///             
        ///             <param name="rootPath">The root path.</param>
        ///             <param name="outputPath">The output path.</param>
        ///             <param name="filePaths">The file paths.</param>
        ///             <example>
        ///             <code>
        ///             var files = GetFiles("./**/Cake.*.dll");
        ///             Zip("./", "cakeassemblies.zip", files);
        ///             </code>
        ///             </example>
        ///         
        public static void Zip(global::Cake.Core.IO.DirectoryPath rootPath, global::Cake.Core.IO.FilePath outputPath, global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> filePaths)
        {
        }

        ///             <summary>
        ///             Zips the files matching the specified pattern.
        ///             </summary>
        ///             
        ///             <param name="rootPath">The root path.</param>
        ///             <param name="outputPath">The output path.</param>
        ///             <param name="pattern">The pattern.</param>
        ///             <example>
        ///             <code>
        ///             Zip("./", "xmlfiles.zip", "./*.xml");
        ///             </code>
        ///             </example>
        ///         
        public static void Zip(global::Cake.Core.IO.DirectoryPath rootPath, global::Cake.Core.IO.FilePath outputPath, System.String pattern)
        {
        }
    }
}

namespace Cake.Common.Net
{
    public static class HttpAliasesMetadata
    {
        ///             <summary>
        ///             Downloads the specified resource over HTTP to a temporary file.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var address = new Uri("http://www.example.org/index.html");
        ///             var resource = DownloadFile(address);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of file to download.</param>
        ///             <returns>The path to the downloaded file.</returns>
        ///         
        public static global::Cake.Core.IO.FilePath DownloadFile(global::System.Uri address)
        {
            return default(global::Cake.Core.IO.FilePath);
        }

        ///             <summary>
        ///             Downloads the specified resource over HTTP to a temporary file.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var resource = DownloadFile("http://www.example.org/index.html");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of the resource to download.</param>
        ///             <returns>The path to the downloaded file.</returns>
        ///         
        public static global::Cake.Core.IO.FilePath DownloadFile(System.String address)
        {
            return default(global::Cake.Core.IO.FilePath);
        }

        ///             <summary>
        ///             Downloads the specified resource over HTTP to the specified output path.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var outputPath = File("./index.html");
        ///             DownloadFile("http://www.example.org/index.html", outputPath);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of the resource to download.</param>
        ///             <param name="outputPath">The output path.</param>
        ///         
        public static void DownloadFile(System.String address, global::Cake.Core.IO.FilePath outputPath)
        {
        }

        ///             <summary>
        ///             Downloads the specified resource over HTTP to a temporary file with specified settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var address = new Uri("http://www.example.org/index.html");
        ///             var resource = DownloadFile(address, new DownloadFileSettings()
        ///             {
        ///                 Username = "bob",
        ///                 Password = "builder"
        ///             });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of file to download.</param>
        ///             <param name="settings">The settings.</param>
        ///             <returns>The path to the downloaded file.</returns>
        ///         
        public static global::Cake.Core.IO.FilePath DownloadFile(global::System.Uri address, global::Cake.Common.Net.DownloadFileSettings settings)
        {
            return default(global::Cake.Core.IO.FilePath);
        }

        ///             <summary>
        ///             Downloads the specified resource over HTTP to a temporary file with specified settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var resource = DownloadFile("http://www.example.org/index.html", new DownloadFileSettings()
        ///             {
        ///                 Username = "bob",
        ///                 Password = "builder"
        ///             });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of the resource to download.</param>
        ///             <param name="settings">The settings.</param>
        ///             <returns>The path to the downloaded file.</returns>
        ///         
        public static global::Cake.Core.IO.FilePath DownloadFile(System.String address, global::Cake.Common.Net.DownloadFileSettings settings)
        {
            return default(global::Cake.Core.IO.FilePath);
        }

        ///             <summary>
        ///             Downloads the specified resource over HTTP to the specified output path.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var address = new Uri("http://www.example.org/index.html");
        ///             var outputPath = File("./index.html");
        ///             DownloadFile(address, outputPath, new DownloadFileSettings()
        ///             {
        ///                 Username = "bob",
        ///                 Password = "builder"
        ///             });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of the resource to download.</param>
        ///             <param name="outputPath">The output path.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void DownloadFile(global::System.Uri address, global::Cake.Core.IO.FilePath outputPath, global::Cake.Common.Net.DownloadFileSettings settings)
        {
        }

        ///             <summary>
        ///             Downloads the specified resource over HTTP to the specified output path and settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var outputPath = File("./index.html");
        ///             DownloadFile("http://www.example.org/index.html", outputPath, new DownloadFileSettings()
        ///             {
        ///                 Username = "bob",
        ///                 Password = "builder"
        ///             });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of the resource to download.</param>
        ///             <param name="outputPath">The output path.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void DownloadFile(System.String address, global::Cake.Core.IO.FilePath outputPath, global::Cake.Common.Net.DownloadFileSettings settings)
        {
        }

        ///             <summary>
        ///             Uploads the specified file via a HTTP POST to the specified uri using multipart/form-data.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var address = "http://www.example.org/upload";
        ///             UploadFile(address, @"path/to/file.txt");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of the upload resource.</param>
        ///             <param name="filePath">The file to upload.</param>
        ///         
        public static void UploadFile(System.String address, global::Cake.Core.IO.FilePath filePath)
        {
        }

        ///             <summary>
        ///             Uploads the specified file via a HTTP POST to the specified uri using multipart/form-data.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var address = new Uri("http://www.example.org/upload");
        ///             UploadFile(address, @"path/to/file.txt");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of the upload resource.</param>
        ///             <param name="filePath">The file to upload.</param>
        ///         
        public static void UploadFile(global::System.Uri address, global::Cake.Core.IO.FilePath filePath)
        {
        }

        ///             <summary>
        ///             Uploads the specified byte array via a HTTP POST to the specified uri using multipart/form-data.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var address = "http://www.example.org/upload";
        ///             UploadFile(address, @"path/to/file.txt");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of the upload resource.</param>
        ///             <param name="data">The data to upload.</param>
        ///             <param name="fileName">The filename to give the uploaded data</param>
        ///         
        public static void UploadFile(System.String address, System.Byte[] data, System.String fileName)
        {
        }

        ///             <summary>
        ///             Uploads the specified byte array via a HTTP POST to the specified uri using multipart/form-data.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var address = new Uri("http://www.example.org/upload");
        ///             UploadFile(address, @"path/to/file.txt");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="address">The URL of the upload resource.</param>
        ///             <param name="data">The data to upload.</param>
        ///             <param name="fileName">The filename to give the uploaded data</param>
        ///         
        public static void UploadFile(global::System.Uri address, System.Byte[] data, System.String fileName)
        {
        }
    }
}

namespace Cake.Common.Security
{
    public static class SecurityAliasesMetadata
    {
        ///             <summary>
        ///             Calculates the hash for a given file using the default (SHA256) algorithm.
        ///             </summary>
        ///             
        ///             <param name="filePath">The file path.</param>
        ///             <returns>A <see cref="T:Cake.Common.Security.FileHash" /> instance representing the calculated hash.</returns>
        ///             <example>
        ///             <code>
        ///             Information(
        ///                "Cake executable file SHA256 hash: {0}",
        ///                CalculateFileHash("Cake.exe").ToHex());
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Security.FileHash CalculateFileHash(global::Cake.Core.IO.FilePath filePath)
        {
            return default(global::Cake.Common.Security.FileHash);
        }

        ///             <summary>
        ///             Calculates the hash for a given file.
        ///             </summary>
        ///             
        ///             <param name="filePath">The file path.</param>
        ///             <param name="hashAlgorithm">The hash algorithm to use.</param>
        ///             <returns>A <see cref="T:Cake.Common.Security.FileHash" /> instance representing the calculated hash.</returns>
        ///             <example>
        ///             <code>
        ///             Information(
        ///                 "Cake executable file MD5 hash: {0}",
        ///                 CalculateFileHash("Cake.exe", HashAlgorithm.MD5).ToHex());
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Security.FileHash CalculateFileHash(global::Cake.Core.IO.FilePath filePath, global::Cake.Common.Security.HashAlgorithm hashAlgorithm)
        {
            return default(global::Cake.Common.Security.FileHash);
        }
    }
}

namespace Cake.Common.Solution
{
    public static class SolutionAliasesMetadata
    {
        ///             <summary>
        ///             Parses project information from a solution file.
        ///             </summary>
        ///             
        ///             <param name="solutionPath">The solution path.</param>
        ///             <returns>A parsed solution.</returns>
        ///             <example>
        ///             <code>
        ///             var solutionPath = "./src/Cake.sln";
        ///             Information("Parsing {0}", solutionPath);
        ///             var parsedSolution = ParseSolution(solutionPath);
        ///             foreach(var project in parsedSolution.Projects)
        ///             {
        ///                 Information(
        ///                     @"Solution project file:
        ///                 Name: {0}
        ///                 Path: {1}
        ///                 Id  : {2}
        ///                 Type: {3}",
        ///                     project.Name,
        ///                     project.Path,
        ///                     project.Id,
        ///                     project.Type
        ///                 );
        ///             }
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Solution.SolutionParserResult ParseSolution(global::Cake.Core.IO.FilePath solutionPath)
        {
            return default(global::Cake.Common.Solution.SolutionParserResult);
        }
    }
}

namespace Cake.Common.Solution.Project
{
    public static class ProjectAliasesMetadata
    {
        ///             <summary>
        ///             Parses project information from project file
        ///             </summary>
        ///             
        ///             <param name="projectPath">The project file path.</param>
        ///             <returns>A parsed project.</returns>
        ///             <example>
        ///             <code>
        ///             var parsedProject = ParseProject("./src/Cake/Cake.csproj");
        ///             Information(
        ///                 @"    Parsed project file:
        ///                 Configuration         : {0}
        ///                 Platform              : {1}
        ///                 OutputType            : {2}
        ///                 OutputPath            : {3}
        ///                 RootNameSpace         : {4}
        ///                 AssemblyName          : {5}
        ///                 TargetFrameworkVersion: {6}
        ///                 Files                 : {7}",
        ///                 parsedProject.Configuration,
        ///                 parsedProject.Platform,
        ///                 parsedProject.OutputType,
        ///                 parsedProject.OutputPath,
        ///                 parsedProject.RootNameSpace,
        ///                 parsedProject.AssemblyName,
        ///                 parsedProject.TargetFrameworkVersion,
        ///                 string.Concat(
        ///                     parsedProject
        ///                         .Files
        ///                         .Select(
        ///                             file=&gt;  string.Format(
        ///                                         "\r\n            {0}",
        ///                                         file.FilePath
        ///                                     )
        ///                         )
        ///                 )
        ///             );
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Solution.Project.ProjectParserResult ParseProject(global::Cake.Core.IO.FilePath projectPath)
        {
            return default(global::Cake.Common.Solution.Project.ProjectParserResult);
        }
    }
}

namespace Cake.Common.Solution.Project.Properties
{
    public static class AssemblyInfoAliasesMetadata
    {
        ///             <summary>
        ///             Creates an assembly information file.
        ///             </summary>
        ///             
        ///             <param name="outputPath">The output path.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             var file = "./SolutionInfo.cs";
        ///             var version = "0.0.1";
        ///             var buildNo = "123";
        ///             var semVersion = string.Concat(version + "-" + buildNo);
        ///             CreateAssemblyInfo(file, new AssemblyInfoSettings {
        ///                 Product = "SampleProject",
        ///                 Version = version,
        ///                 FileVersion = version,
        ///                 InformationalVersion = semVersion,
        ///                 Copyright = string.Format("Copyright (c) Contoso 2014 - {0}", DateTime.Now.Year)
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void CreateAssemblyInfo(global::Cake.Core.IO.FilePath outputPath, global::Cake.Common.Solution.Project.Properties.AssemblyInfoSettings settings)
        {
        }

        ///             <summary>
        ///             Parses an existing assembly information file.
        ///             </summary>
        ///             
        ///             <param name="assemblyInfoPath">The assembly info path.</param>
        ///             <returns>The content of the assembly info file.</returns>
        ///             <example>
        ///             <code>
        ///             var assemblyInfo = ParseAssemblyInfo("./SolutionInfo.cs");
        ///             Information("Version: {0}", assemblyInfo.AssemblyVersion);
        ///             Information("File version: {0}", assemblyInfo.AssemblyFileVersion);
        ///             Information("Informational version: {0}", assemblyInfo.AssemblyInformationalVersion);
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Solution.Project.Properties.AssemblyInfoParseResult ParseAssemblyInfo(global::Cake.Core.IO.FilePath assemblyInfoPath)
        {
            return default(global::Cake.Common.Solution.Project.Properties.AssemblyInfoParseResult);
        }
    }
}

namespace Cake.Common.Solution.Project.XmlDoc
{
    public static class XmlDocAliasesMetadata
    {
        ///             <summary>
        ///             Parses Xml documentation example code from given path.
        ///             </summary>
        ///             
        ///             <param name="xmlFilePath">The Path to the file to parse.</param>
        ///             <returns>Parsed example code.</returns>
        ///             <example>
        ///             <code>
        ///             var exampleCodes = ParseXmlDocExampleCode("./Cake.Common.xml");
        ///             foreach(var exampleCode in exampleCodes)
        ///             {
        ///                 Information(
        ///                     "{0}\r\n{1}",
        ///                     exampleCode.Name,
        ///                     exampleCode.Code
        ///                 );
        ///             }
        ///             </code>
        ///             </example>
        ///         
        public static global::System.Collections.Generic.IEnumerable<global::Cake.Common.Solution.Project.XmlDoc.XmlDocExampleCode> ParseXmlDocExampleCode(global::Cake.Core.IO.FilePath xmlFilePath)
        {
            return default(global::System.Collections.Generic.IEnumerable<global::Cake.Common.Solution.Project.XmlDoc.XmlDocExampleCode>);
        }

        ///             <summary>
        ///             Parses Xml documentation example code from file(s) using given pattern.
        ///             </summary>
        ///             
        ///             <param name="pattern">The globber file pattern.</param>
        ///             <returns>Parsed example code.</returns>
        ///             <example>
        ///             <code>
        ///             var filesExampleCodes = ParseXmlDocFilesExampleCode("./Cake.*.xml");
        ///             foreach(var exampleCode in filesExampleCodes)
        ///             {
        ///                 Information(
        ///                     "{0}\r\n{1}",
        ///                     exampleCode.Name,
        ///                     exampleCode.Code
        ///                 );
        ///             }
        ///             </code>
        ///             </example>
        ///         
        public static global::System.Collections.Generic.IEnumerable<global::Cake.Common.Solution.Project.XmlDoc.XmlDocExampleCode> ParseXmlDocFilesExampleCode(System.String pattern)
        {
            return default(global::System.Collections.Generic.IEnumerable<global::Cake.Common.Solution.Project.XmlDoc.XmlDocExampleCode>);
        }
    }
}

namespace Cake.Common.Text
{
    public static class TextTransformationAliasesMetadata
    {
        ///             <summary>
        ///             Creates a text transformation from the provided template.
        ///             </summary>
        ///             
        ///             <param name="template">The template.</param>
        ///             <returns>A <see cref="T:Cake.Common.Text.TextTransformation`1" /> representing the provided template.</returns>
        ///             <example>
        ///             This sample shows how to create a <see cref="T:Cake.Common.Text.TextTransformation`1" /> using
        ///             the specified template.
        ///             <code>
        ///             string text = TransformText("Hello &lt;%subject%&gt;!")
        ///                .WithToken("subject", "world")
        ///                .ToString();
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Text.TextTransformation<global::Cake.Core.Text.TextTransformationTemplate> TransformText(System.String template)
        {
            return default(global::Cake.Common.Text.TextTransformation<global::Cake.Core.Text.TextTransformationTemplate>);
        }

        ///             <summary>
        ///             Creates a text transformation from the provided template, using the specified placeholder.
        ///             </summary>
        ///             
        ///             <param name="template">The template.</param>
        ///             <param name="leftPlaceholder">The left placeholder.</param>
        ///             <param name="rightPlaceholder">The right placeholder.</param>
        ///             <returns>A <see cref="T:Cake.Common.Text.TextTransformation`1" /> representing the provided template.</returns>
        ///             <example>
        ///             This sample shows how to create a <see cref="T:Cake.Common.Text.TextTransformation`1" /> using
        ///             the specified template and placeholder.
        ///             <code>
        ///             string text = TransformText("Hello {subject}!", "{", "}")
        ///                .WithToken("subject", "world")
        ///                .ToString();
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Text.TextTransformation<global::Cake.Core.Text.TextTransformationTemplate> TransformText(System.String template, System.String leftPlaceholder, System.String rightPlaceholder)
        {
            return default(global::Cake.Common.Text.TextTransformation<global::Cake.Core.Text.TextTransformationTemplate>);
        }

        ///             <summary>
        ///             Creates a text transformation from the provided template on disc.
        ///             </summary>
        ///             
        ///             <param name="path">The template file path.</param>
        ///             <returns>A <see cref="T:Cake.Common.Text.TextTransformation`1" /> representing the provided template.</returns>
        ///             <example>
        ///             This sample shows how to create a <see cref="T:Cake.Common.Text.TextTransformation`1" /> using
        ///             the specified template file with the placeholder format <c>&lt;%key%&gt;</c>.
        ///             <code>
        ///             string text = TransformTextFile("./template.txt")
        ///                .WithToken("subject", "world")
        ///                .ToString();
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Text.TextTransformation<global::Cake.Core.Text.TextTransformationTemplate> TransformTextFile(global::Cake.Core.IO.FilePath path)
        {
            return default(global::Cake.Common.Text.TextTransformation<global::Cake.Core.Text.TextTransformationTemplate>);
        }

        ///             <summary>
        ///             Creates a text transformation from the provided template on disc, using the specified placeholder.
        ///             </summary>
        ///             
        ///             <param name="path">The template file path.</param>
        ///             <param name="leftPlaceholder">The left placeholder.</param>
        ///             <param name="rightPlaceholder">The right placeholder.</param>
        ///             <returns>A <see cref="T:Cake.Common.Text.TextTransformation`1" /> representing the provided template.</returns>
        ///             <example>
        ///             This sample shows how to create a <see cref="T:Cake.Common.Text.TextTransformation`1" /> using
        ///             the specified template file and placeholder.
        ///             <code>
        ///             string text = TransformTextFile("./template.txt", "{", "}")
        ///                .WithToken("subject", "world")
        ///                .ToString();
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Text.TextTransformation<global::Cake.Core.Text.TextTransformationTemplate> TransformTextFile(global::Cake.Core.IO.FilePath path, System.String leftPlaceholder, System.String rightPlaceholder)
        {
            return default(global::Cake.Common.Text.TextTransformation<global::Cake.Core.Text.TextTransformationTemplate>);
        }
    }
}

namespace Cake.Common.Tools
{
    public static class DotNetBuildAliasesMetadata
    {
        ///             <summary>
        ///             Builds the specified solution using MSBuild or XBuild.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             DotNetBuild("./project/project.sln");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="solution">The solution.</param>
        ///         
        public static void DotNetBuild(global::Cake.Core.IO.FilePath solution)
        {
        }

        ///             <summary>
        ///             Builds the specified solution using MSBuild or XBuild.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             DotNetBuild("./project/project.sln", settings =&gt;
        ///                 settings.SetConfiguration("Debug")
        ///                     .SetVerbosity(Core.Diagnostics.Verbosity.Minimal)
        ///                     .WithTarget("Build")
        ///                     .WithProperty("TreatWarningsAsErrors","true"));
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="solution">The solution.</param>
        ///             <param name="configurator">The configurator.</param>
        ///         
        public static void DotNetBuild(global::Cake.Core.IO.FilePath solution, global::System.Action<global::Cake.Common.Tools.DotNetBuildSettings> configurator)
        {
        }
    }
}

namespace Cake.Common.Tools.Cake
{
    public static class CakeAliasesMetadata
    {
        ///             <summary>
        ///             Executes Cake expression out of process
        ///             </summary>
        ///             
        ///             <param name="cakeExpression">The cake expression</param>
        ///             <example>
        ///             <code>
        ///             CakeExecuteExpression("Information(\"Hello {0}\", \"World\");");
        ///             </code>
        ///             </example>
        ///         
        public static void CakeExecuteExpression(System.String cakeExpression)
        {
        }

        ///             <summary>
        ///             Executes Cake expression out of process
        ///             </summary>
        ///             
        ///             <param name="cakeExpression">The cake expression</param>
        ///             <param name="settings">The settings <see cref="T:Cake.Common.Tools.Cake.CakeSettings" />.</param>
        ///             <example>
        ///             <code>
        ///             CakeExecuteExpression(
        ///                 "Information(\"Hello {0}!\", Argument&lt;string&gt;(\"name\"));",
        ///                 new CakeSettings {
        ///                     ToolPath="./Cake.exe" ,
        ///                     Arguments = new Dictionary&lt;string, string&gt;{{"name", "World"}}
        ///                     });
        ///             </code>
        ///             </example>
        ///         
        public static void CakeExecuteExpression(System.String cakeExpression, global::Cake.Common.Tools.Cake.CakeSettings settings)
        {
        }

        ///             <summary>
        ///             Executes cake script out of process
        ///             </summary>
        ///             
        ///             <param name="cakeScriptPath">The script file.</param>
        ///             <example>
        ///             <code>
        ///             CakeExecuteScript("./helloworld.cake");
        ///             </code>
        ///             </example>
        ///         
        public static void CakeExecuteScript(global::Cake.Core.IO.FilePath cakeScriptPath)
        {
        }

        ///             <summary>
        ///             Executes cake script out of process
        ///             </summary>
        ///             
        ///             <param name="cakeScriptPath">The script file.</param>
        ///             <param name="settings">The settings <see cref="T:Cake.Common.Tools.Cake.CakeSettings" />.</param>
        ///             <example>
        ///             <code>
        ///             CakeExecuteScript("./helloworld.cake", new CakeSettings{ ToolPath="./Cake.exe" });
        ///             </code>
        ///             </example>
        ///         
        public static void CakeExecuteScript(global::Cake.Core.IO.FilePath cakeScriptPath, global::Cake.Common.Tools.Cake.CakeSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.Chocolatey
{
    public static class ChocolateyAliasesMetadata
    {
        ///             <summary>
        ///             Adds Chocolatey package source using the specified name &amp;source to global user config
        ///             </summary>
        ///             
        ///             <param name="name">Name of the source.</param>
        ///             <param name="source">Path to the package(s) source.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyAddSource("MySource", "http://www.mysource.com");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyAddSource(System.String name, System.String source)
        {
        }

        ///             <summary>
        ///             Adds Chocolatey package source using the specified name, source &amp; settings to global user config
        ///             </summary>
        ///             
        ///             <param name="name">Name of the source.</param>
        ///             <param name="source">Path to the package(s) source.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyAddSource("MySource", "http://www.mysource.com", new ChocolateySourcesSettings {
        ///                 UserName              = "user",
        ///                 Password              = "password",
        ///                 Priority              = 13,
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyAddSource(System.String name, System.String source, global::Cake.Common.Tools.Chocolatey.Sources.ChocolateySourcesSettings settings)
        {
        }

        ///             <summary>
        ///             Sets the Api Key for a Chocolatey Source using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="apiKey">The API Key.</param>
        ///             <param name="source">The source.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyApiKey("myApiKey", "http://www.mysource.com", new ChocolateyApiKeySettings {
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyApiKey(System.String apiKey, System.String source, global::Cake.Common.Tools.Chocolatey.ApiKey.ChocolateyApiKeySettings settings)
        {
        }

        ///             <summary>
        ///             Sets the config parameter using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="name">The name.</param>
        ///             <param name="value">The value.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyConfig("cacheLocation", @"c:\temp", new ChocolateyConfigSettings {
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyConfig(System.String name, System.String value, global::Cake.Common.Tools.Chocolatey.Config.ChocolateyConfigSettings settings)
        {
        }

        ///             <summary>
        ///             Disables a Chocolatey Feature using the specified name
        ///             </summary>
        ///             
        ///             <param name="name">Name of the feature.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyDisableFeature("checkSumFiles");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyDisableFeature(System.String name)
        {
        }

        ///             <summary>
        ///             Disables a Chocolatey Feature using the specified name and settings
        ///             </summary>
        ///             
        ///             <param name="name">Name of the feature.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyDisableFeature("checkSumFiles", new ChocolateyFeatureSettings {
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyDisableFeature(System.String name, global::Cake.Common.Tools.Chocolatey.Features.ChocolateyFeatureSettings settings)
        {
        }

        ///             <summary>
        ///             Disables a Chocolatey Source using the specified name
        ///             </summary>
        ///             
        ///             <param name="name">Name of the source.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyDisableSource("MySource");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyDisableSource(System.String name)
        {
        }

        ///             <summary>
        ///             Disables a Chocolatey Source using the specified name and settings
        ///             </summary>
        ///             
        ///             <param name="name">Name of the source.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyDisableSource("MySource", new ChocolateySourcesSettings {
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyDisableSource(System.String name, global::Cake.Common.Tools.Chocolatey.Sources.ChocolateySourcesSettings settings)
        {
        }

        ///             <summary>
        ///             Enables a Chocolatey Feature using the specified name
        ///             </summary>
        ///             
        ///             <param name="name">Name of the feature.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyEnableFeature("checkSumFiles");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyEnableFeature(System.String name)
        {
        }

        ///             <summary>
        ///             Enables a Chocolatey Feature using the specified name and settings
        ///             </summary>
        ///             
        ///             <param name="name">Name of the feature.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyEnableFeature("checkSumFiles", new ChocolateyFeatureSettings {
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyEnableFeature(System.String name, global::Cake.Common.Tools.Chocolatey.Features.ChocolateyFeatureSettings settings)
        {
        }

        ///             <summary>
        ///             Enables a Chocolatey Source using the specified name
        ///             </summary>
        ///             
        ///             <param name="name">Name of the source.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyEnableSource("MySource");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyEnableSource(System.String name)
        {
        }

        ///             <summary>
        ///             Enables a Chocolatey Source using the specified name and settings
        ///             </summary>
        ///             
        ///             <param name="name">Name of the source.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyEnableSource("MySource", new ChocolateySourcesSettings {
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyEnableSource(System.String name, global::Cake.Common.Tools.Chocolatey.Sources.ChocolateySourcesSettings settings)
        {
        }

        ///             <summary>
        ///             Installs a Chocolatey package.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to install.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyInstall("MyChocolateyPackage");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyInstall(System.String packageId)
        {
        }

        ///             <summary>
        ///             Installs a Chocolatey package using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to install.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyInstall("MyChocolateyPackage", new ChocolateyInstallSettings {
        ///                 Source                = true,
        ///                 Version               = "1.2.3",
        ///                 Prerelease            = false,
        ///                 Forcex86              = false,
        ///                 InstallArguments      = "arg1",
        ///                 OverrideArguments     = false,
        ///                 NotSilent             = false,
        ///                 PackageParameters     = "param1",
        ///                 AllowDowngrade        = false,
        ///                 SideBySide            = false,
        ///                 IgnoreDependencies    = false,
        ///                 ForceDependencies     = false,
        ///                 SkipPowerShell        = false,
        ///                 User                  = "user",
        ///                 Password              = "password",
        ///                 IgnoreChecksums       = false,
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyInstall(System.String packageId, global::Cake.Common.Tools.Chocolatey.Install.ChocolateyInstallSettings settings)
        {
        }

        ///             <summary>
        ///             Installs Chocolatey packages using the specified package configuration.
        ///             </summary>
        ///             
        ///             <param name="packageConfigPath">The package configuration to install.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyInstallFromConfig("./tools/packages.config");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyInstallFromConfig(global::Cake.Core.IO.FilePath packageConfigPath)
        {
        }

        ///             <summary>
        ///             Installs Chocolatey packages using the specified package configuration and settings.
        ///             </summary>
        ///             
        ///             <param name="packageConfigPath">The package configuration to install.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyInstallFromConfig("./tools/packages.config", new ChocolateyInstallSettings {
        ///                 Source                = true,
        ///                 Version               = "1.2.3",
        ///                 Prerelease            = false,
        ///                 Forcex86              = false,
        ///                 InstallArguments      = "arg1",
        ///                 OverrideArguments     = false,
        ///                 NotSilent             = false,
        ///                 PackageParameters     = "param1",
        ///                 AllowDowngrade        = false,
        ///                 SideBySide            = false,
        ///                 IgnoreDependencies    = false,
        ///                 ForceDependencies     = false,
        ///                 SkipPowerShell        = false,
        ///                 User                  = "user",
        ///                 Password              = "password",
        ///                 IgnoreChecksums       = false,
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyInstallFromConfig(global::Cake.Core.IO.FilePath packageConfigPath, global::Cake.Common.Tools.Chocolatey.Install.ChocolateyInstallSettings settings)
        {
        }

        ///             <summary>
        ///             Generate package specification files for a new package using the default settings.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to create.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyNew("MyChocolateyPackage");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyNew(System.String packageId)
        {
        }

        ///             <summary>
        ///             Generate package specification files for a new package using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to create.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyNew("MyChocolateyPackage", new ChocolateyNewSettings {
        ///                 PackageVersion = "1.2.3",
        ///                 MaintainerName = "John Doe",
        ///                 MaintainerRepo = "johndoe"
        ///             });
        ///             </code>
        ///             </example>
        ///             <example>
        ///             <code>
        ///             var settings = new ChocolateyNewSettings {
        ///                 MaintainerName = "John Doe"
        ///             }
        ///             settings.AdditionalPropertyValues("Tags", "CustomPackage");
        ///             ChocolateyNew("MyChocolateyPackage", settings);
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyNew(System.String packageId, global::Cake.Common.Tools.Chocolatey.New.ChocolateyNewSettings settings)
        {
        }

        ///              <summary>
        ///              Creates a Chocolatey package using the specified settings.
        ///              </summary>
        ///              
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var chocolateyPackSettings   = new ChocolateyPackSettings {
        ///                                                  Id                      = "TestChocolatey",
        ///                                                  Title                   = "The tile of the package",
        ///                                                  Version                 = "0.0.0.1",
        ///                                                  Authors                 = new[] {"John Doe"},
        ///                                                  Owners                  = new[] {"Contoso"},
        ///                                                  Summary                 = "Excellent summary of what the package does",
        ///                                                  Description             = "The description of the package",
        ///                                                  ProjectUrl              = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  PackageSourceUrl        = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  ProjectSourceUrl        = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  DocsUrl                 = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  MailingListUrl          = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  BugTrackerUrl           = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  Tags                    = new [] {"Cake", "Script", "Build"},
        ///                                                  Copyright               = "Some company 2015",
        ///                                                  LicenseUrl              = new Uri("https://github.com/SomeUser/TestChocolatey/blob/master/LICENSE.md"),
        ///                                                  RequireLicenseAcceptance= false,
        ///                                                  IconUrl                 = new Uri("http://cdn.rawgit.com/SomeUser/TestChocolatey/master/icons/testchocolatey.png"),
        ///                                                  ReleaseNotes            = new [] {"Bug fixes", "Issue fixes", "Typos"},
        ///                                                  Files                   = new [] {
        ///                                                                                       new ChocolateyNuSpecContent {Source = "bin/TestChocolatey.dll", Target = "bin"},
        ///                                                                                    },
        ///                                                  Debug                   = false,
        ///                                                  Verbose                 = false,
        ///                                                  Force                   = false,
        ///                                                  Noop                    = false,
        ///                                                  LimitOutput             = false,
        ///                                                  ExecutionTimeout        = 13,
        ///                                                  CacheLocation           = @"C:\temp",
        ///                                                  AllowUnofficial          = false
        ///                                              };
        ///             
        ///                  ChocolateyPack(chocolateyPackSettings);
        ///              </code>
        ///              </example>
        ///         
        public static void ChocolateyPack(global::Cake.Common.Tools.Chocolatey.Pack.ChocolateyPackSettings settings)
        {
        }

        ///              <summary>
        ///              Creates Chocolatey packages using the specified Nuspec files.
        ///              </summary>
        ///              
        ///              <param name="filePaths">The nuspec file paths.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var chocolateyPackSettings   = new ChocolateyPackSettings {
        ///                                                  Id                      = "TestChocolatey",
        ///                                                  Title                   = "The tile of the package",
        ///                                                  Version                 = "0.0.0.1",
        ///                                                  Authors                 = new[] {"John Doe"},
        ///                                                  Owners                  = new[] {"Contoso"},
        ///                                                  Summary                 = "Excellent summary of what the package does",
        ///                                                  Description             = "The description of the package",
        ///                                                  ProjectUrl              = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  PackageSourceUrl        = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  ProjectSourceUrl        = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  DocsUrl                 = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  MailingListUrl          = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  BugTrackerUrl           = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  Tags                    = new [] {"Cake", "Script", "Build"},
        ///                                                  Copyright               = "Some company 2015",
        ///                                                  LicenseUrl              = new Uri("https://github.com/SomeUser/TestChocolatey/blob/master/LICENSE.md"),
        ///                                                  RequireLicenseAcceptance= false,
        ///                                                  IconUrl                 = new Uri("http://cdn.rawgit.com/SomeUser/TestChocolatey/master/icons/testchocolatey.png"),
        ///                                                  ReleaseNotes            = new [] {"Bug fixes", "Issue fixes", "Typos"},
        ///                                                  Files                   = new [] {
        ///                                                                                       new ChocolateyNuSpecContent {Source = "bin/TestChocolatey.dll", Target = "bin"},
        ///                                                                                    },
        ///                                                  Debug                   = false,
        ///                                                  Verbose                 = false,
        ///                                                  Force                   = false,
        ///                                                  Noop                    = false,
        ///                                                  LimitOutput             = false,
        ///                                                  ExecutionTimeout        = 13,
        ///                                                  CacheLocation           = @"C:\temp",
        ///                                                  AllowUnofficial          = false
        ///                                              };
        ///             
        ///                  var nuspecFiles = GetFiles("./**/*.nuspec");
        ///                  ChocolateyPack(nuspecFiles, chocolateyPackSettings);
        ///              </code>
        ///              </example>
        ///         
        public static void ChocolateyPack(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> filePaths, global::Cake.Common.Tools.Chocolatey.Pack.ChocolateyPackSettings settings)
        {
        }

        ///              <summary>
        ///              Creates a Chocolatey package using the specified Nuspec file.
        ///              </summary>
        ///              
        ///              <param name="nuspecFilePath">The nuspec file path.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var chocolateyPackSettings   = new ChocolateyPackSettings {
        ///                                                  Id                      = "TestChocolatey",
        ///                                                  Title                   = "The tile of the package",
        ///                                                  Version                 = "0.0.0.1",
        ///                                                  Authors                 = new[] {"John Doe"},
        ///                                                  Owners                  = new[] {"Contoso"},
        ///                                                  Summary                 = "Excellent summary of what the package does",
        ///                                                  Description             = "The description of the package",
        ///                                                  ProjectUrl              = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  PackageSourceUrl        = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  ProjectSourceUrl        = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  DocsUrl                 = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  MailingListUrl          = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  BugTrackerUrl           = new Uri("https://github.com/SomeUser/TestChocolatey/"),
        ///                                                  Tags                    = new [] {"Cake", "Script", "Build"},
        ///                                                  Copyright               = "Some company 2015",
        ///                                                  LicenseUrl              = new Uri("https://github.com/SomeUser/TestChocolatey/blob/master/LICENSE.md"),
        ///                                                  RequireLicenseAcceptance= false,
        ///                                                  IconUrl                 = new Uri("http://cdn.rawgit.com/SomeUser/TestChocolatey/master/icons/testchocolatey.png"),
        ///                                                  ReleaseNotes            = new [] {"Bug fixes", "Issue fixes", "Typos"},
        ///                                                  Files                   = new [] {
        ///                                                                                       new ChocolateyNuSpecContent {Source = "bin/TestChocolatey.dll", Target = "bin"},
        ///                                                                                    },
        ///                                                  Debug                   = false,
        ///                                                  Verbose                 = false,
        ///                                                  Force                   = false,
        ///                                                  Noop                    = false,
        ///                                                  LimitOutput             = false,
        ///                                                  ExecutionTimeout        = 13,
        ///                                                  CacheLocation           = @"C:\temp",
        ///                                                  AllowUnofficial          = false
        ///                                              };
        ///             
        ///                  ChocolateyPack("./nuspec/TestChocolatey.nuspec", chocolateyPackSettings);
        ///              </code>
        ///              </example>
        ///         
        public static void ChocolateyPack(global::Cake.Core.IO.FilePath nuspecFilePath, global::Cake.Common.Tools.Chocolatey.Pack.ChocolateyPackSettings settings)
        {
        }

        ///             <summary>
        ///             Pins a Chocolatey package using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="name">The name.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyPin("MyChocolateyPackage", new ChocolateyPinSettings {
        ///                 Version               = "1.2.3",
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyPin(System.String name, global::Cake.Common.Tools.Chocolatey.Pin.ChocolateyPinSettings settings)
        {
        }

        ///              <summary>
        ///              Pushes Chocolatey packages to a Chocolatey server and publishes them.
        ///              </summary>
        ///              
        ///              <param name="packageFilePaths">The <c>.nupkg</c> file paths.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///              // Get the paths to the packages.
        ///              var packages = GetFiles("./**/*.nupkg");
        ///             
        ///              // Push the package.
        ///              ChocolateyPush(packages, new ChocolateyPushSettings {
        ///                  Source                = "http://example.com/chocolateyfeed",
        ///                  ApiKey                = "4003d786-cc37-4004-bfdf-c4f3e8ef9b3a"
        ///                  Timeout               = 300
        ///                  Debug                 = false,
        ///                  Verbose               = false,
        ///                  Force                 = false,
        ///                  Noop                  = false,
        ///                  LimitOutput           = false,
        ///                  ExecutionTimeout      = 13,
        ///                  CacheLocation         = @"C:\temp",
        ///                  AllowUnofficial        = false
        ///              });
        ///              </code>
        ///              </example>
        ///         
        public static void ChocolateyPush(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> packageFilePaths, global::Cake.Common.Tools.Chocolatey.Push.ChocolateyPushSettings settings)
        {
        }

        ///              <summary>
        ///              Pushes a Chocolatey package to a Chocolatey server and publishes it.
        ///              </summary>
        ///              
        ///              <param name="packageFilePath">The <c>.nupkg</c> file path.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///              // Get the path to the package.
        ///              var package = "./chocolatey/MyChocolateyPackage.0.0.1.nupkg";
        ///             
        ///              // Push the package.
        ///              ChocolateyPush(package, new ChocolateyPushSettings {
        ///                  Source                = "http://example.com/chocolateyfeed",
        ///                  ApiKey                = "4003d786-cc37-4004-bfdf-c4f3e8ef9b3a"
        ///                  Timeout               = 300
        ///                  Debug                 = false,
        ///                  Verbose               = false,
        ///                  Force                 = false,
        ///                  Noop                  = false,
        ///                  LimitOutput           = false,
        ///                  ExecutionTimeout      = 13,
        ///                  CacheLocation         = @"C:\temp",
        ///                  AllowUnofficial        = false
        ///              });
        ///              </code>
        ///              </example>
        ///         
        public static void ChocolateyPush(global::Cake.Core.IO.FilePath packageFilePath, global::Cake.Common.Tools.Chocolatey.Push.ChocolateyPushSettings settings)
        {
        }

        ///             <summary>
        ///             Removes Chocolatey package source using the specified name &amp; source from global user config
        ///             </summary>
        ///             
        ///             <param name="name">Name of the source.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyRemoveSource("MySource");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyRemoveSource(System.String name)
        {
        }

        ///             <summary>
        ///             Removes Chocolatey package source using the specified name, source &amp; settings from global user config
        ///             </summary>
        ///             
        ///             <param name="name">Name of the source.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyRemoveSource("MySource", new ChocolateySourcesSettings {
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyRemoveSource(System.String name, global::Cake.Common.Tools.Chocolatey.Sources.ChocolateySourcesSettings settings)
        {
        }

        ///             <summary>
        ///             Uninstalls a Chocolatey package.
        ///             </summary>
        ///             
        ///             <param name="packageIds">The ids of the packages to uninstall.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyUninstall("MyChocolateyPackage");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyUninstall(global::System.Collections.Generic.IEnumerable<System.String> packageIds)
        {
        }

        ///             <summary>
        ///             Uninstalls a Chocolatey package.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to uninstall.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyUninstall("MyChocolateyPackage");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyUninstall(System.String packageId)
        {
        }

        ///             <summary>
        ///             Uninstalls Chocolatey packages using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="packageIds">The ids of the packages to uninstall.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyUninstall("MyChocolateyPackage", new ChocolateyUninstallSettings {
        ///                 Source                  = true,
        ///                 Version                 = "1.2.3",
        ///                 UninstallArguments      = "arg1",
        ///                 OverrideArguments       = false,
        ///                 NotSilent               = false,
        ///                 PackageParameters       = "param1",
        ///                 SideBySide              = false,
        ///                 IgnoreDependencies      = false,
        ///                 ForceDependencies       = false,
        ///                 SkipPowerShell          = false,
        ///                 Debug                   = false,
        ///                 Verbose                 = false,
        ///                 FailOnStandardError     = false,
        ///                 UseSystemPowershell     = false,
        ///                 AllVersions             = false,
        ///                 Force                   = false,
        ///                 Noop                    = false,
        ///                 LimitOutput             = false,
        ///                 ExecutionTimeout        = 13,
        ///                 CacheLocation           = @"C:\temp",
        ///                 AllowUnofficial         = false,
        ///                 GlobalArguments         = false,
        ///                 GlobalPackageParameters = false,
        ///                 IgnorePackageExitCodes  = false,
        ///                 UsePackageExitCodes     = false,
        ///                 UseAutoUninstaller      = false,
        ///                 SkipAutoUninstaller     = false,
        ///                 FailOnAutoUninstaller   = false,
        ///                 IgnoreAutoUninstaller   = false
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyUninstall(global::System.Collections.Generic.IEnumerable<System.String> packageIds, global::Cake.Common.Tools.Chocolatey.Uninstall.ChocolateyUninstallSettings settings)
        {
        }

        ///             <summary>
        ///             Uninstalls a Chocolatey package using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to uninstall.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyUninstall("MyChocolateyPackage", new ChocolateyUninstallSettings {
        ///                 Source                  = true,
        ///                 Version                 = "1.2.3",
        ///                 UninstallArguments      = "arg1",
        ///                 OverrideArguments       = false,
        ///                 NotSilent               = false,
        ///                 PackageParameters       = "param1",
        ///                 SideBySide              = false,
        ///                 IgnoreDependencies      = false,
        ///                 ForceDependencies       = false,
        ///                 SkipPowerShell          = false,
        ///                 Debug                   = false,
        ///                 Verbose                 = false,
        ///                 FailOnStandardError     = false,
        ///                 UseSystemPowershell     = false,
        ///                 AllVersions             = false,
        ///                 Force                   = false,
        ///                 Noop                    = false,
        ///                 LimitOutput             = false,
        ///                 ExecutionTimeout        = 13,
        ///                 CacheLocation           = @"C:\temp",
        ///                 AllowUnofficial         = false,
        ///                 GlobalArguments         = false,
        ///                 GlobalPackageParameters = false,
        ///                 IgnorePackageExitCodes  = false,
        ///                 UsePackageExitCodes     = false,
        ///                 UseAutoUninstaller      = false,
        ///                 SkipAutoUninstaller     = false,
        ///                 FailOnAutoUninstaller   = false,
        ///                 IgnoreAutoUninstaller   = false
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyUninstall(System.String packageId, global::Cake.Common.Tools.Chocolatey.Uninstall.ChocolateyUninstallSettings settings)
        {
        }

        ///             <summary>
        ///             Upgrades Chocolatey package.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to upgrade.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyUpgrade("MyChocolateyPackage");
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyUpgrade(System.String packageId)
        {
        }

        ///             <summary>
        ///             Upgrades Chocolatey package using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to upgrade.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ChocolateyUpgrade("MyChocolateyPackage", new ChocolateyUpgradeSettings {
        ///                 Source                = true,
        ///                 Version               = "1.2.3",
        ///                 Prerelease            = false,
        ///                 Forcex86              = false,
        ///                 InstallArguments      = "arg1",
        ///                 OverrideArguments     = false,
        ///                 NotSilent             = false,
        ///                 PackageParameters     = "param1",
        ///                 AllowDowngrade        = false,
        ///                 SideBySide            = false,
        ///                 IgnoreDependencies    = false,
        ///                 SkipPowerShell        = false,
        ///                 FailOnUnfound        = false,
        ///                 FailOnNotInstalled        = false,
        ///                 User                  = "user",
        ///                 Password              = "password",
        ///                 IgnoreChecksums       = false,
        ///                 Debug                 = false,
        ///                 Verbose               = false,
        ///                 Force                 = false,
        ///                 Noop                  = false,
        ///                 LimitOutput           = false,
        ///                 ExecutionTimeout      = 13,
        ///                 CacheLocation         = @"C:\temp",
        ///                 AllowUnofficial        = false
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ChocolateyUpgrade(System.String packageId, global::Cake.Common.Tools.Chocolatey.Upgrade.ChocolateyUpgradeSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.DotCover
{
    public static class DotCoverAliasesMetadata
    {
        ///             <summary>
        ///             Runs <see href="https://www.jetbrains.com/dotcover/help/dotCover__Console_Runner_Commands.html#analyse">DotCover Analyse</see>
        ///             for the specified action and settings.
        ///             </summary>
        ///             
        ///             <param name="action">The action to run DotCover for.</param>
        ///             <param name="outputFile">The DotCover output file.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             DotCoverAnalyse(tool =&gt; {
        ///               tool.XUnit2("./**/App.Tests.dll",
        ///                 new XUnit2Settings {
        ///                   ShadowCopy = false
        ///                 });
        ///               },
        ///               new FilePath("./result.xml"),
        ///               new DotCoverAnalyseSettings()
        ///                 .WithFilter("+:App")
        ///                 .WithFilter("-:App.Tests"));
        ///             </code>
        ///             </example>
        ///         
        public static void DotCoverAnalyse(global::System.Action<global::Cake.Core.ICakeContext> action, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.DotCover.Analyse.DotCoverAnalyseSettings settings)
        {
        }

        ///             <summary>
        ///             Runs <see href="https://www.jetbrains.com/dotcover/help/dotCover__Console_Runner_Commands.html#cover">DotCover Cover</see>
        ///             for the specified action and settings.
        ///             </summary>
        ///             
        ///             <param name="action">The action to run DotCover for.</param>
        ///             <param name="outputFile">The DotCover output file.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             DotCoverCover(tool =&gt; {
        ///               tool.XUnit2("./**/App.Tests.dll",
        ///                 new XUnit2Settings {
        ///                   ShadowCopy = false
        ///                 });
        ///               },
        ///               new FilePath("./result.dcvr"),
        ///               new DotCoverCoverSettings()
        ///                 .WithFilter("+:App")
        ///                 .WithFilter("-:App.Tests"));
        ///             </code>
        ///             </example>
        ///         
        public static void DotCoverCover(global::System.Action<global::Cake.Core.ICakeContext> action, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.DotCover.Cover.DotCoverCoverSettings settings)
        {
        }

        ///             <summary>
        ///             Runs <see href="https://www.jetbrains.com/dotcover/help/dotCover__Console_Runner_Commands.html#merge">DotCover Merge</see>
        ///             for the specified action and settings.
        ///             </summary>
        ///             
        ///             <param name="sourceFiles">The list of DotCover coverage snapshot files.</param>
        ///             <param name="outputFile">The merged output file.</param>
        ///             <example>
        ///             <code>
        ///             DotCoverMerge(new[] {
        ///                 new FilePath("./result1.dcvr"),
        ///                 new FilePath("./result2.dcvr")
        ///               },
        ///               new FilePath("./merged.dcvr"));
        ///             </code>
        ///             </example>
        ///         
        public static void DotCoverMerge(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> sourceFiles, global::Cake.Core.IO.FilePath outputFile)
        {
        }

        ///             <summary>
        ///             Runs <see href="https://www.jetbrains.com/dotcover/help/dotCover__Console_Runner_Commands.html#merge">DotCover Merge</see>
        ///             for the specified action and settings.
        ///             </summary>
        ///             
        ///             <param name="sourceFiles">The list of DotCover coverage snapshot files.</param>
        ///             <param name="outputFile">The merged output file.</param>
        ///             <param name="settings">The settings</param>
        ///             <example>
        ///             <code>
        ///             DotCoverMerge(new[] {
        ///                 new FilePath("./result1.dcvr"),
        ///                 new FilePath("./result2.dcvr")
        ///               },
        ///               new FilePath("./merged.dcvr"),
        ///               new DotCoverMergeSettings {
        ///                 LogFile = new FilePath("./log.txt")
        ///               });
        ///             </code>
        ///             </example>
        ///         
        public static void DotCoverMerge(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> sourceFiles, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.DotCover.Merge.DotCoverMergeSettings settings)
        {
        }

        ///             <summary>
        ///             Runs <see href="https://www.jetbrains.com/dotcover/help/dotCover__Console_Runner_Commands.html#report">DotCover Report</see>
        ///             for the specified action and settings.
        ///             </summary>
        ///             
        ///             <param name="sourceFile">The DotCover coverage snapshot file name.</param>
        ///             <param name="outputFile">The DotCover output file.</param>
        ///             <param name="settings">The settings</param>
        ///             <example>
        ///             <code>
        ///             DotCoverReport(new FilePath("./result.dcvr"),
        ///               new FilePath("./result.html"),
        ///               new DotCoverReportSettings {
        ///                 ReportType = DotCoverReportType.HTML
        ///               });
        ///             </code>
        ///             </example>
        ///         
        public static void DotCoverReport(global::Cake.Core.IO.FilePath sourceFile, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.DotCover.Report.DotCoverReportSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.DotNetCore
{
    public static class DotNetCoreAliasesMetadata
    {
        ///             <summary>
        ///             Build all projects.
        ///             </summary>
        ///             
        ///             <param name="project">The projects path.</param>
        ///             <example>
        ///             <code>
        ///                 DotNetCoreBuild("./src/*");
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCoreBuild(System.String project)
        {
        }

        ///              <summary>
        ///              Build all projects.
        ///              </summary>
        ///              
        ///              <param name="project">The projects path.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var settings = new DotNetCoreBuildSettings
        ///                  {
        ///                      Framework = "netcoreapp1.0",
        ///                      Configuration = "Debug",
        ///                      OutputDirectory = "./artifacts/"
        ///                  };
        ///             
        ///                  DotNetCoreBuild("./src/*", settings);
        ///              </code>
        ///              </example>
        ///         
        public static void DotNetCoreBuild(System.String project, global::Cake.Common.Tools.DotNetCore.Build.DotNetCoreBuildSettings settings)
        {
        }

        ///             <summary>
        ///             Execute an assembly.
        ///             </summary>
        ///             
        ///             <param name="assemblyPath">The assembly path.</param>
        ///             <example>
        ///             <code>
        ///                 DotNetCoreExecute("./bin/Debug/app.dll");
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCoreExecute(global::Cake.Core.IO.FilePath assemblyPath)
        {
        }

        ///             <summary>
        ///             Execute an assembly with arguments in the specific path.
        ///             </summary>
        ///             
        ///             <param name="assemblyPath">The assembly path.</param>
        ///             <param name="arguments">The arguments.</param>
        ///             <example>
        ///             <code>
        ///                 DotNetCoreExecute("./bin/Debug/app.dll", "--arg");
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCoreExecute(global::Cake.Core.IO.FilePath assemblyPath, global::Cake.Core.IO.ProcessArgumentBuilder arguments)
        {
        }

        ///              <summary>
        ///              Execute an assembly with arguments in the specific path with settings.
        ///              </summary>
        ///              
        ///              <param name="assemblyPath">The assembly path.</param>
        ///              <param name="arguments">The arguments.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var settings = new DotNetCoreSettings
        ///                  {
        ///                      Verbose = true
        ///                  };
        ///             
        ///                  DotNetCoreExecute("./bin/Debug/app.dll", "--arg", settings);
        ///              </code>
        ///              </example>
        ///         
        public static void DotNetCoreExecute(global::Cake.Core.IO.FilePath assemblyPath, global::Cake.Core.IO.ProcessArgumentBuilder arguments, global::Cake.Common.Tools.DotNetCore.DotNetCoreSettings settings)
        {
        }

        ///             <summary>
        ///             Package all projects.
        ///             </summary>
        ///             
        ///             <param name="project">The projects path.</param>
        ///             <example>
        ///             <code>
        ///                 DotNetCorePack("./src/*");
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCorePack(System.String project)
        {
        }

        ///              <summary>
        ///              Package all projects.
        ///              </summary>
        ///              
        ///              <param name="project">The projects path.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var settings = new DotNetCorePackSettings
        ///                  {
        ///                      Configuration = "Release",
        ///                      OutputDirectory = "./artifacts/"
        ///                  };
        ///             
        ///                  DotNetCorePack("./src/*", settings);
        ///              </code>
        ///              </example>
        ///         
        public static void DotNetCorePack(System.String project, global::Cake.Common.Tools.DotNetCore.Pack.DotNetCorePackSettings settings)
        {
        }

        ///             <summary>
        ///             Publish all projects.
        ///             </summary>
        ///             
        ///             <param name="project">The projects path.</param>
        ///             <example>
        ///             <code>
        ///                 DotNetCorePublish("./src/*");
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCorePublish(System.String project)
        {
        }

        ///              <summary>
        ///              Publish all projects.
        ///              </summary>
        ///              
        ///              <param name="project">The projects path.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var settings = new DotNetCorePublishSettings
        ///                  {
        ///                      Framework = "netcoreapp1.0",
        ///                      Configuration = "Release",
        ///                      OutputDirectory = "./artifacts/"
        ///                  };
        ///             
        ///                  DotNetCorePublish("./src/*", settings);
        ///              </code>
        ///              </example>
        ///         
        public static void DotNetCorePublish(System.String project, global::Cake.Common.Tools.DotNetCore.Publish.DotNetCorePublishSettings settings)
        {
        }

        ///             <summary>
        ///             Restore all NuGet Packages.
        ///             </summary>
        ///             
        ///             <example>
        ///             <code>
        ///                 DotNetCoreRestore();
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCoreRestore()
        {
        }

        ///              <summary>
        ///              Restore all NuGet Packages with the settings.
        ///              </summary>
        ///              
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var settings = new DotNetCoreRestoreSettings
        ///                  {
        ///                      Sources = new[] {"https://www.example.com/nugetfeed", "https://www.example.com/nugetfeed2"},
        ///                      FallbackSources = new[] {"https://www.example.com/fallbacknugetfeed"},
        ///                      PackagesDirectory = "./packages",
        ///                      Verbosity = Information,
        ///                      DisableParallel = true,
        ///                      InferRuntimes = new[] {"runtime1", "runtime2"}
        ///                  };
        ///             
        ///                  DotNetCoreRestore(settings);
        ///              </code>
        ///              </example>
        ///         
        public static void DotNetCoreRestore(global::Cake.Common.Tools.DotNetCore.Restore.DotNetCoreRestoreSettings settings)
        {
        }

        ///             <summary>
        ///             Restore all NuGet Packages in the specified path.
        ///             </summary>
        ///             
        ///             <param name="root">List of projects and project folders to restore. Each value can be: a path to a project.json or global.json file, or a folder to recursively search for project.json files.</param>
        ///             <example>
        ///             <code>
        ///                 DotNetCoreRestore("./src/*");
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCoreRestore(System.String root)
        {
        }

        ///              <summary>
        ///              Restore all NuGet Packages in the specified path with settings.
        ///              </summary>
        ///              
        ///              <param name="root">List of projects and project folders to restore. Each value can be: a path to a project.json or global.json file, or a folder to recursively search for project.json files.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var settings = new DotNetCoreRestoreSettings
        ///                  {
        ///                      Sources = new[] {"https://www.example.com/nugetfeed", "https://www.example.com/nugetfeed2"},
        ///                      FallbackSources = new[] {"https://www.example.com/fallbacknugetfeed"},
        ///                      PackagesDirectory = "./packages",
        ///                      Verbosity = Information,
        ///                      DisableParallel = true,
        ///                      InferRuntimes = new[] {"runtime1", "runtime2"}
        ///                  };
        ///             
        ///                  DotNetCoreRestore("./src/*", settings);
        ///              </code>
        ///              </example>
        ///         
        public static void DotNetCoreRestore(System.String root, global::Cake.Common.Tools.DotNetCore.Restore.DotNetCoreRestoreSettings settings)
        {
        }

        ///             <summary>
        ///             Run all projects.
        ///             </summary>
        ///             
        ///             <example>
        ///             <code>
        ///                 DotNetCoreRun();
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCoreRun()
        {
        }

        ///             <summary>
        ///             Run project.
        ///             </summary>
        ///             
        ///             <param name="project">The project path.</param>
        ///             <example>
        ///             <code>
        ///                 DotNetCoreRun("./src/Project");
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCoreRun(System.String project)
        {
        }

        ///             <summary>
        ///             Run project with path and arguments.
        ///             </summary>
        ///             
        ///             <param name="project">The project path.</param>
        ///             <param name="arguments">The arguments.</param>
        ///             <example>
        ///             <code>
        ///                 DotNetCoreRun("./src/Project", "--args");
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCoreRun(System.String project, global::Cake.Core.IO.ProcessArgumentBuilder arguments)
        {
        }

        ///              <summary>
        ///              Run project with settings.
        ///              </summary>
        ///              
        ///              <param name="project">The project path.</param>
        ///              <param name="arguments">The arguments.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var settings = new DotNetCoreRunSettings
        ///                  {
        ///                      Framework = "netcoreapp1.0",
        ///                      Configuration = "Release"
        ///                  };
        ///             
        ///                  DotNetCoreRun("./src/Project", "--args", settings);
        ///              </code>
        ///              </example>
        ///         
        public static void DotNetCoreRun(System.String project, global::Cake.Core.IO.ProcessArgumentBuilder arguments, global::Cake.Common.Tools.DotNetCore.Run.DotNetCoreRunSettings settings)
        {
        }

        ///             <summary>
        ///             Test project.
        ///             </summary>
        ///             
        ///             <example>
        ///             <code>
        ///                 DotNetCoreTest();
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCoreTest()
        {
        }

        ///             <summary>
        ///             Test project with path.
        ///             </summary>
        ///             
        ///             <param name="project">The project path.</param>
        ///             <example>
        ///             <code>
        ///                 DotNetCoreTest("./src/Project");
        ///             </code>
        ///             </example>
        ///         
        public static void DotNetCoreTest(System.String project)
        {
        }

        ///              <summary>
        ///              Test project with settings.
        ///              </summary>
        ///              
        ///              <param name="project">The project path.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var settings = new DotNetCoreTestSettings
        ///                  {
        ///                      Configuration = "Release"
        ///                  };
        ///             
        ///                  DotNetCoreTest("./test/Project.Tests", settings);
        ///              </code>
        ///              </example>
        ///         
        public static void DotNetCoreTest(System.String project, global::Cake.Common.Tools.DotNetCore.Test.DotNetCoreTestSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.DupFinder
{
    public static class DupFinderAliasesMetadata
    {
        ///             <summary>
        ///             Analyses the specified projects with ReSharper's DupFinder.
        ///             The files can either be solutions and projects or a source files.
        ///             </summary>
        ///             
        ///             <param name="files">The files to analyze.</param>
        ///             <example>
        ///             <code>
        ///             var projects = GetFiles("./src/**/*.csproj");
        ///             DupFinder(projects);
        ///             </code>
        ///             </example>
        ///         
        public static void DupFinder(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> files)
        {
        }

        ///             <summary>
        ///             Analyses all files matching the specified pattern with ReSharper's DupFinder.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <example>
        ///             <code>
        ///             DupFinder("*.cs");
        ///             </code>
        ///             </example>
        ///         
        public static void DupFinder(System.String pattern)
        {
        }

        ///             <summary>
        ///             Analyses the specified file with ReSharper's DupFinder.
        ///             The file can either be a solution/project or a source file.
        ///             </summary>
        ///             
        ///             <param name="file">The file to analyze.</param>
        ///             <example>
        ///             <code>
        ///             DupFinder("./src/MySolution.sln");
        ///             </code>
        ///             </example>
        ///         
        public static void DupFinder(global::Cake.Core.IO.FilePath file)
        {
        }

        ///              <summary>
        ///              Analyses all files matching the specified pattern with ReSharper's DupFinder,
        ///              using the specified settings.
        ///              </summary>
        ///              
        ///              <param name="pattern">The pattern.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///              var buildOutputDirectory = Directory("./.build");
        ///              var resharperReportsDirectory = buildOutputDirectory + Directory("_ReSharperReports");
        ///             
        ///              DupFinder("*.cs", new DupFinderSettings {
        ///                  OutputFile = resharperReportsDirectory + File("dupfinder-output.xml"),
        ///              });
        ///              </code>
        ///              </example>
        ///         
        public static void DupFinder(System.String pattern, global::Cake.Common.Tools.DupFinder.DupFinderSettings settings)
        {
        }

        ///              <summary>
        ///              Analyses the specified projects with ReSharper's DupFinder using the specified settings.
        ///              The files can either be solutions and projects or a source files.
        ///              </summary>
        ///              
        ///              <param name="files">The files to analyze.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///              var buildOutputDirectory = Directory("./.build");
        ///              var resharperReportsDirectory = buildOutputDirectory + Directory("_ReSharperReports");
        ///              var rootDirectoryPath = MakeAbsolute(Context.Environment.WorkingDirectory);
        ///             
        ///              var projects = GetFiles("./src/**/*.csproj");
        ///              DupFinder(projects, new DupFinderSettings {
        ///                  ShowStats = true,
        ///                  ShowText = true,
        ///                  ExcludePattern = new String[]
        ///                  {
        ///                      rootDirectoryPath + "/**/*Designer.cs",
        ///                  },
        ///                  OutputFile = resharperReportsDirectory + File("dupfinder-output.xml"),
        ///                  ThrowExceptionOnFindingDuplicates = true
        ///              });
        ///              </code>
        ///              </example>
        ///         
        public static void DupFinder(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> files, global::Cake.Common.Tools.DupFinder.DupFinderSettings settings)
        {
        }

        ///              <summary>
        ///              Analyses the specified file with ReSharper's DupFinder using the specified settings.
        ///              The file can either be a solution/project or a source file.
        ///              </summary>
        ///              
        ///              <param name="file">The file to analyze.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///              var buildOutputDirectory = Directory("./.build");
        ///              var resharperReportsDirectory = buildOutputDirectory + Directory("_ReSharperReports");
        ///              var rootDirectoryPath = MakeAbsolute(Context.Environment.WorkingDirectory);
        ///             
        ///              DupFinder("./src/MySolution.sln", new DupFinderSettings {
        ///                  ShowStats = true,
        ///                  ShowText = true,
        ///                  ExcludePattern = new String[]
        ///                  {
        ///                      rootDirectoryPath + "/**/*Designer.cs",
        ///                  },
        ///                  OutputFile = resharperReportsDirectory + File("dupfinder-output.xml"),
        ///                  ThrowExceptionOnFindingDuplicates = true
        ///              });
        ///              </code>
        ///              </example>
        ///         
        public static void DupFinder(global::Cake.Core.IO.FilePath file, global::Cake.Common.Tools.DupFinder.DupFinderSettings settings)
        {
        }

        ///             <summary>
        ///             Runs ReSharper's DupFinder using the provided config file.
        ///             </summary>
        ///             
        ///             <param name="configFile">The config file.</param>
        ///             <example>
        ///             <code>
        ///             DupFinderFromConfig("./src/dupfinder.config");
        ///             </code>
        ///             </example>
        ///         
        public static void DupFinderFromConfig(global::Cake.Core.IO.FilePath configFile)
        {
        }
    }
}

namespace Cake.Common.Tools.Fixie
{
    public static class FixieAliasesMetadata
    {
        ///             <summary>
        ///             Runs all Fixie tests in the specified assemblies.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var assemblies = new [] {
        ///                 "UnitTests1.dll",
        ///                 "UnitTests2.dll"
        ///             };
        ///             Fixie(assemblies);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///         
        public static void Fixie(global::System.Collections.Generic.IEnumerable<System.String> assemblies)
        {
        }

        ///             <summary>
        ///             Runs all Fixie tests in the specified assemblies.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var assemblies = GetFiles("./src/UnitTests/*.dll");
        ///             Fixie(assemblies);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///         
        public static void Fixie(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies)
        {
        }

        ///             <summary>
        ///             Runs all Fixie tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             Fixie("./src/UnitTests/*.dll");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///         
        public static void Fixie(System.String pattern)
        {
        }

        ///             <summary>
        ///             Runs all Fixie tests in the specified assemblies,
        ///             using the specified settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var assemblies = GetFiles("./src/UnitTests/*.dll");
        ///             Fixie(assemblies, new FixieSettings {
        ///                 NUnitXml = TestResult.xml
        ///                 });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void Fixie(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies, global::Cake.Common.Tools.Fixie.FixieSettings settings)
        {
        }

        ///             <summary>
        ///             Runs all Fixie tests in the specified assemblies,
        ///             using the specified settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var assemblies = new [] {
        ///                 "UnitTests1.dll",
        ///                 "UnitTests2.dll"
        ///             };
        ///             Fixie(assemblies, new FixieSettings {
        ///                 NUnitXml = TestResult.xml
        ///                 });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void Fixie(global::System.Collections.Generic.IEnumerable<System.String> assemblies, global::Cake.Common.Tools.Fixie.FixieSettings settings)
        {
        }

        ///             <summary>
        ///             Runs all Fixie tests in the assemblies matching the specified pattern,
        ///             using the specified settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             Fixie("./src/UnitTests/*.dll", new FixieSettings {
        ///                 NUnitXml = TestResult.xml
        ///                 });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void Fixie(System.String pattern, global::Cake.Common.Tools.Fixie.FixieSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.GitLink
{
    public static class GitLinkAliasesMetadata
    {
        ///             <summary>
        ///             Update pdb files to link all sources.
        ///             This will allow anyone to step through the source code while debugging without a symbol source server.
        ///             </summary>
        ///             
        ///             <param name="repositoryRootPath">The Solution File to analyze.</param>
        ///             <example>
        ///             <code>
        ///             GitLink("C:/temp/solution");
        ///             </code>
        ///             </example>
        ///         
        public static void GitLink(global::Cake.Core.IO.DirectoryPath repositoryRootPath)
        {
        }

        ///             <summary>
        ///             Update pdb files to link all sources, using specified settings.
        ///             This will allow anyone to step through the source code while debugging without a symbol source server.
        ///             </summary>
        ///             
        ///             <param name="repositoryRootPath">The path to the Root of the Repository to analyze.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             GitLink("C:/temp/solution", new GitLinkSettings {
        ///                 RepositoryUrl = "http://mydomain.com",
        ///                 Branch        = "master",
        ///                 ShaHash       = "abcdef",
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void GitLink(global::Cake.Core.IO.DirectoryPath repositoryRootPath, global::Cake.Common.Tools.GitLink.GitLinkSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.GitReleaseManager
{
    public static class GitReleaseManagerAliasesMetadata
    {
        ///             <summary>
        ///             Add Assets to an existing release.
        ///             </summary>
        ///             
        ///             <param name="userName">The user name.</param>
        ///             <param name="password">The password.</param>
        ///             <param name="owner">The owner.</param>
        ///             <param name="repository">The repository.</param>
        ///             <param name="tagName">The tag name.</param>
        ///             <param name="assets">The assets.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerAddAssets("user", "password", "owner", "repo", "0.1.0", "c:/temp/asset1.txt,c:/temp/asset2.txt");
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseManagerAddAssets(System.String userName, System.String password, System.String owner, System.String repository, System.String tagName, System.String assets)
        {
        }

        ///             <summary>
        ///             Add Assets to an existing release using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="userName">The user name.</param>
        ///             <param name="password">The password.</param>
        ///             <param name="owner">The owner.</param>
        ///             <param name="repository">The repository.</param>
        ///             <param name="tagName">The tag name.</param>
        ///             <param name="assets">The assets.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerAddAssets("user", "password", "owner", "repo", "0.1.0", "c:/temp/asset1.txt,c:/temp/asset2.txt" new GitReleaseManagerAddAssetsSettings {
        ///                 TargetDirectory   = "c:/repo",
        ///                 LogFilePath       = "c:/temp/grm.log"
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseManagerAddAssets(System.String userName, System.String password, System.String owner, System.String repository, System.String tagName, System.String assets, global::Cake.Common.Tools.GitReleaseManager.AddAssets.GitReleaseManagerAddAssetsSettings settings)
        {
        }

        ///             <summary>
        ///             Closes the milestone associated with a release.
        ///             </summary>
        ///             
        ///             <param name="userName">The user name.</param>
        ///             <param name="password">The password.</param>
        ///             <param name="owner">The owner.</param>
        ///             <param name="repository">The repository.</param>
        ///             <param name="milestone">The milestone.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerClose("user", "password", "owner", "repo", "0.1.0");
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseManagerClose(System.String userName, System.String password, System.String owner, System.String repository, System.String milestone)
        {
        }

        ///             <summary>
        ///             Closes the milestone associated with a release using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="userName">The user name.</param>
        ///             <param name="password">The password.</param>
        ///             <param name="owner">The owner.</param>
        ///             <param name="repository">The repository.</param>
        ///             <param name="milestone">The milestone.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerClose("user", "password", "owner", "repo", "0.1.0", new GitReleaseManagerCloseMilestoneSettings {
        ///                 TargetDirectory   = "c:/repo",
        ///                 LogFilePath       = "c:/temp/grm.log"
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseManagerClose(System.String userName, System.String password, System.String owner, System.String repository, System.String milestone, global::Cake.Common.Tools.GitReleaseManager.Close.GitReleaseManagerCloseMilestoneSettings settings)
        {
        }

        ///             <summary>
        ///             Creates a Package Release.
        ///             </summary>
        ///             
        ///             <param name="userName">The user name.</param>
        ///             <param name="password">The password.</param>
        ///             <param name="owner">The owner.</param>
        ///             <param name="repository">The repository.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerCreate("user", "password", "owner", "repo");
        ///             </code>
        ///             </example>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerCreate("user", "password", "owner", "repo");
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseManagerCreate(System.String userName, System.String password, System.String owner, System.String repository)
        {
        }

        ///             <summary>
        ///             Creates a Package Release using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="userName">The user name.</param>
        ///             <param name="password">The password.</param>
        ///             <param name="owner">The owner.</param>
        ///             <param name="repository">The repository.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerCreate("user", "password", "owner", "repo", new GitReleaseManagerCreateSettings {
        ///                 Milestone         = "0.1.0",
        ///                 Prerelease        = false,
        ///                 Assets            = "c:/temp/asset1.txt,c:/temp/asset2.txt",
        ///                 TargetCommitish   = "master",
        ///                 TargetDirectory   = "c:/repo",
        ///                 LogFilePath       = "c:/temp/grm.log"
        ///             });
        ///             </code>
        ///             </example>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerCreate("user", "password", "owner", "repo", new GitReleaseManagerCreateSettings {
        ///                 Name              = "0.1.0",
        ///                 InputFilePath     = "c:/repo/releasenotes.md",
        ///                 Prerelease        = false,
        ///                 Assets            = "c:/temp/asset1.txt,c:/temp/asset2.txt",
        ///                 TargetCommitish   = "master",
        ///                 TargetDirectory   = "c:/repo",
        ///                 LogFilePath       = "c:/temp/grm.log"
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseManagerCreate(System.String userName, System.String password, System.String owner, System.String repository, global::Cake.Common.Tools.GitReleaseManager.Create.GitReleaseManagerCreateSettings settings)
        {
        }

        ///             <summary>
        ///             Exports the release notes.
        ///             </summary>
        ///             
        ///             <param name="userName">The user name.</param>
        ///             <param name="password">The password.</param>
        ///             <param name="owner">The owner.</param>
        ///             <param name="repository">The repository.</param>
        ///             <param name="fileOutputPath">The output file path.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerExport("user", "password", "owner", "repo", "c:/temp/releasenotes.md")
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseManagerExport(System.String userName, System.String password, System.String owner, System.String repository, global::Cake.Core.IO.FilePath fileOutputPath)
        {
        }

        ///             <summary>
        ///             Exports the release notes using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="userName">The user name.</param>
        ///             <param name="password">The password.</param>
        ///             <param name="owner">The owner.</param>
        ///             <param name="repository">The repository.</param>
        ///             <param name="fileOutputPath">The output file path.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerExport("user", "password", "owner", "repo", "c:/temp/releasenotes.md", new GitReleaseManagerExportSettings {
        ///                 TagName           = "0.1.0",
        ///                 TargetDirectory   = "c:/repo",
        ///                 LogFilePath       = "c:/temp/grm.log"
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseManagerExport(System.String userName, System.String password, System.String owner, System.String repository, global::Cake.Core.IO.FilePath fileOutputPath, global::Cake.Common.Tools.GitReleaseManager.Export.GitReleaseManagerExportSettings settings)
        {
        }

        ///             <summary>
        ///             Publishes the release.
        ///             </summary>
        ///             
        ///             <param name="userName">The user name.</param>
        ///             <param name="password">The password.</param>
        ///             <param name="owner">The owner.</param>
        ///             <param name="repository">The repository.</param>
        ///             <param name="tagName">The tag name.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerPublish("user", "password", "owner", "repo", "0.1.0");
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseManagerPublish(System.String userName, System.String password, System.String owner, System.String repository, System.String tagName)
        {
        }

        ///             <summary>
        ///             Publishes the release using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="userName">The user name.</param>
        ///             <param name="password">The password.</param>
        ///             <param name="owner">The owner.</param>
        ///             <param name="repository">The repository.</param>
        ///             <param name="tagName">The tag name.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseManagerPublish("user", "password", "owner", "repo", "0.1.0", new GitReleaseManagerPublishSettings {
        ///                 TargetDirectory   = "c:/repo",
        ///                 LogFilePath       = "c:/temp/grm.log"
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseManagerPublish(System.String userName, System.String password, System.String owner, System.String repository, System.String tagName, global::Cake.Common.Tools.GitReleaseManager.Publish.GitReleaseManagerPublishSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.GitReleaseNotes
{
    public static class GitReleaseNotesAliasesMetadata
    {
        ///             <summary>
        ///             Generates a set of release notes based on the commit history of the repository and specified settings.
        ///             </summary>
        ///             
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             GitReleaseNotes("c:/temp/releasenotes.md", new GitReleaseNotesSettings {
        ///                 WorkingDirectory         = "c:/temp",
        ///                 Verbose                  = true,
        ///                 IssueTracker             = IssueTracker.GitHub,
        ///                 AllTags                  = true,
        ///                 RepoUserName             = "bob",
        ///                 RepoPassword             = "password",
        ///                 RepoUrl                  = "http://myrepo.co.uk",
        ///                 RepoBranch               = "master",
        ///                 IssueTrackerUrl          = "http://myissuetracker.co.uk",
        ///                 IssueTrackerUserName     = "bob",
        ///                 IssueTrackerPassword     = "password",
        ///                 IssueTrackerProjectId    = "1234",
        ///                 Categories               = "Category1",
        ///                 Version                  = "1.2.3.4",
        ///                 AllLabels                = true
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void GitReleaseNotes(global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.GitReleaseNotes.GitReleaseNotesSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.GitVersion
{
    public static class GitVersionAliasesMetadata
    {
        ///             <summary>
        ///             Retrieves the GitVersion output.
        ///             </summary>
        ///             
        ///             <returns>The git version info.</returns>
        ///             <example>
        ///             <para>Update the assembly info files for the project.</para>
        ///             <para>Cake task:</para>
        ///             <code>
        ///             <![CDATA[
        ///             Task("UpdateAssemblyInfo")
        ///                 .Does(() =>
        ///             {
        ///                 GitVersion(new GitVersionSettings {
        ///                     UpdateAssemblyInfo = true
        ///                 });
        ///             });
        ///             ]]>
        ///             </code>
        ///             <para>Get the git version info for the project using a dynamic repository.</para>
        ///             <para>Cake task:</para>
        ///             <code>
        ///             <![CDATA[
        ///             Task("GetVersionInfo")
        ///                 .Does(() =>
        ///             {
        ///                 var result = GitVersion(new GitVersionSettings {
        ///                     UserName = "MyUser",
        ///                     Password = "MyPassword,
        ///                     Url = "http://git.myhost.com/myproject.git"
        ///                     Branch = "develop"
        ///                     Commit = EnviromentVariable("MY_COMMIT")
        ///                 });
        ///                 // Use result for building nuget packages, setting build server version, etc...
        ///             });
        ///             ]]>
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Tools.GitVersion.GitVersion GitVersion()
        {
            return default(global::Cake.Common.Tools.GitVersion.GitVersion);
        }

        ///             <summary>
        ///             Retrieves the GitVersion output.
        ///             </summary>
        ///             
        ///             <param name="settings">The GitVersion settings.</param>
        ///             <returns>The git version info.</returns>
        ///             <example>
        ///             <para>Update the assembly info files for the project.</para>
        ///             <para>Cake task:</para>
        ///             <code>
        ///             <![CDATA[
        ///             Task("UpdateAssemblyInfo")
        ///                 .Does(() =>
        ///             {
        ///                 GitVersion(new GitVersionSettings {
        ///                     UpdateAssemblyInfo = true
        ///                 });
        ///             });
        ///             ]]>
        ///             </code>
        ///             <para>Get the git version info for the project using a dynamic repository.</para>
        ///             <para>Cake task:</para>
        ///             <code>
        ///             <![CDATA[
        ///             Task("GetVersionInfo")
        ///                 .Does(() =>
        ///             {
        ///                 var result = GitVersion(new GitVersionSettings {
        ///                     UserName = "MyUser",
        ///                     Password = "MyPassword,
        ///                     Url = "http://git.myhost.com/myproject.git"
        ///                     Branch = "develop"
        ///                     Commit = EnviromentVariable("MY_COMMIT")
        ///                 });
        ///                 // Use result for building nuget packages, setting build server version, etc...
        ///             });
        ///             ]]>
        ///             </code>
        ///             </example>
        ///         
        public static global::Cake.Common.Tools.GitVersion.GitVersion GitVersion(global::Cake.Common.Tools.GitVersion.GitVersionSettings settings)
        {
            return default(global::Cake.Common.Tools.GitVersion.GitVersion);
        }
    }
}

namespace Cake.Common.Tools.ILMerge
{
    public static class ILMergeAliasesMetadata
    {
        ///             <summary>
        ///             Merges the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="primaryAssembly">The primary assembly.</param>
        ///             <param name="assemblyPaths">The assembly paths.</param>
        ///             <example>
        ///             <code>
        ///             var assemblyPaths = GetFiles("./**/Cake.*.dll");
        ///             ILMerge("./MergedCake.exe", "./Cake.exe", assemblyPaths);
        ///             </code>
        ///             </example>
        ///         
        public static void ILMerge(global::Cake.Core.IO.FilePath outputFile, global::Cake.Core.IO.FilePath primaryAssembly, global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblyPaths)
        {
        }

        ///             <summary>
        ///             Merges the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="primaryAssembly">The primary assembly.</param>
        ///             <param name="assemblyPaths">The assembly paths.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             var assemblyPaths = GetFiles("./**/Cake.*.dll");
        ///             ILMerge(
        ///                 "./MergedCake.exe",
        ///                 "./Cake.exe",
        ///                 assemblyPaths,
        ///                 new ILMergeSettings { Internalize = true });
        ///             </code>
        ///             </example>
        ///         
        public static void ILMerge(global::Cake.Core.IO.FilePath outputFile, global::Cake.Core.IO.FilePath primaryAssembly, global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblyPaths, global::Cake.Common.Tools.ILMerge.ILMergeSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.ILRepack
{
    public static class ILRepackAliasesMetadata
    {
        ///             <summary>
        ///             Merges the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="primaryAssembly">The primary assembly.</param>
        ///             <param name="assemblyPaths">The assembly paths.</param>
        ///             <example>
        ///             <code>
        ///             var assemblyPaths = GetFiles("./**/Cake.*.dll");
        ///             ILRepack("./MergedCake.exe", "./Cake.exe", assemblyPaths);
        ///             </code>
        ///             </example>
        ///         
        public static void ILRepack(global::Cake.Core.IO.FilePath outputFile, global::Cake.Core.IO.FilePath primaryAssembly, global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblyPaths)
        {
        }

        ///             <summary>
        ///             Merges the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="primaryAssembly">The primary assembly.</param>
        ///             <param name="assemblyPaths">The assembly paths.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             var assemblyPaths = GetFiles("./**/Cake.*.dll");
        ///             ILRepack(
        ///                 "./MergedCake.exe",
        ///                 "./Cake.exe",
        ///                 assemblyPaths,
        ///                 new ILRepackSettings { Internalize = true });
        ///             </code>
        ///             </example>
        ///         
        public static void ILRepack(global::Cake.Core.IO.FilePath outputFile, global::Cake.Core.IO.FilePath primaryAssembly, global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblyPaths, global::Cake.Common.Tools.ILRepack.ILRepackSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.InnoSetup
{
    public static class InnoSetupAliasesMetadata
    {
        ///             <summary>
        ///             Compiles the given Inno Setup script using the default settings.
        ///             </summary>
        ///             
        ///             <param name="scriptFile">The path to the <c>.iss</c> script file to compile.</param>
        ///             <example>
        ///             <code>
        ///             InnoSetup("./src/Cake.iss");
        ///             </code>
        ///             </example>
        ///         
        public static void InnoSetup(global::Cake.Core.IO.FilePath scriptFile)
        {
        }

        ///             <summary>
        ///             Compiles the given Inno Setup script using the given <paramref name="settings" />.
        ///             </summary>
        ///             
        ///             <param name="scriptFile">The path to the <c>.iss</c> script file to compile.</param>
        ///             <param name="settings">The <see cref="T:Cake.Common.Tools.InnoSetup.InnoSetupSettings" /> to use.</param>
        ///             <example>
        ///             <code>
        ///             InnoSetup("./src/Cake.iss", new InnoSetupSettings {
        ///                 OutputDir = outputDirectory
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void InnoSetup(global::Cake.Core.IO.FilePath scriptFile, global::Cake.Common.Tools.InnoSetup.InnoSetupSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.InspectCode
{
    public static class InspectCodeAliasesMetadata
    {
        ///             <summary>
        ///             Analyses the specified solution with Resharper's InspectCode.
        ///             </summary>
        ///             
        ///             <param name="solution">The solution.</param>
        ///             <example>
        ///             <code>
        ///             InspectCode("./src/MySolution.sln");
        ///             </code>
        ///             </example>
        ///         
        public static void InspectCode(global::Cake.Core.IO.FilePath solution)
        {
        }

        ///              <summary>
        ///              Analyses the specified solution with Resharper's InspectCode,
        ///              using the specified settings.
        ///              </summary>
        ///              
        ///              <param name="solution">The solution.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///              var buildOutputDirectory = Directory("./.build");
        ///              var resharperReportsDirectory = buildOutputDirectory + Directory("_ReSharperReports");
        ///             
        ///              var msBuildProperties = new Dictionary&lt;string, string&gt;();
        ///              msBuildProperties.Add("configuration", configuration);
        ///              msBuildProperties.Add("platform", "AnyCPU");
        ///             
        ///              InspectCode("./MySolution.sln", new InspectCodeSettings {
        ///                  SolutionWideAnalysis = true,
        ///                  Profile = "./MySolution.sln.DotSettings",
        ///                  MsBuildProperties = msBuildProperties,
        ///                  OutputFile = resharperReportsDirectory + File("inspectcode-output.xml"),
        ///                  ThrowExceptionOnFindingViolations = true
        ///              });
        ///              </code>
        ///              </example>
        ///         
        public static void InspectCode(global::Cake.Core.IO.FilePath solution, global::Cake.Common.Tools.InspectCode.InspectCodeSettings settings)
        {
        }

        ///             <summary>
        ///             Runs ReSharper's InspectCode using the specified config file.
        ///             </summary>
        ///             
        ///             <param name="configFile">The config file.</param>
        ///             <example>
        ///             <code>
        ///             InspectCodeFromConfig("./src/inspectcode.config");
        ///             </code>
        ///             </example>
        ///         
        public static void InspectCodeFromConfig(global::Cake.Core.IO.FilePath configFile)
        {
        }
    }
}

namespace Cake.Common.Tools.MSBuild
{
    public static class MSBuildAliasesMetadata
    {
        ///             <summary>
        ///             Builds the specified solution using MSBuild.
        ///             </summary>
        ///             
        ///             <param name="solution">The solution.</param>
        ///             <example>
        ///             <code>
        ///             MSBuild("./src/Cake.sln");
        ///             </code>
        ///             </example>
        ///         
        public static void MSBuild(global::Cake.Core.IO.FilePath solution)
        {
        }

        ///             <summary>
        ///             Builds the specified solution using MSBuild.
        ///             </summary>
        ///             
        ///             <param name="solution">The solution to build.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             MSBuild("./src/Cake.sln", new MSBuildSettings {
        ///                 Verbosity = Verbosity.Minimal,
        ///                 ToolVersion = MSBuildToolVersion.VS2015,
        ///                 Configuration = "Release",
        ///                 PlatformTarget = PlatformTarget.MSIL
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void MSBuild(global::Cake.Core.IO.FilePath solution, global::Cake.Common.Tools.MSBuild.MSBuildSettings settings)
        {
        }

        ///             <summary>
        ///             Builds the specified solution using MSBuild.
        ///             </summary>
        ///             
        ///             <param name="solution">The solution to build.</param>
        ///             <param name="configurator">The settings configurator.</param>
        ///             <example>
        ///             <code>
        ///             MSBuild("./src/Cake.sln", configurator =&gt;
        ///                 configurator.SetConfiguration("Debug")
        ///                     .SetVerbosity(Verbosity.Minimal)
        ///                     .UseToolVersion(MSBuildToolVersion.VS2015)
        ///                     .SetMSBuildPlatform(MSBuildPlatform.x86)
        ///                     .SetPlatformTarget(PlatformTarget.MSIL));
        ///             </code>
        ///             </example>
        ///         
        public static void MSBuild(global::Cake.Core.IO.FilePath solution, global::System.Action<global::Cake.Common.Tools.MSBuild.MSBuildSettings> configurator)
        {
        }
    }
}

namespace Cake.Common.Tools.MSTest
{
    public static class MSTestAliasesMetadata
    {
        ///             <summary>
        ///             Runs all MSTest unit tests in the specified assemblies.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var paths = new List&lt;FilePath&gt;() { "./assemblydir1", "./assemblydir2" };
        ///             MSTest(paths);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblyPaths">The assembly paths.</param>
        ///         
        public static void MSTest(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblyPaths)
        {
        }

        ///             <summary>
        ///             Runs all MSTest unit tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             MSTest("./Tests/*.UnitTests.dll");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///         
        public static void MSTest(System.String pattern)
        {
        }

        ///             <summary>
        ///             Runs all MSTest unit tests in the specified assemblies.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var paths = new List&lt;FilePath&gt;() { "./assemblydir1", "./assemblydir2" };
        ///             MSTest(paths, new MSTestSettings() { NoIsolation = false });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblyPaths">The assembly paths.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void MSTest(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblyPaths, global::Cake.Common.Tools.MSTest.MSTestSettings settings)
        {
        }

        ///             <summary>
        ///             Runs all MSTest unit tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             MSTest("./Tests/*.UnitTests.dll", new MSTestSettings() { NoIsolation = false });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void MSTest(System.String pattern, global::Cake.Common.Tools.MSTest.MSTestSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.NSIS
{
    public static class NSISAliasesMetadata
    {
        ///             <summary>
        ///             Compiles the given NSIS script using the default settings.
        ///             </summary>
        ///             
        ///             <param name="scriptFile">The path to the <c>.nsi</c> script file to compile.</param>
        ///             <example>
        ///             <code>
        ///             MakeNSIS("./src/Cake.nsi");
        ///             </code>
        ///             </example>
        ///         
        public static void MakeNSIS(global::Cake.Core.IO.FilePath scriptFile)
        {
        }

        ///             <summary>
        ///             Compiles the given NSIS script using the given <paramref name="settings" />.
        ///             </summary>
        ///             
        ///             <param name="scriptFile">The path to the <c>.nsi</c> script file to compile.</param>
        ///             <param name="settings">The <see cref="T:Cake.Common.Tools.NSIS.MakeNSISSettings" /> to use.</param>
        ///             <example>
        ///             <code>
        ///             MakeNSIS("./src/Cake.nsi", new MakeNSISSettings {
        ///                 NoConfig = true
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void MakeNSIS(global::Cake.Core.IO.FilePath scriptFile, global::Cake.Common.Tools.NSIS.MakeNSISSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.NUnit
{
    public static class NUnit3AliasesMetadata
    {
        ///             <summary>
        ///             Runs all NUnit unit tests in the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <example>
        ///             <code>
        ///             NUnit3(new [] { "./src/Example.Tests/bin/Release/Example.Tests.dll" });
        ///             </code>
        ///             </example>
        ///         
        public static void NUnit3(global::System.Collections.Generic.IEnumerable<System.String> assemblies)
        {
        }

        ///             <summary>
        ///             Runs all NUnit unit tests in the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <example>
        ///             <code>
        ///             var testAssemblies = GetFiles("./src/**/bin/Release/*.Tests.dll");
        ///             NUnit3(testAssemblies);
        ///             </code>
        ///             </example>
        ///         
        public static void NUnit3(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies)
        {
        }

        ///             <summary>
        ///             Runs all NUnit unit tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <example>
        ///             <code>
        ///             NUnit3("./src/**/bin/Release/*.Tests.dll");
        ///             </code>
        ///             </example>
        ///         
        public static void NUnit3(System.String pattern)
        {
        }

        ///             <summary>
        ///             Runs all NUnit unit tests in the specified assemblies,
        ///             using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             var testAssemblies = GetFiles("./src/**/bin/Release/*.Tests.dll");
        ///             NUnit3(testAssemblies, new NUnit3Settings {
        ///                 NoResults = true
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void NUnit3(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies, global::Cake.Common.Tools.NUnit.NUnit3Settings settings)
        {
        }

        ///             <summary>
        ///             Runs all NUnit unit tests in the specified assemblies,
        ///             using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             NUnit3(new [] { "./src/Example.Tests/bin/Release/Example.Tests.dll" }, new NUnit3Settings {
        ///                 NoResults = true
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void NUnit3(global::System.Collections.Generic.IEnumerable<System.String> assemblies, global::Cake.Common.Tools.NUnit.NUnit3Settings settings)
        {
        }

        ///             <summary>
        ///             Runs all NUnit unit tests in the assemblies matching the specified pattern,
        ///             using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             NUnit3("./src/**/bin/Release/*.Tests.dll", new NUnit3Settings {
        ///                 NoResults = true
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void NUnit3(System.String pattern, global::Cake.Common.Tools.NUnit.NUnit3Settings settings)
        {
        }
    }

    public static class NUnitAliasesMetadata
    {
        ///             <summary>
        ///             Runs all NUnit unit tests in the specified assemblies.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var assemblies = new [] {
        ///                 "UnitTests1.dll",
        ///                 "UnitTests2.dll"
        ///             };
        ///             NUnit(assemblies);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///         
        public static void NUnit(global::System.Collections.Generic.IEnumerable<System.String> assemblies)
        {
        }

        ///             <summary>
        ///             Runs all NUnit unit tests in the specified assemblies.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var assemblies = GetFiles("./src/UnitTests/*.dll");
        ///             NUnit(assemblies);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///         
        public static void NUnit(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies)
        {
        }

        ///             <summary>
        ///             Runs all NUnit unit tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             NUnit("./src/UnitTests/*.dll");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///         
        public static void NUnit(System.String pattern)
        {
        }

        ///             <summary>
        ///             Runs all NUnit unit tests in the specified assemblies,
        ///             using the specified settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var assemblies = GetFiles(""./src/UnitTests/*.dll"");
        ///             NUnit(assemblies, new NUnitSettings {
        ///                 Timeout = 4000,
        ///                 StopOnError = true
        ///                 });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void NUnit(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies, global::Cake.Common.Tools.NUnit.NUnitSettings settings)
        {
        }

        ///             <summary>
        ///             Runs all NUnit unit tests in the specified assemblies,
        ///             using the specified settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var assemblies = new [] {
        ///                 "UnitTests1.dll",
        ///                 "UnitTests2.dll"
        ///             };
        ///             NUnit(assemblies, new NUnitSettings {
        ///                 Timeout = 4000,
        ///                 StopOnError = true
        ///                 });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void NUnit(global::System.Collections.Generic.IEnumerable<System.String> assemblies, global::Cake.Common.Tools.NUnit.NUnitSettings settings)
        {
        }

        ///             <summary>
        ///             Runs all NUnit unit tests in the assemblies matching the specified pattern,
        ///             using the specified settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             NUnit("./src/UnitTests/*.dll", new NUnitSettings {
        ///                 Timeout = 4000,
        ///                 StopOnError = true
        ///                 });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void NUnit(System.String pattern, global::Cake.Common.Tools.NUnit.NUnitSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.NuGet
{
    public static class NuGetAliasesMetadata
    {
        ///             <summary>
        ///             Adds a NuGet package using package id and source.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to add.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             NuGetAdd("MyNugetPackage", new NuGetAddSettings({
        ///                 Source = "//bar/packages/"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetAdd(System.String packageId, global::Cake.Common.Tools.NuGet.Add.NuGetAddSettings settings)
        {
        }

        ///             <summary>
        ///             Adds a NuGet package using package id and source.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to add.</param>
        ///             <param name="source">Path to the local feed source.</param>
        ///             <example>
        ///             <code>
        ///             NuGetAdd("MyNugetPackage", "//bar/packages/");
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetAdd(System.String packageId, System.String source)
        {
        }

        ///              <summary>
        ///              Adds NuGet package source using the specified name &amp;source to global user config
        ///              </summary>
        ///              
        ///              <param name="name">Name of the source.</param>
        ///              <param name="source">Path to the package(s) source.</param>
        ///              <example>
        ///              <code>
        ///              var feed = new
        ///                          {
        ///                              Name = EnvironmentVariable("PUBLIC_FEED_NAME"),
        ///                              Source = EnvironmentVariable("PUBLIC_FEED_SOURCE")
        ///                          };
        ///             
        ///              NuGetAddSource(
        ///                  name:feed.Name,
        ///                  source:feed.Source
        ///              );
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetAddSource(System.String name, System.String source)
        {
        }

        ///              <summary>
        ///              Adds NuGet package source using the specified name, source &amp; settings to global user config
        ///              </summary>
        ///              
        ///              <param name="name">Name of the source.</param>
        ///              <param name="source">Path to the package(s) source.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///              var nugetSourceSettings = new NuGetSourcesSettings
        ///                                          {
        ///                                              UserName = EnvironmentVariable("PRIVATE_FEED_USERNAME"),
        ///                                              Password = EnvironmentVariable("PRIVATE_FEED_PASSWORD"),
        ///                                              IsSensitiveSource = true,
        ///                                              Verbosity = NuGetVerbosity.Detailed
        ///                                          };
        ///             
        ///              var feed = new
        ///                          {
        ///                              Name = EnvironmentVariable("PRIVATE_FEED_NAME"),
        ///                              Source = EnvironmentVariable("PRIVATE_FEED_SOURCE")
        ///                          };
        ///             
        ///              NuGetAddSource(
        ///                  name:feed.Name,
        ///                  source:feed.Source,
        ///                  settings:nugetSourceSettings
        ///              );
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetAddSource(System.String name, System.String source, global::Cake.Common.Tools.NuGet.Sources.NuGetSourcesSettings settings)
        {
        }

        ///             <summary>
        ///             Checks whether or not a NuGet package source exists in the global user configuration, using the specified source.
        ///             </summary>
        ///             
        ///             <param name="source">Path to the package(s) source.</param>
        ///             <returns>Whether or not the NuGet package source exists in the global user configuration.</returns>
        ///             <example>
        ///               <code>
        ///             var feed = new
        ///             {
        ///                 Name = EnvironmentVariable("PRIVATE_FEED_NAME"),
        ///                 Source = EnvironmentVariable("PRIVATE_FEED_SOURCE")
        ///             };
        ///             if (!NuGetHasSource(source:feed.Source))
        ///             {
        ///                 Information("Source missing");
        ///             }
        ///             else
        ///             {
        ///                 Information("Source already exists");
        ///             }
        ///             </code>
        ///             </example>
        ///         
        public static System.Boolean NuGetHasSource(System.String source)
        {
            return default(System.Boolean);
        }

        ///             <summary>
        ///             Checks whether or not a NuGet package source exists in the global user configuration, using the specified source and settings.
        ///             </summary>
        ///             
        ///             <param name="source">Path to the package(s) source.</param>
        ///             <param name="settings">The settings.</param>
        ///             <returns>Whether the specified NuGet package source exist.</returns>
        ///             <example>
        ///               <code>
        ///             var nugetSourceSettings = new NuGetSourcesSettings
        ///             {
        ///                 UserName = EnvironmentVariable("PRIVATE_FEED_USERNAME"),
        ///                 Password = EnvironmentVariable("PRIVATE_FEED_PASSWORD"),
        ///                 IsSensitiveSource = true,
        ///                 Verbosity = NuGetVerbosity.Detailed
        ///             };
        ///             var feed = new
        ///             {
        ///                 Name = EnvironmentVariable("PRIVATE_FEED_NAME"),
        ///                 Source = EnvironmentVariable("PRIVATE_FEED_SOURCE")
        ///             };
        ///             if (!NuGetHasSource(
        ///                 source:feed.Source,
        ///                 settings:nugetSourceSettings))
        ///             {
        ///                 Information("Source missing");
        ///             }
        ///             else
        ///             {
        ///                 Information("Source already exists");
        ///             }
        ///             </code>
        ///             </example>
        ///         
        public static System.Boolean NuGetHasSource(System.String source, global::Cake.Common.Tools.NuGet.Sources.NuGetSourcesSettings settings)
        {
            return default(System.Boolean);
        }

        ///             <summary>
        ///             Adds all packages from source to destination.
        ///             </summary>
        ///             
        ///             <param name="source">The local feed package source.</param>
        ///             <param name="destination">The local feed destination source.</param>
        ///             <example>
        ///             <code>
        ///             NuGetInit("//foo/packages", "//bar/packages/");
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetInit(System.String source, System.String destination)
        {
        }

        ///             <summary>
        ///             Adds all packages from source to destination using specified settings.
        ///             </summary>
        ///             
        ///             <param name="source">The local feed package source.</param>
        ///             <param name="destination">The local feed destination source.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             NuGetInit("//foo/packages", "//bar/packages/", new NuGetInitSettings {
        ///                 Expand = true
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetInit(System.String source, System.String destination, global::Cake.Common.Tools.NuGet.Init.NuGetInitSettings settings)
        {
        }

        ///             <summary>
        ///             Installs NuGet packages.
        ///             </summary>
        ///             
        ///             <param name="packageIds">The id's of the package to install.</param>
        ///             <example>
        ///             <code>
        ///             NuGetInstall(new[] { "MyNugetPackage", "OtherNugetPackage" });
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetInstall(global::System.Collections.Generic.IEnumerable<System.String> packageIds)
        {
        }

        ///             <summary>
        ///             Installs a NuGet package.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to install.</param>
        ///             <example>
        ///             <code>
        ///             NuGetInstall("MyNugetPackage");
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetInstall(System.String packageId)
        {
        }

        ///             <summary>
        ///             Installs NuGet packages using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="packageIds">The id's of the package to install.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             NuGetInstall(new[] { "MyNugetPackage", "OtherNugetPackage" }, new NuGetInstallSettings {
        ///                 ExcludeVersion  = true,
        ///                 OutputDirectory = "./tools"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetInstall(global::System.Collections.Generic.IEnumerable<System.String> packageIds, global::Cake.Common.Tools.NuGet.Install.NuGetInstallSettings settings)
        {
        }

        ///             <summary>
        ///             Installs a NuGet package using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="packageId">The id of the package to install.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             NuGetInstall("MyNugetPackage", new NuGetInstallSettings {
        ///                 ExcludeVersion  = true,
        ///                 OutputDirectory = "./tools"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetInstall(System.String packageId, global::Cake.Common.Tools.NuGet.Install.NuGetInstallSettings settings)
        {
        }

        ///              <summary>
        ///              Installs NuGet packages using the specified package configurations.
        ///              </summary>
        ///              
        ///              <param name="packageConfigPaths">The package configurations to install.</param>
        ///              <example>
        ///              <code>
        ///              var packageConfigs = GetFiles("./**/packages.config");
        ///             
        ///              NuGetInstallFromConfig(packageConfigs);
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetInstallFromConfig(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> packageConfigPaths)
        {
        }

        ///             <summary>
        ///             Installs NuGet packages using the specified package configuration.
        ///             </summary>
        ///             
        ///             <param name="packageConfigPath">The package configuration to install.</param>
        ///             <example>
        ///             <code>
        ///             NuGetInstallFromConfig("./tools/packages.config");
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetInstallFromConfig(global::Cake.Core.IO.FilePath packageConfigPath)
        {
        }

        ///              <summary>
        ///              Installs NuGet packages using the specified package configurations and settings.
        ///              </summary>
        ///              
        ///              <param name="packageConfigPaths">The package configurations to install.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///              var packageConfigs = GetFiles("./**/packages.config");
        ///             
        ///              NuGetInstallFromConfig(packageConfigs, new NuGetInstallSettings {
        ///                  ExcludeVersion  = true,
        ///                  OutputDirectory = "./tools"
        ///                  });
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetInstallFromConfig(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> packageConfigPaths, global::Cake.Common.Tools.NuGet.Install.NuGetInstallSettings settings)
        {
        }

        ///             <summary>
        ///             Installs NuGet packages using the specified package configuration and settings.
        ///             </summary>
        ///             
        ///             <param name="packageConfigPath">The package configuration to install.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             NuGetInstallFromConfig("./tools/packages.config", new NuGetInstallSettings {
        ///                 ExcludeVersion  = true,
        ///                 OutputDirectory = "./tools"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetInstallFromConfig(global::Cake.Core.IO.FilePath packageConfigPath, global::Cake.Common.Tools.NuGet.Install.NuGetInstallSettings settings)
        {
        }

        ///              <summary>
        ///              Creates a NuGet package using the specified settings.
        ///              </summary>
        ///              
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var nuGetPackSettings   = new NuGetPackSettings {
        ///                                                  Id                      = "TestNuget",
        ///                                                  Version                 = "0.0.0.1",
        ///                                                  Title                   = "The tile of the package",
        ///                                                  Authors                 = new[] {"John Doe"},
        ///                                                  Owners                  = new[] {"Contoso"},
        ///                                                  Description             = "The description of the package",
        ///                                                  Summary                 = "Excellent summary of what the package does",
        ///                                                  ProjectUrl              = new Uri("https://github.com/SomeUser/TestNuget/"),
        ///                                                  IconUrl                 = new Uri("http://cdn.rawgit.com/SomeUser/TestNuget/master/icons/testnuget.png"),
        ///                                                  LicenseUrl              = new Uri("https://github.com/SomeUser/TestNuget/blob/master/LICENSE.md"),
        ///                                                  Copyright               = "Some company 2015",
        ///                                                  ReleaseNotes            = new [] {"Bug fixes", "Issue fixes", "Typos"},
        ///                                                  Tags                    = new [] {"Cake", "Script", "Build"},
        ///                                                  RequireLicenseAcceptance= false,
        ///                                                  Symbols                 = false,
        ///                                                  NoPackageAnalysis       = true,
        ///                                                  Files                   = new [] {
        ///                                                                                       new NuSpecContent {Source = "bin/TestNuget.dll", Target = "bin"},
        ///                                                                                    },
        ///                                                  BasePath                = "./src/TestNuget/bin/release",
        ///                                                  OutputDirectory         = "./nuget"
        ///                                              };
        ///             
        ///                  NuGetPack(nuGetPackSettings);
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetPack(global::Cake.Common.Tools.NuGet.Pack.NuGetPackSettings settings)
        {
        }

        ///              <summary>
        ///              Creates NuGet packages using the specified Nuspec or project files.
        ///              </summary>
        ///              
        ///              <param name="filePaths">The nuspec or project file paths.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var nuGetPackSettings   = new NuGetPackSettings {
        ///                                                  Id                      = "TestNuget",
        ///                                                  Version                 = "0.0.0.1",
        ///                                                  Title                   = "The tile of the package",
        ///                                                  Authors                 = new[] {"John Doe"},
        ///                                                  Owners                  = new[] {"Contoso"},
        ///                                                  Description             = "The description of the package",
        ///                                                  Summary                 = "Excellent summary of what the package does",
        ///                                                  ProjectUrl              = new Uri("https://github.com/SomeUser/TestNuget/"),
        ///                                                  IconUrl                 = new Uri("http://cdn.rawgit.com/SomeUser/TestNuget/master/icons/testnuget.png"),
        ///                                                  LicenseUrl              = new Uri("https://github.com/SomeUser/TestNuget/blob/master/LICENSE.md"),
        ///                                                  Copyright               = "Some company 2015",
        ///                                                  ReleaseNotes            = new [] {"Bug fixes", "Issue fixes", "Typos"},
        ///                                                  Tags                    = new [] {"Cake", "Script", "Build"},
        ///                                                  RequireLicenseAcceptance= false,
        ///                                                  Symbols                 = false,
        ///                                                  NoPackageAnalysis       = true,
        ///                                                  Files                   = new [] {
        ///                                                                                       new NuSpecContent {Source = "bin/TestNuget.dll", Target = "bin"},
        ///                                                                                    },
        ///                                                  BasePath                = "./src/TestNuget/bin/release",
        ///                                                  OutputDirectory         = "./nuget"
        ///                                              };
        ///             
        ///                  var nuspecFiles = GetFiles("./**/*.nuspec");
        ///                  NuGetPack(nuspecFiles, nuGetPackSettings);
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetPack(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> filePaths, global::Cake.Common.Tools.NuGet.Pack.NuGetPackSettings settings)
        {
        }

        ///              <summary>
        ///              Creates a NuGet package using the specified Nuspec or project file.
        ///              </summary>
        ///              
        ///              <param name="filePath">The nuspec or project file path.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  var nuGetPackSettings   = new NuGetPackSettings {
        ///                                                  Id                      = "TestNuget",
        ///                                                  Version                 = "0.0.0.1",
        ///                                                  Title                   = "The tile of the package",
        ///                                                  Authors                 = new[] {"John Doe"},
        ///                                                  Owners                  = new[] {"Contoso"},
        ///                                                  Description             = "The description of the package",
        ///                                                  Summary                 = "Excellent summary of what the package does",
        ///                                                  ProjectUrl              = new Uri("https://github.com/SomeUser/TestNuget/"),
        ///                                                  IconUrl                 = new Uri("http://cdn.rawgit.com/SomeUser/TestNuget/master/icons/testnuget.png"),
        ///                                                  LicenseUrl              = new Uri("https://github.com/SomeUser/TestNuget/blob/master/LICENSE.md"),
        ///                                                  Copyright               = "Some company 2015",
        ///                                                  ReleaseNotes            = new [] {"Bug fixes", "Issue fixes", "Typos"},
        ///                                                  Tags                    = new [] {"Cake", "Script", "Build"},
        ///                                                  RequireLicenseAcceptance= false,
        ///                                                  Symbols                 = false,
        ///                                                  NoPackageAnalysis       = true,
        ///                                                  Files                   = new [] {
        ///                                                                                       new NuSpecContent {Source = "bin/TestNuget.dll", Target = "bin"},
        ///                                                                                    },
        ///                                                  BasePath                = "./src/TestNuget/bin/release",
        ///                                                  OutputDirectory         = "./nuget"
        ///                                              };
        ///             
        ///                  NuGetPack("./nuspec/TestNuget.nuspec", nuGetPackSettings);
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetPack(global::Cake.Core.IO.FilePath filePath, global::Cake.Common.Tools.NuGet.Pack.NuGetPackSettings settings)
        {
        }

        ///              <summary>
        ///              Pushes NuGet packages to a NuGet server and publishes them.
        ///              </summary>
        ///              
        ///              <param name="packageFilePaths">The <c>.nupkg</c> file paths.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <para>NOTE: Starting with NuGet 3.4.2, the Source parameter is a mandatory parameter.</para>
        ///              <para>It is strongly recommended that you ALWAYS set the Source property within the <see cref="T:Cake.Common.Tools.NuGet.Push.NuGetPushSettings" /> instance.</para>
        ///              <code>
        ///              // Get the paths to the packages.
        ///              var packages = GetFiles("./**/*.nupkg");
        ///             
        ///              // Push the package.
        ///              NuGetPush(packages, new NuGetPushSettings {
        ///                  Source = "http://example.com/nugetfeed",
        ///                  ApiKey = "4003d786-cc37-4004-bfdf-c4f3e8ef9b3a"
        ///              });
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetPush(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> packageFilePaths, global::Cake.Common.Tools.NuGet.Push.NuGetPushSettings settings)
        {
        }

        ///              <summary>
        ///              Pushes a NuGet package to a NuGet server and publishes it.
        ///              </summary>
        ///              
        ///              <param name="packageFilePath">The <c>.nupkg</c> file path.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <para>NOTE: Starting with NuGet 3.4.2, the Source parameter is a mandatory parameter.</para>
        ///              <para>It is strongly recommended that you ALWAYS set the Source property within the <see cref="T:Cake.Common.Tools.NuGet.Push.NuGetPushSettings" /> instance.</para>
        ///              <code>
        ///              // Get the path to the package.
        ///              var package = "./nuget/SlackPRTGCommander.0.0.1.nupkg";
        ///             
        ///              // Push the package.
        ///              NuGetPush(package, new NuGetPushSettings {
        ///                  Source = "http://example.com/nugetfeed",
        ///                  ApiKey = "4003d786-cc37-4004-bfdf-c4f3e8ef9b3a"
        ///              });
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetPush(global::Cake.Core.IO.FilePath packageFilePath, global::Cake.Common.Tools.NuGet.Push.NuGetPushSettings settings)
        {
        }

        ///              <summary>
        ///              Removes NuGet package source using the specified name &amp; source from global user config
        ///              </summary>
        ///              
        ///              <param name="name">Name of the source.</param>
        ///              <param name="source">Path to the package(s) source.</param>
        ///              <example>
        ///              <code>
        ///              var feed = new
        ///                          {
        ///                              Name = EnvironmentVariable("PRIVATE_FEED_NAME"),
        ///                              Source = EnvironmentVariable("PRIVATE_FEED_SOURCE")
        ///                          };
        ///             
        ///              NuGetRemoveSource(
        ///                 name:feed.Name,
        ///                 source:feed.Source
        ///              );
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetRemoveSource(System.String name, System.String source)
        {
        }

        ///              <summary>
        ///              Removes NuGet package source using the specified name, source &amp; settings from global user config
        ///              </summary>
        ///              
        ///              <param name="name">Name of the source.</param>
        ///              <param name="source">Path to the package(s) source.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///              var nugetSourceSettings = new NuGetSourcesSettings
        ///                                          {
        ///                                              UserName = EnvironmentVariable("PRIVATE_FEED_USERNAME"),
        ///                                              Password = EnvironmentVariable("PRIVATE_FEED_PASSWORD"),
        ///                                              IsSensitiveSource = true,
        ///                                              Verbosity = NuGetVerbosity.Detailed
        ///                                          };
        ///             
        ///              var feed = new
        ///                          {
        ///                              Name = EnvironmentVariable("PRIVATE_FEED_NAME"),
        ///                              Source = EnvironmentVariable("PRIVATE_FEED_SOURCE")
        ///                          };
        ///             
        ///              NuGetRemoveSource(
        ///                 name:feed.Name,
        ///                 source:feed.Source,
        ///                 settings:nugetSourceSettings
        ///              );
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetRemoveSource(System.String name, System.String source, global::Cake.Common.Tools.NuGet.Sources.NuGetSourcesSettings settings)
        {
        }

        ///             <summary>
        ///             Restores NuGet packages for the specified targets.
        ///             </summary>
        ///             
        ///             <param name="targetFilePaths">The targets to restore.</param>
        ///             <example>
        ///             <code>
        ///                 var solutions = GetFiles("./**/*.sln");
        ///                 NuGetRestore(solutions);
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetRestore(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> targetFilePaths)
        {
        }

        ///             <summary>
        ///             Restores NuGet packages for the specified target.
        ///             </summary>
        ///             
        ///             <param name="targetFilePath">The target to restore.</param>
        ///             <example>
        ///             <code>
        ///                 var solutions = GetFiles("./**/*.sln");
        ///                 // Restore all NuGet packages.
        ///                 foreach(var solution in solutions)
        ///                 {
        ///                     Information("Restoring {0}", solution);
        ///                     NuGetRestore(solution);
        ///                 }
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetRestore(global::Cake.Core.IO.FilePath targetFilePath)
        {
        }

        ///             <summary>
        ///             Restores NuGet packages using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="targetFilePaths">The targets to restore.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///                 var solutions = GetFiles("./**/*.sln");
        ///                 NuGetRestore(solutions, new NuGetRestoreSettings { NoCache = true });
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetRestore(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> targetFilePaths, global::Cake.Common.Tools.NuGet.Restore.NuGetRestoreSettings settings)
        {
        }

        ///             <summary>
        ///             Restores NuGet packages using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="targetFilePath">The target to restore.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///                 var solutions = GetFiles("./**/*.sln");
        ///                 // Restore all NuGet packages.
        ///                 foreach(var solution in solutions)
        ///                 {
        ///                     Information("Restoring {0}", solution);
        ///                     NuGetRestore(solution, new NuGetRestoreSettings { NoCache = true });
        ///                 }
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetRestore(global::Cake.Core.IO.FilePath targetFilePath, global::Cake.Common.Tools.NuGet.Restore.NuGetRestoreSettings settings)
        {
        }

        ///             <summary>
        ///             Installs NuGet packages using the specified API key and source.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             NuGetSetApiKey("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", "https://nuget.org/api/v2/");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="apiKey">The API key.</param>
        ///             <param name="source">Server URL where the API key is valid.</param>
        ///         
        public static void NuGetSetApiKey(System.String apiKey, System.String source)
        {
        }

        ///             <summary>
        ///             Installs NuGet packages using the specified API key, source and settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var setting = new NuGetSetApiKeySettings {
        ///                 Verbosity = NuGetVerbosity.Detailed
        ///                 };
        ///             NuGetSetApiKey("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", "https://nuget.org/api/v2/", setting);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="apiKey">The API key.</param>
        ///             <param name="source">Server URL where the API key is valid.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void NuGetSetApiKey(System.String apiKey, System.String source, global::Cake.Common.Tools.NuGet.SetApiKey.NuGetSetApiKeySettings settings)
        {
        }

        ///             <summary>
        ///             Set the proxy settings to be used while connecting to your NuGet feed.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             NuGetSetProxy("127.0.0.1:8080", "proxyuser","Pa$$w0rd1");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="proxy">The url of the proxy.</param>
        ///             <param name="username">The username used to access the proxy.</param>
        ///             <param name="password">The password used to access the proxy.</param>
        ///         
        public static void NuGetSetProxy(System.String proxy, System.String username, System.String password)
        {
        }

        ///             <summary>
        ///             Set the proxy settings to be used while connecting to your NuGet feed, including settings.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var setting = new NuGetSetProxySettings {
        ///                 Verbosity = NuGetVerbosity.Detailed
        ///                 };
        ///             NuGetSetProxy("127.0.0.1:8080", "proxyuser","Pa$$w0rd1", setting);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="proxy">The url of the proxy.</param>
        ///             <param name="username">The username used to access the proxy.</param>
        ///             <param name="password">The password used to access the proxy.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void NuGetSetProxy(System.String proxy, System.String username, System.String password, global::Cake.Common.Tools.NuGet.SetProxy.NuGetSetProxySettings settings)
        {
        }

        ///              <summary>
        ///              Updates NuGet packages.
        ///              </summary>
        ///              
        ///              <param name="targetFiles">The targets to update.</param>
        ///              <example>
        ///              <code>
        ///              var targets = GetFiles("./**/packages.config");
        ///             
        ///              NuGetUpdate(targets);
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetUpdate(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> targetFiles)
        {
        }

        ///             <summary>
        ///             Updates NuGet packages.
        ///             </summary>
        ///             
        ///             <param name="targetFile">The target to update.</param>
        ///             <example>
        ///             <code>
        ///             NuGetUpdate("./tools/packages.config");
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetUpdate(global::Cake.Core.IO.FilePath targetFile)
        {
        }

        ///              <summary>
        ///              Updates NuGet packages using the specified settings.
        ///              </summary>
        ///              
        ///              <param name="targetFiles">The targets to update.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///              var targets = GetFiles("./**/packages.config");
        ///             
        ///              NuGetUpdate(targets, new NuGetUpdateSettings {
        ///                  Prerelease = true,
        ///              });
        ///              </code>
        ///              </example>
        ///         
        public static void NuGetUpdate(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> targetFiles, global::Cake.Common.Tools.NuGet.Update.NuGetUpdateSettings settings)
        {
        }

        ///             <summary>
        ///             Updates NuGet packages using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="targetFile">The target to update.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             NuGetUpdate("./tools/packages.config", new NuGetUpdateSettings {
        ///                 Prerelease = true,
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void NuGetUpdate(global::Cake.Core.IO.FilePath targetFile, global::Cake.Common.Tools.NuGet.Update.NuGetUpdateSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.OctopusDeploy
{
    public static class OctopusDeployAliasesMetadata
    {
        ///              <summary>
        ///              Creates a release for the specified Octopus Deploy Project.
        ///              </summary>
        ///              
        ///              <param name="projectName">The name of the project.</param>
        ///              <param name="settings">The settings.</param>
        ///              <example>
        ///              <code>
        ///                  // Minimum required
        ///                  OctoCreateRelease(projectNameOnServer, new CreateReleaseSettings {
        ///                      Server = "http://octopus-deploy.example",
        ///                      ApiKey = "API-XXXXXXXXXXXXXXXXXXXX"
        ///                  });
        ///             
        ///                  OctoCreateRelease(projectNameOnServer, new CreateReleaseSettings {
        ///                      Server = "http://octopus-deploy.example",
        ///                      Username = "DeployUser",
        ///                      Password = "a-very-secure-password"
        ///                  });
        ///             
        ///                  OctoCreateRelease(projectNameOnServer, new CreateReleaseSettings {
        ///                      ConfigurationFile = @"C:\OctopusDeploy.config"
        ///                  });
        ///             
        ///                  // Additional Options
        ///                  OctoCreateRelease(projectNameOnServer, new CreateReleaseSettings {
        ///                      ToolPath = "./tools/OctopusTools/Octo.exe"
        ///                      EnableDebugLogging = true,
        ///                      IgnoreSslErrors = true,
        ///                      EnableServiceMessages = true, // Enables teamcity services messages when logging
        ///                      ReleaseNumber = "1.8.2",
        ///                      DefaultPackageVersion = "1.0.0.0", // All packages in the release should be 1.0.0.0
        ///                      Packages = new Dictionary&lt;string, string&gt;
        ///                                  {
        ///                                      { "PackageOne", "1.0.2.3" },
        ///                                      { "PackageTwo", "5.2.3" }
        ///                                  },
        ///                      PackagesFolder = @"C:\MyOtherNugetFeed",
        ///             
        ///                      // One or the other
        ///                      ReleaseNotes = "Version 2.0 \n What a milestone we have ...",
        ///                      ReleaseNotesFile = "./ReleaseNotes.md",
        ///             
        ///                      IgnoreExisting = true // if this release number already exists, ignore it
        ///                  });
        ///              </code>
        ///              </example>
        ///         
        public static void OctoCreateRelease(System.String projectName, global::Cake.Common.Tools.OctopusDeploy.CreateReleaseSettings settings)
        {
        }

        ///              <summary>
        ///              Deploys the specified already existing release into a specified environment
        ///              See <see href="http://docs.octopusdeploy.com/display/OD/Deploying+releases">Octopus Documentation</see> for more details.
        ///              </summary>
        ///              
        ///              <param name="server">The Octopus server URL</param>
        ///              <param name="apiKey">The user's API key</param>
        ///              <param name="projectName">Name of the target project</param>
        ///              <param name="deployTo">Target environment name</param>
        ///              <param name="releaseNumber">Version number of the release to deploy. Specify "latest" for the latest release</param>
        ///              <param name="settings">Deployment settings</param>
        ///              <example>
        ///              <code>
        ///                  // bare minimum
        ///                  OctoDeployRelease("http://octopus-deploy.example", "API-XXXXXXXXXXXXXXXXXXXX", "MyGreatProject", "Testing", "2.1.15-RC" new OctopusDeployReleaseDeploymentSettings());
        ///             
        ///                  // All of deployment arguments
        ///                  OctoDeployRelease("http://octopus-deploy.example", "API-XXXXXXXXXXXXXXXXXXXX", "MyGreatProject", "Testing", "2.1.15-RC" new OctopusDeployReleaseDeploymentSettings {
        ///                      ShowProgress = true,
        ///                      ForcePackageDownload = true,
        ///                      WaitForDeployment = true,
        ///                      DeploymentTimeout = TimeSpan.FromMinutes(1),
        ///                      CancelOnTimeout = true,
        ///                      DeploymentChecksLeepCycle = TimeSpan.FromMinutes(77),
        ///                      GuidedFailure = true,
        ///                      SpecificMachines = new string[] { "Machine1", "Machine2" },
        ///                      Force = true,
        ///                      SkipSteps = new[] { "Step1", "Step2" },
        ///                      NoRawLog = true,
        ///                      RawLogFile = "someFile.txt",
        ///                      DeployAt = new DateTime(2010, 6, 15).AddMinutes(1),
        ///                      Tenant = new[] { "Tenant1", "Tenant2" },
        ///                      TenantTags = new[] { "Tag1", "Tag2" },
        ///                  });
        ///              </code>
        ///              </example>
        ///         
        public static void OctoDeployRelease(System.String server, System.String apiKey, System.String projectName, System.String deployTo, System.String releaseNumber, global::Cake.Common.Tools.OctopusDeploy.OctopusDeployReleaseDeploymentSettings settings)
        {
        }

        ///             <summary>
        ///             Packs the specified folder into an Octopus Deploy package.
        ///             </summary>
        ///             
        ///             <param name="id">The package ID.</param>
        ///         
        public static void OctoPack(System.String id)
        {
        }

        ///             <summary>
        ///             Packs the specified folder into an Octopus Deploy package.
        ///             </summary>
        ///             
        ///             <param name="id">The package ID.</param>
        ///             <param name="settings">The settings</param>
        ///         
        public static void OctoPack(System.String id, global::Cake.Common.Tools.OctopusDeploy.OctopusPackSettings settings = null)
        {
        }

        ///             <summary>
        ///             Pushes the specified packages to the Octopus Deploy repository
        ///             </summary>
        ///             
        ///             <param name="server">The Octopus server URL</param>
        ///             <param name="apiKey">The user's API key</param>
        ///             <param name="packagePaths">Paths to the packages</param>
        ///             <param name="settings">The settings</param>
        ///         
        public static void OctoPush(System.String server, System.String apiKey, global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> packagePaths, global::Cake.Common.Tools.OctopusDeploy.OctopusPushSettings settings)
        {
        }

        ///             <summary>
        ///             Pushes the specified package to the Octopus Deploy repository
        ///             </summary>
        ///             
        ///             <param name="server">The Octopus server URL</param>
        ///             <param name="apiKey">The user's API key</param>
        ///             <param name="packagePath">Path to the package</param>
        ///             <param name="settings">The settings</param>
        ///         
        public static void OctoPush(System.String server, System.String apiKey, global::Cake.Core.IO.FilePath packagePath, global::Cake.Common.Tools.OctopusDeploy.OctopusPushSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.OpenCover
{
    public static class OpenCoverAliasesMetadata
    {
        ///             <summary>
        ///             Runs <see href="https://github.com/OpenCover/opencover">OpenCover</see>
        ///             for the specified action and settings.
        ///             </summary>
        ///             
        ///             <param name="action">The action to run OpenCover for.</param>
        ///             <param name="outputFile">The OpenCover output file.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             OpenCover(tool =&gt; {
        ///               tool.XUnit2("./**/App.Tests.dll",
        ///                 new XUnit2Settings {
        ///                   ShadowCopy = false
        ///                 });
        ///               },
        ///               new FilePath("./result.xml"),
        ///               new OpenCoverSettings()
        ///                 .WithFilter("+[App]*")
        ///                 .WithFilter("-[App.Tests]*"));
        ///             </code>
        ///             </example>
        ///         
        public static void OpenCover(global::System.Action<global::Cake.Core.ICakeContext> action, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.OpenCover.OpenCoverSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.ReportGenerator
{
    public static class ReportGeneratorAliasesMetadata
    {
        ///             <summary>
        ///             Converts the specified coverage report into human readable form.
        ///             </summary>
        ///             
        ///             <param name="report">The coverage report.</param>
        ///             <param name="targetDir">The output directory.</param>
        ///             <example>
        ///             <code>
        ///             ReportGenerator("c:/temp/coverage/report.xml", "c:/temp/output");
        ///             </code>
        ///             </example>
        ///         
        public static void ReportGenerator(global::Cake.Core.IO.FilePath report, global::Cake.Core.IO.DirectoryPath targetDir)
        {
        }

        ///             <summary>
        ///             Converts the specified coverage reports into human readable form.
        ///             </summary>
        ///             
        ///             <param name="reports">The coverage reports.</param>
        ///             <param name="targetDir">The output directory.</param>
        ///             <example>
        ///             <code>
        ///             ReportGenerator(new[] { "c:/temp/coverage1.xml", "c:/temp/coverage2.xml" }, "c:/temp/output");
        ///             </code>
        ///             </example>
        ///         
        public static void ReportGenerator(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> reports, global::Cake.Core.IO.DirectoryPath targetDir)
        {
        }

        ///             <summary>
        ///             Converts the coverage report specified by the glob pattern into human readable form.
        ///             </summary>
        ///             
        ///             <param name="pattern">The glob pattern.</param>
        ///             <param name="targetDir">The output directory.</param>
        ///             <example>
        ///             <code>
        ///             ReportGenerator("c:/temp/coverage/*.xml", "c:/temp/output");
        ///             </code>
        ///             </example>
        ///         
        public static void ReportGenerator(System.String pattern, global::Cake.Core.IO.DirectoryPath targetDir)
        {
        }

        ///             <summary>
        ///             Converts the specified coverage reports into human readable form using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="reports">The coverage reports.</param>
        ///             <param name="targetDir">The output directory.</param>
        ///             <param name="settings">The settings.</param>&gt;
        ///             <example>
        ///             <code>
        ///             ReportGenerator(new[] { "c:/temp/coverage1.xml", "c:/temp/coverage2.xml" }, "c:/temp/output", new ReportGeneratorSettings(){
        ///                 ToolPath = "c:/tools/reportgenerator.exe"
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ReportGenerator(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> reports, global::Cake.Core.IO.DirectoryPath targetDir, global::Cake.Common.Tools.ReportGenerator.ReportGeneratorSettings settings)
        {
        }

        ///             <summary>
        ///             Converts the specified coverage report into human readable form using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="report">The coverage report.</param>
        ///             <param name="targetDir">The output directory.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ReportGenerator("c:/temp/coverage.xml", "c:/temp/output", new ReportGeneratorSettings(){
        ///                 ToolPath = "c:/tools/reportgenerator.exe"
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ReportGenerator(global::Cake.Core.IO.FilePath report, global::Cake.Core.IO.DirectoryPath targetDir, global::Cake.Common.Tools.ReportGenerator.ReportGeneratorSettings settings)
        {
        }

        ///             <summary>
        ///             Converts the coverage report specified by the glob pattern into human readable form using the specified settings.
        ///             </summary>
        ///             
        ///             <param name="pattern">The glob pattern.</param>
        ///             <param name="targetDir">The output directory.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             ReportGenerator("c:/temp/coverage/*.xml", "c:/temp/output", new ReportGeneratorSettings(){
        ///                 ToolPath = "c:/tools/reportgenerator.exe"
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ReportGenerator(System.String pattern, global::Cake.Core.IO.DirectoryPath targetDir, global::Cake.Common.Tools.ReportGenerator.ReportGeneratorSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.ReportUnit
{
    public static class ReportUnitAliasesMetadata
    {
        ///             <summary>
        ///             Converts the reports in the specified directory into human readable form.
        ///             </summary>
        ///             
        ///             <param name="inputFolder">The input folder.</param>
        ///             <example>
        ///             <para>Provide only an input folder, which will causes ReportUnit to search entire directory for report files.</para>
        ///             <para>Cake task:</para>
        ///             <code>
        ///             ReportUnit("c:/temp");
        ///             </code>
        ///             </example>
        ///         
        public static void ReportUnit(global::Cake.Core.IO.DirectoryPath inputFolder)
        {
        }

        ///             <summary>
        ///             Converts the single specified report into human readable form and outputs to specified file.
        ///             </summary>
        ///             
        ///             <param name="inputFile">The input file.</param>
        ///             <param name="outputFile">The output file.</param>
        ///             <example>
        ///             <para>Provide both input and output file, which will causes ReportUnit to transform only the specific file, and output to the specified location.</para>
        ///             <para>Cake task:</para>
        ///             <code>
        ///             ReportUnit("c:/temp/input", "c:/temp/output");
        ///             </code>
        ///             </example>
        ///         
        public static void ReportUnit(global::Cake.Core.IO.FilePath inputFile, global::Cake.Core.IO.FilePath outputFile)
        {
        }

        ///             <summary>
        ///             Converts the reports in the specified directory into human readable form.
        ///             </summary>
        ///             
        ///             <param name="inputFolder">The input folder.</param>
        ///             <param name="settings">The ReportUnit settings.</param>
        ///             <example>
        ///             <para>Provide an input folder and custom ToolPath, which will causes ReportUnit to search entire directory for report files.</para>
        ///             <para>Cake task:</para>
        ///             <code>
        ///             ReportUnit("c:/temp", new ReportUnitSettings(){
        ///                 ToolPath = "c:/tools/reportunit.exe"
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ReportUnit(global::Cake.Core.IO.DirectoryPath inputFolder, global::Cake.Common.Tools.ReportUnit.ReportUnitSettings settings)
        {
        }

        ///             <summary>
        ///             Converts the single specified report into human readable form and outputs to specified file.
        ///             </summary>
        ///             
        ///             <param name="inputFile">The input file.</param>
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="settings">The ReportUnit settings.</param>
        ///             <example>
        ///             <para>Provide both input and output file, which will causes ReportUnit to transform only the specific file, and output to the specified location.  Also use a custom path for the reportunit.exe.</para>
        ///             <para>Cake task:</para>
        ///             <code>
        ///             ReportUnit("c:/temp/input", "c:/temp/output", new ReportUnitSettings(){
        ///                 ToolPath = "c:/tools/reportunit.exe"
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void ReportUnit(global::Cake.Core.IO.FilePath inputFile, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.ReportUnit.ReportUnitSettings settings)
        {
        }

        ///             <summary>
        ///             Converts the reports in the specified directory into human readable form and outputs to specified folder.
        ///             </summary>
        ///             
        ///             <param name="inputFolder">The input folder.</param>
        ///             <param name="outputFolder">The output folder.</param>
        ///             <param name="settings">The ReportUnit settings.</param>
        ///             <example>
        ///             <para>Provide both input and output folder, which will causes ReportUnit to search entire directory for report files, and output the results to specified location.</para>
        ///             <para>Cake task:</para>
        ///             <code>
        ///             ReportUnit("c:/temp/input", "c:/temp/output");
        ///             </code>
        ///             </example>
        ///         
        public static void ReportUnit(global::Cake.Core.IO.DirectoryPath inputFolder, global::Cake.Core.IO.DirectoryPath outputFolder, global::Cake.Common.Tools.ReportUnit.ReportUnitSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.Roundhouse
{
    public static class RoundhouseAliasesMetadata
    {
        ///             <summary>
        ///             Executes Roundhouse migration to drop the database using the provided settings.
        ///             </summary>
        ///             
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             RoundhouseDrop(new RoundhouseSettings{
        ///                 ServerName = "Sql2008R2",
        ///                 DatabaseName = "AdventureWorks2008R2"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void RoundhouseDrop(global::Cake.Common.Tools.Roundhouse.RoundhouseSettings settings)
        {
        }

        ///             <summary>
        ///             Executes Roundhouse with the given configured settings.
        ///             </summary>
        ///             
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             RoundhouseMigrate(new RoundhouseSettings{
        ///                 ServerName = "Sql2008R2",
        ///                 DatabaseName = "AdventureWorks2008R2",
        ///                 SqlFilesDirectory = "./src/sql"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void RoundhouseMigrate(global::Cake.Common.Tools.Roundhouse.RoundhouseSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.SignTool
{
    public static class SignToolSignAliasesMetadata
    {
        ///             <summary>
        ///             Signs the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The target assembly.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             Task("Sign")
        ///                 .IsDependentOn("Clean")
        ///                 .IsDependentOn("Restore")
        ///                 .IsDependentOn("Build")
        ///                 .Does(() =&gt;
        ///             {
        ///                 var files = new string[] { "Core.dll", "Common.dll" };
        ///                 Sign(files, new SignToolSignSettings {
        ///                         TimeStampUri = new Uri("http://timestamp.digicert.com"),
        ///                         CertPath = "digitalcertificate.pfx",
        ///                         Password = "TopSecret"
        ///                 });
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void Sign(global::System.Collections.Generic.IEnumerable<System.String> assemblies, global::Cake.Common.Tools.SignTool.SignToolSignSettings settings)
        {
        }

        ///             <summary>
        ///             Signs the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The target assembly.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             Task("Sign")
        ///                 .IsDependentOn("Clean")
        ///                 .IsDependentOn("Restore")
        ///                 .IsDependentOn("Build")
        ///                 .Does(() =&gt;
        ///             {
        ///                 var files = GetFiles(solutionDir + "/**/bin/" + configuration + "/**/*.exe");
        ///                 Sign(files, new SignToolSignSettings {
        ///                         TimeStampUri = new Uri("http://timestamp.digicert.com"),
        ///                         CertPath = "digitalcertificate.pfx",
        ///                         Password = "TopSecret"
        ///                 });
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void Sign(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies, global::Cake.Common.Tools.SignTool.SignToolSignSettings settings)
        {
        }

        ///             <summary>
        ///             Signs the specified assembly.
        ///             </summary>
        ///             
        ///             <param name="assembly">The target assembly.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             Task("Sign")
        ///                 .IsDependentOn("Clean")
        ///                 .IsDependentOn("Restore")
        ///                 .IsDependentOn("Build")
        ///                 .Does(() =&gt;
        ///             {
        ///                 var file = new FilePath("Core.dll");
        ///                 Sign(file, new SignToolSignSettings {
        ///                         TimeStampUri = new Uri("http://timestamp.digicert.com"),
        ///                         CertPath = "digitalcertificate.pfx",
        ///                         Password = "TopSecret"
        ///                 });
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void Sign(global::Cake.Core.IO.FilePath assembly, global::Cake.Common.Tools.SignTool.SignToolSignSettings settings)
        {
        }

        ///             <summary>
        ///             Signs the specified assembly.
        ///             </summary>
        ///             
        ///             <param name="assembly">The target assembly.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             Task("Sign")
        ///                 .IsDependentOn("Clean")
        ///                 .IsDependentOn("Restore")
        ///                 .IsDependentOn("Build")
        ///                 .Does(() =&gt;
        ///             {
        ///                 var file = "Core.dll";
        ///                 Sign(file, new SignToolSignSettings {
        ///                         TimeStampUri = new Uri("http://timestamp.digicert.com"),
        ///                         CertPath = "digitalcertificate.pfx",
        ///                         Password = "TopSecret"
        ///                 });
        ///             });
        ///             </code>
        ///             </example>
        ///         
        public static void Sign(System.String assembly, global::Cake.Common.Tools.SignTool.SignToolSignSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.SpecFlow
{
    public static class SpecFlowAliasesMetadata
    {
        ///             <summary>
        ///             Creates a report that shows the usage and binding status of the steps for the entire project.
        ///             You can use this report to find both unused code in the automation layer and scenario steps that have no definition yet.
        ///             See <see href="https://github.com/techtalk/SpecFlow/wiki/Reporting#step-definition-report">SpecFlow Documentation</see> for more information.
        ///             </summary>
        ///             
        ///             <param name="projectFile">The path of the project file containing the feature files.</param>
        ///         
        public static void SpecFlowStepDefinitionReport(global::Cake.Core.IO.FilePath projectFile)
        {
        }

        ///             <summary>
        ///             Creates a report that shows the usage and binding status of the steps for the entire project.
        ///             You can use this report to find both unused code in the automation layer and scenario steps that have no definition yet.
        ///             See <see href="https://github.com/techtalk/SpecFlow/wiki/Reporting#step-definition-report">SpecFlow Documentation</see> for more information.
        ///             </summary>
        ///             
        ///             <param name="projectFile">The path of the project file containing the feature files.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void SpecFlowStepDefinitionReport(global::Cake.Core.IO.FilePath projectFile, global::Cake.Common.Tools.SpecFlow.StepDefinitionReport.SpecFlowStepDefinitionReportSettings settings)
        {
        }

        ///             <summary>
        ///             Creates a formatted HTML report of a test execution.
        ///             The report contains a summary about the executed tests and the result and also a detailed report for the individual scenario executions.
        ///             See <see href="https://github.com/techtalk/SpecFlow/wiki/Reporting#test-execution-report">SpecFlow Documentation</see> for more information.
        ///             </summary>
        ///             
        ///             <param name="action">The action to run SpecFlow for. Supported actions are: MSTest, NUnit3 and XUnit2</param>
        ///             <param name="projectFile">The path of the project file containing the feature files.</param>
        ///         
        public static void SpecFlowTestExecutionReport(global::System.Action<global::Cake.Core.ICakeContext> action, global::Cake.Core.IO.FilePath projectFile)
        {
        }

        ///             <summary>
        ///             Creates a formatted HTML report of a test execution.
        ///             The report contains a summary about the executed tests and the result and also a detailed report for the individual scenario executions.
        ///             See <see href="https://github.com/techtalk/SpecFlow/wiki/Reporting#test-execution-report">SpecFlow Documentation</see> for more information.
        ///             </summary>
        ///             
        ///             <param name="action">The action to run SpecFlow for. Supported actions are: MSTest, NUNit, NUNit3, XUnit and XUnit2</param>
        ///             <param name="projectFile">The path of the project file containing the feature files.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void SpecFlowTestExecutionReport(global::System.Action<global::Cake.Core.ICakeContext> action, global::Cake.Core.IO.FilePath projectFile, global::Cake.Common.Tools.SpecFlow.TestExecutionReport.SpecFlowTestExecutionReportSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.TextTransform
{
    public static class TextTransformAliasesMetadata
    {
        ///             <summary>
        ///             Transform a text template.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             // Transform a .tt template.
        ///             var transform = File("./src/Cake/Transform.tt");
        ///             TransformTemplate(transform);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="sourceFile">The source file.</param>
        ///         
        public static void TransformTemplate(global::Cake.Core.IO.FilePath sourceFile)
        {
        }

        ///             <summary>
        ///             Transform a text template.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             // Transform a .tt template.
        ///             var transform = File("./src/Cake/Transform.tt");
        ///             TransformTemplate(transform, new TextTransformSettings { OutputFile="./src/Cake/Transform.cs" });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="sourceFile">The source file.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void TransformTemplate(global::Cake.Core.IO.FilePath sourceFile, global::Cake.Common.Tools.TextTransform.TextTransformSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.VSTest
{
    public static class VSTestAliasesMetadata
    {
        ///             <summary>
        ///             Runs all VSTest unit tests in the specified assemblies.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var paths = new List&lt;FilePath&gt;() { "./assemblydir1", "./assemblydir2" };
        ///             VSTest(paths);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblyPaths">The assembly paths.</param>
        ///         
        public static void VSTest(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblyPaths)
        {
        }

        ///             <summary>
        ///             Runs all VSTest unit tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             VSTest("./Tests/*.UnitTests.dll");
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///         
        public static void VSTest(System.String pattern)
        {
        }

        ///             <summary>
        ///             Runs all VSTest unit tests in the specified assemblies.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var paths = new List&lt;FilePath&gt;() { "./assemblydir1", "./assemblydir2" };
        ///             VSTest(paths, new VSTestSettings() { InIsolation = true });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="assemblyPaths">The assembly paths.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void VSTest(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblyPaths, global::Cake.Common.Tools.VSTest.VSTestSettings settings)
        {
        }

        ///             <summary>
        ///             Runs all VSTest unit tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             VSTest("./Tests/*.UnitTests.dll", new VSTestSettings() { Logger = VSTestLogger.Trx });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void VSTest(System.String pattern, global::Cake.Common.Tools.VSTest.VSTestSettings settings)
        {
        }
    }
}

namespace Cake.Common.Tools.WiX
{
    public static class WiXAliasesMetadata
    {
        ///             <summary>
        ///             Compiles all <c>.wxs</c> sources in the provided source files.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var files = GetFiles("./src/*.wxs");
        ///             CandleSettings settings = new CandleSettings {
        ///                 Architecture = Architecture.X64,
        ///                 Verbose = true
        ///                 };
        ///             WiXCandle(files, settings);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="sourceFiles">The source files.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void WiXCandle(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> sourceFiles, global::Cake.Common.Tools.WiX.CandleSettings settings = null)
        {
        }

        ///             <summary>
        ///             Compiles all <c>.wxs</c> sources matching the specified pattern.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             CandleSettings settings = new CandleSettings {
        ///                 Architecture = Architecture.X64,
        ///                 Verbose = true
        ///                 };
        ///             WiXCandle("./src/*.wxs", settings);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The globbing pattern.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void WiXCandle(System.String pattern, global::Cake.Common.Tools.WiX.CandleSettings settings = null)
        {
        }

        ///             <summary>
        ///             Harvests from the desired files.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var harvestFile = File("./tools/Cake/Cake.Core.dll");
        ///             var filePath = File("Wix.File.wxs");
        ///             WiXHeat(harvestFile, filePath, WiXHarvestType.File);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="objectFile">The object file.</param>
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="harvestType">The WiX harvest type.</param>
        ///         
        public static void WiXHeat(global::Cake.Core.IO.FilePath objectFile, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.WiX.Heat.WiXHarvestType harvestType)
        {
        }

        ///             <summary>
        ///             Harvests files for a website or performance.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var filePath = File("Wix.Website.wxs");
        ///             WiXHeat("Default Web Site", filePath, WiXHarvestType.Website);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="harvestTarget">The harvest target.</param>
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="harvestType">The WiX harvest type.</param>
        ///         
        public static void WiXHeat(System.String harvestTarget, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.WiX.Heat.WiXHarvestType harvestType)
        {
        }

        ///             <summary>
        ///             Harvests files in the provided object files.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             DirectoryPath harvestDirectory = Directory("./src");
        ///             var filePath = new FilePath("Wix.Directory.wxs");
        ///             WiXHeat(harvestDirectory, filePath, WiXHarvestType.Dir);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="directoryPath">The object files.</param>
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="harvestType">The WiX harvest type.</param>
        ///         
        public static void WiXHeat(global::Cake.Core.IO.DirectoryPath directoryPath, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.WiX.Heat.WiXHarvestType harvestType)
        {
        }

        ///             <summary>
        ///             Harvests from the desired files.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var harvestFiles = File("./tools/Cake/*.dll");
        ///             var filePath = File("Wix.File.wxs");
        ///             WiXHeat(harvestFiles, filePath, WiXHarvestType.File, new HeatSettings { NoLogo = true });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="objectFile">The object file.</param>
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="harvestType">The WiX harvest type.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void WiXHeat(global::Cake.Core.IO.FilePath objectFile, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.WiX.Heat.WiXHarvestType harvestType, global::Cake.Common.Tools.WiX.Heat.HeatSettings settings)
        {
        }

        ///             <summary>
        ///             Harvests files for a website or performance.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var filePath = File("Wix.Website.wxs");
        ///             WiXHeat("Default Web Site", filePath, WiXHarvestType.Website, new HeatSettings { NoLogo = true });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="harvestTarget">The harvest target.</param>
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="harvestType">The WiX harvest type.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void WiXHeat(System.String harvestTarget, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.WiX.Heat.WiXHarvestType harvestType, global::Cake.Common.Tools.WiX.Heat.HeatSettings settings)
        {
        }

        ///             <summary>
        ///             Harvests files in the provided directory path.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             DirectoryPath harvestDirectory = Directory("./src");
        ///             var filePath = File("Wix.Directory.wxs");
        ///             Information(MakeAbsolute(harvestDirectory).FullPath);
        ///             WiXHeat(harvestDirectory, filePath, WiXHarvestType.Dir, new HeatSettings { NoLogo = true });
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="directoryPath">The directory path.</param>
        ///             <param name="outputFile">The output file.</param>
        ///             <param name="harvestType">The WiX harvest type.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void WiXHeat(global::Cake.Core.IO.DirectoryPath directoryPath, global::Cake.Core.IO.FilePath outputFile, global::Cake.Common.Tools.WiX.Heat.WiXHarvestType harvestType, global::Cake.Common.Tools.WiX.Heat.HeatSettings settings)
        {
        }

        ///             <summary>
        ///             Links all <c>.wixobj</c> files in the provided object files.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             var files = GetFiles("./src/*.wxs");
        ///             LightSettings settings = new LightSettings {
        ///                 RawArguments = "-O1 -pedantic -v"
        ///                 };
        ///             WiXLight(files, settings);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="objectFiles">The object files.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void WiXLight(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> objectFiles, global::Cake.Common.Tools.WiX.LightSettings settings = null)
        {
        }

        ///             <summary>
        ///             Links all <c>.wixobj</c> files matching the specified pattern.
        ///             </summary>
        ///             <example>
        ///             <code>
        ///             LightSettings settings = new LightSettings {
        ///                 RawArguments = "-O1 -pedantic -v"
        ///                 };
        ///             WiXLight("./src/*.wixobj", settings);
        ///             </code>
        ///             </example>
        ///             
        ///             <param name="pattern">The globbing pattern.</param>
        ///             <param name="settings">The settings.</param>
        ///         
        public static void WiXLight(System.String pattern, global::Cake.Common.Tools.WiX.LightSettings settings = null)
        {
        }
    }
}

namespace Cake.Common.Tools.XBuild
{
    public static class XBuildAliasesMetadata
    {
        ///             <summary>
        ///             Builds the specified solution using XBuild.
        ///             </summary>
        ///             
        ///             <param name="solution">The solution to build.</param>
        ///             <example>
        ///             <code>
        ///             XBuild("./src/Cake.sln");
        ///             </code>
        ///             </example>
        ///         
        public static void XBuild(global::Cake.Core.IO.FilePath solution)
        {
        }

        ///             <summary>
        ///             Builds the specified solution using XBuild.
        ///             </summary>
        ///             
        ///             <param name="solution">The solution to build.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             XBuild("./src/Cake.sln", new XBuildSettings {
        ///                 Verbosity = Verbosity.Minimal,
        ///                 ToolVersion = XBuildToolVersion.NET40,
        ///                 Configuration = "Release"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void XBuild(global::Cake.Core.IO.FilePath solution, global::Cake.Common.Tools.XBuild.XBuildSettings settings)
        {
        }

        ///             <summary>
        ///             Builds the specified solution using XBuild.
        ///             </summary>
        ///             
        ///             <param name="solution">The solution to build.</param>
        ///             <param name="configurator">The settings configurator.</param>
        ///             <example>
        ///             <code>
        ///             XBuild("./src/Cake.sln", configurator =&gt;
        ///                 configurator.SetConfiguration("Debug")
        ///                     .SetVerbosity(Verbosity.Minimal)
        ///                     .UseToolVersion(XBuildToolVersion.NET40));
        ///             </code>
        ///             </example>
        ///         
        public static void XBuild(global::Cake.Core.IO.FilePath solution, global::System.Action<global::Cake.Common.Tools.XBuild.XBuildSettings> configurator)
        {
        }
    }
}

namespace Cake.Common.Tools.XUnit
{
    public static class XUnit2AliasesMetadata
    {
        ///             <summary>
        ///             Runs all xUnit.net v2 tests in the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <example>
        ///             <code>
        ///             XUnit2(new []{
        ///                 "./src/Cake.Common.Tests/bin/Release/Cake.Common.Tests.dll",
        ///                 "./src/Cake.Core.Tests/bin/Release/Cake.Core.Tests.dll",
        ///                 "./src/Cake.NuGet.Tests/bin/Release/Cake.NuGet.Tests.dll",
        ///                 "./src/Cake.Tests/bin/Release/Cake.Tests.dll"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit2(global::System.Collections.Generic.IEnumerable<System.String> assemblies)
        {
        }

        ///             <summary>
        ///             Runs all xUnit.net tests in the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <example>
        ///             <code>
        ///             var testAssemblies = GetFiles("./src/**/bin/Release/*.Tests.dll");
        ///             XUnit2(testAssemblies);
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit2(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies)
        {
        }

        ///             <summary>
        ///             Runs all xUnit.net v2 tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <example>
        ///             <code>
        ///             XUnit2("./src/**/bin/Release/*.Tests.dll");
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit2(System.String pattern)
        {
        }

        ///             <summary>
        ///             Runs all xUnit.net v2 tests in the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             var testAssemblies = GetFiles("./src/**/bin/Release/*.Tests.dll");
        ///             XUnit2(testAssemblies,
        ///                  new XUnit2Settings {
        ///                     Parallelism = ParallelismOption.All,
        ///                     HtmlReport = true,
        ///                     NoAppDomain = true,
        ///                     OutputDirectory = "./build"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit2(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies, global::Cake.Common.Tools.XUnit.XUnit2Settings settings)
        {
        }

        ///             <summary>
        ///             Runs all xUnit.net v2 tests in the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             XUnit2(new []{
        ///                 "./src/Cake.Common.Tests/bin/Release/Cake.Common.Tests.dll",
        ///                 "./src/Cake.Core.Tests/bin/Release/Cake.Core.Tests.dll",
        ///                 "./src/Cake.NuGet.Tests/bin/Release/Cake.NuGet.Tests.dll",
        ///                 "./src/Cake.Tests/bin/Release/Cake.Tests.dll"
        ///                  },
        ///                  new XUnit2Settings {
        ///                     Parallelism = ParallelismOption.All,
        ///                     HtmlReport = true,
        ///                     NoAppDomain = true,
        ///                     OutputDirectory = "./build"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit2(global::System.Collections.Generic.IEnumerable<System.String> assemblies, global::Cake.Common.Tools.XUnit.XUnit2Settings settings)
        {
        }

        ///             <summary>
        ///             Runs all xUnit.net v2 tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             XUnit2("./src/**/bin/Release/*.Tests.dll",
        ///                  new XUnit2Settings {
        ///                     Parallelism = ParallelismOption.All,
        ///                     HtmlReport = true,
        ///                     NoAppDomain = true,
        ///                     OutputDirectory = "./build"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit2(System.String pattern, global::Cake.Common.Tools.XUnit.XUnit2Settings settings)
        {
        }
    }

    public static class XUnitAliasesMetadata
    {
        ///             <summary>
        ///             Runs all xUnit.net tests in the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <example>
        ///             <code>
        ///             XUnit(new []{
        ///                 "./src/Cake.Common.Tests/bin/Release/Cake.Common.Tests.dll",
        ///                 "./src/Cake.Core.Tests/bin/Release/Cake.Core.Tests.dll",
        ///                 "./src/Cake.NuGet.Tests/bin/Release/Cake.NuGet.Tests.dll",
        ///                 "./src/Cake.Tests/bin/Release/Cake.Tests.dll"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit(global::System.Collections.Generic.IEnumerable<System.String> assemblies)
        {
        }

        ///             <summary>
        ///             Runs all xUnit.net tests in the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <example>
        ///             <code>
        ///             var testAssemblies = GetFiles("./src/**/bin/Release/*.Tests.dll");
        ///             XUnit(testAssemblies);
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies)
        {
        }

        ///             <summary>
        ///             Runs all xUnit.net tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <example>
        ///             <code>
        ///             XUnit("./src/**/bin/Release/*.Tests.dll");
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit(System.String pattern)
        {
        }

        ///             <summary>
        ///             Runs all xUnit.net tests in the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             var testAssemblies = GetFiles("./src/**/bin/Release/*.Tests.dll");
        ///             XUnit(testAssemblies,
        ///                  new XUnitSettings {
        ///                     HtmlReport = true,
        ///                     OutputDirectory = "./build"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit(global::System.Collections.Generic.IEnumerable<global::Cake.Core.IO.FilePath> assemblies, global::Cake.Common.Tools.XUnit.XUnitSettings settings)
        {
        }

        ///             <summary>
        ///             Runs all xUnit.net tests in the specified assemblies.
        ///             </summary>
        ///             
        ///             <param name="assemblies">The assemblies.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             XUnit(new []{
        ///                 "./src/Cake.Common.Tests/bin/Release/Cake.Common.Tests.dll",
        ///                 "./src/Cake.Core.Tests/bin/Release/Cake.Core.Tests.dll",
        ///                 "./src/Cake.NuGet.Tests/bin/Release/Cake.NuGet.Tests.dll",
        ///                 "./src/Cake.Tests/bin/Release/Cake.Tests.dll"
        ///                  },
        ///                  new XUnitSettings {
        ///                     HtmlReport = true,
        ///                     OutputDirectory = "./build"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit(global::System.Collections.Generic.IEnumerable<System.String> assemblies, global::Cake.Common.Tools.XUnit.XUnitSettings settings)
        {
        }

        ///             <summary>
        ///             Runs all xUnit.net tests in the assemblies matching the specified pattern.
        ///             </summary>
        ///             
        ///             <param name="pattern">The pattern.</param>
        ///             <param name="settings">The settings.</param>
        ///             <example>
        ///             <code>
        ///             XUnit("./src/**/bin/Release/*.Tests.dll",
        ///                  new XUnitSettings {
        ///                     HtmlReport = true,
        ///                     OutputDirectory = "./build"
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static void XUnit(System.String pattern, global::Cake.Common.Tools.XUnit.XUnitSettings settings)
        {
        }
    }
}

namespace Cake.Common.Xml
{
    public static class XmlPeekAliasesMetadata
    {
        ///             <summary>
        ///             Gets the value of a target node.
        ///             </summary>
        ///             <returns>The value found at the given XPath query.</returns>
        ///             
        ///             <param name="filePath">The target file.</param>
        ///             <param name="xpath">The xpath of the node to get.</param>
        ///             <example>
        ///             <code>
        ///             string autoFacVersion = XmlPeek("./src/Cake/packages.config", "/packages/package[@id='Autofac']/@version");
        ///             </code>
        ///             </example>
        ///         
        public static System.String XmlPeek(global::Cake.Core.IO.FilePath filePath, System.String xpath)
        {
            return default(System.String);
        }

        ///             <summary>
        ///             Get the value of a target node.
        ///             </summary>
        ///             <returns>The value found at the given XPath query.</returns>
        ///             
        ///             <param name="filePath">The target file.</param>
        ///             <param name="xpath">The xpath of the nodes to set.</param>
        ///             <param name="settings">Additional settings to tweak Xml Peek behavior.</param>
        ///             <example>
        ///             <code>
        ///             <para>XML document:</para>
        ///             <![CDATA[
        ///             <?xml version="1.0" encoding="UTF-8"?>
        ///             <pastery xmlns = "http://cakebuild.net/pastery" >
        ///                 < cake price="1.62" />
        ///             </pastery>
        ///             ]]>
        ///             </code>
        ///             <para>XmlPeek usage:</para>
        ///             <code>
        ///             string version = XmlPeek("./pastery.xml", "/pastery:pastery/pastery:cake/@price",
        ///                 new XmlPeekSettings {
        ///                     Namespaces = new Dictionary&lt;string, string&gt; {{ "pastery", "http://cakebuild.net/pastery" }}
        ///                 });
        ///             string unknown = XmlPeek("./pastery.xml", "/pastery:pastery/pastery:cake/@recipe",
        ///                 new XmlPeekSettings {
        ///                     Namespaces = new Dictionary&lt;string, string&gt; {{ "pastery", "http://cakebuild.net/pastery" }},
        ///                     SuppressWarnings = true
        ///                 });
        ///             </code>
        ///             </example>
        ///         
        public static System.String XmlPeek(global::Cake.Core.IO.FilePath filePath, System.String xpath, global::Cake.Common.Xml.XmlPeekSettings settings)
        {
            return default(System.String);
        }
    }

    public static class XmlPokeAliasesMetadata
    {
        ///             <summary>
        ///             Set the value of, or remove, target nodes.
        ///             </summary>
        ///             
        ///             <param name="filePath">The target file.</param>
        ///             <param name="xpath">The xpath of the nodes to set.</param>
        ///             <param name="value">The value to set too. Leave blank to remove the selected nodes.</param>
        ///             <example>
        ///               <para>
        ///               Change the <c>server</c> setting in the configuration from <c>testhost.somecompany.com</c>
        ///               to <c>productionhost.somecompany.com</c>.
        ///               </para>
        ///               <para>XML file:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <configuration>
        ///                 <appSettings>
        ///                     <add key="server" value="testhost.somecompany.com" />
        ///                 </appSettings>
        ///             </configuration>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var file = File("test.xml");
        ///                 XmlPoke(file, "/configuration/appSettings/add[@key = 'server']/@value", "productionhost.somecompany.com");
        ///             });
        ///                 ]]>
        ///               </code>
        ///             </example>
        ///             <example>
        ///               <para>
        ///               Modify the <c>noNamespaceSchemaLocation</c> in an XML file.
        ///               </para>
        ///               <para>XML file:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <Commands xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="Path Value">
        ///             </Commands>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var file = File("test.xml");
        ///                 XmlPoke(file, "/Commands/@xsi:noNamespaceSchemaLocation", "d:\\Commands.xsd", new XmlPokeSettings {
        ///                     Namespaces = new Dictionary<string, string> {
        ///                         { /* Prefix */ "xsi", /* URI */ "http://www.w3.org/2001/XMLSchema-instance" }
        ///                     }
        ///                 });
        ///             });
        ///                 ]]>
        ///               </code>
        ///             <example>
        ///               <para>
        ///               Remove an app setting from a config file.
        ///               </para>
        ///               <para>XML file:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <configuration>
        ///                 <appSettings>
        ///                     <add key="server" value="testhost.somecompany.com" />
        ///                     <add key="testing" value="true" />
        ///                 </appSettings>
        ///             </configuration>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var file = File("test.xml");
        ///                 XmlPoke(file, "/configuration/appSettings/add[@testing]", null);
        ///             });
        ///                 ]]>
        ///               </code>
        ///             </example>
        ///             <para>
        ///             Credit to NAnt for the original example.
        ///             http://nant.sourceforge.net/release/latest/help/tasks/xmlpoke.html
        ///             </para>
        ///             </example>
        ///         
        public static void XmlPoke(global::Cake.Core.IO.FilePath filePath, System.String xpath, System.String value)
        {
        }

        ///             <summary>
        ///             Set the value of, or remove, target nodes.
        ///             </summary>
        ///             
        ///             <param name="filePath">The target file.</param>
        ///             <param name="xpath">The xpath of the nodes to set.</param>
        ///             <param name="value">The value to set too. Leave blank to remove the selected nodes.</param>
        ///             <param name="settings">Additional settings to tweak Xml Poke behavior.</param>
        ///             <example>
        ///               <para>
        ///               Change the <c>server</c> setting in the configuration from <c>testhost.somecompany.com</c>
        ///               to <c>productionhost.somecompany.com</c>.
        ///               </para>
        ///               <para>XML file:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <configuration>
        ///                 <appSettings>
        ///                     <add key="server" value="testhost.somecompany.com" />
        ///                 </appSettings>
        ///             </configuration>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var file = File("test.xml");
        ///                 XmlPoke(file, "/configuration/appSettings/add[@key = 'server']/@value", "productionhost.somecompany.com");
        ///             });
        ///                 ]]>
        ///               </code>
        ///             </example>
        ///             <example>
        ///               <para>
        ///               Modify the <c>noNamespaceSchemaLocation</c> in an XML file.
        ///               </para>
        ///               <para>XML file:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <Commands xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="Path Value">
        ///             </Commands>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var file = File("test.xml");
        ///                 XmlPoke(file, "/Commands/@xsi:noNamespaceSchemaLocation", "d:\\Commands.xsd", new XmlPokeSettings {
        ///                     Namespaces = new Dictionary<string, string> {
        ///                         { /* Prefix */ "xsi", /* URI */ "http://www.w3.org/2001/XMLSchema-instance" }
        ///                     }
        ///                 });
        ///             });
        ///                 ]]>
        ///               </code>
        ///             <example>
        ///               <para>
        ///               Remove an app setting from a config file.
        ///               </para>
        ///               <para>XML file:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <configuration>
        ///                 <appSettings>
        ///                     <add key="server" value="testhost.somecompany.com" />
        ///                     <add key="testing" value="true" />
        ///                 </appSettings>
        ///             </configuration>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var file = File("test.xml");
        ///                 XmlPoke(file, "/configuration/appSettings/add[@testing]", null);
        ///             });
        ///                 ]]>
        ///               </code>
        ///             </example>
        ///             <para>
        ///             Credit to NAnt for the original example.
        ///             http://nant.sourceforge.net/release/latest/help/tasks/xmlpoke.html
        ///             </para>
        ///             </example>
        ///         
        public static void XmlPoke(global::Cake.Core.IO.FilePath filePath, System.String xpath, System.String value, global::Cake.Common.Xml.XmlPokeSettings settings)
        {
        }

        ///             <summary>
        ///             Set the value of, or remove, target nodes.
        ///             </summary>
        ///             
        ///             <param name="sourceXml">The source xml to transform.</param>
        ///             <param name="xpath">The xpath of the nodes to set.</param>
        ///             <param name="value">The value to set too. Leave blank to remove the selected nodes.</param>
        ///             <returns>Resulting XML.</returns>
        ///             <example>
        ///               <para>
        ///               Change the <c>server</c> setting in the configuration from <c>testhost.somecompany.com</c>
        ///               to <c>productionhost.somecompany.com</c>.
        ///               </para>
        ///               <para>XML string:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <configuration>
        ///                 <appSettings>
        ///                     <add key="server" value="testhost.somecompany.com" />
        ///                 </appSettings>
        ///             </configuration>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var result = XmlPokeString(xmlString, "/configuration/appSettings/add[@key = 'server']/@value", "productionhost.somecompany.com");
        ///             });
        ///                 ]]>
        ///               </code>
        ///             </example>
        ///             <example>
        ///               <para>
        ///               Modify the <c>noNamespaceSchemaLocation</c> in an XML file.
        ///               </para>
        ///               <para>XML string:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <Commands xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="Path Value">
        ///             </Commands>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var result = XmlPokeString(xmlString, "/Commands/@xsi:noNamespaceSchemaLocation", "d:\\Commands.xsd", new XmlPokeSettings {
        ///                     Namespaces = new Dictionary<string, string> {
        ///                         { /* Prefix */ "xsi", /* URI */ "http://www.w3.org/2001/XMLSchema-instance" }
        ///                     }
        ///                 });
        ///             });
        ///                 ]]>
        ///               </code>
        ///             <example>
        ///               <para>
        ///               Remove an app setting from a config file.
        ///               </para>
        ///               <para>XML string:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <configuration>
        ///                 <appSettings>
        ///                     <add key="server" value="testhost.somecompany.com" />
        ///                     <add key="testing" value="true" />
        ///                 </appSettings>
        ///             </configuration>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var result = XmlPokeString(xmlString, "/configuration/appSettings/add[@testing]", null);
        ///             });
        ///                 ]]>
        ///               </code>
        ///             </example>
        ///             <para>
        ///             Credit to NAnt for the original example.
        ///             http://nant.sourceforge.net/release/latest/help/tasks/xmlpoke.html
        ///             </para>
        ///             </example>
        ///         
        public static System.String XmlPokeString(System.String sourceXml, System.String xpath, System.String value)
        {
            return default(System.String);
        }

        ///             <summary>
        ///             Set the value of, or remove, target nodes.
        ///             </summary>
        ///             
        ///             <param name="sourceXml">The source xml to transform.</param>
        ///             <param name="xpath">The xpath of the nodes to set.</param>
        ///             <param name="value">The value to set too. Leave blank to remove the selected nodes.</param>
        ///             <param name="settings">Additional settings to tweak Xml Poke behavior.</param>
        ///             <returns>Resulting XML.</returns>
        ///             <example>
        ///               <para>
        ///               Change the <c>server</c> setting in the configuration from <c>testhost.somecompany.com</c>
        ///               to <c>productionhost.somecompany.com</c>.
        ///               </para>
        ///               <para>XML string:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <configuration>
        ///                 <appSettings>
        ///                     <add key="server" value="testhost.somecompany.com" />
        ///                 </appSettings>
        ///             </configuration>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var result = XmlPokeString(xmlString, "/configuration/appSettings/add[@key = 'server']/@value", "productionhost.somecompany.com");
        ///             });
        ///                 ]]>
        ///               </code>
        ///             </example>
        ///             <example>
        ///               <para>
        ///               Modify the <c>noNamespaceSchemaLocation</c> in an XML file.
        ///               </para>
        ///               <para>XML string:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <Commands xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="Path Value">
        ///             </Commands>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var result = XmlPokeString(xmlString, "/Commands/@xsi:noNamespaceSchemaLocation", "d:\\Commands.xsd", new XmlPokeSettings {
        ///                     Namespaces = new Dictionary<string, string> {
        ///                         { /* Prefix */ "xsi", /* URI */ "http://www.w3.org/2001/XMLSchema-instance" }
        ///                     }
        ///                 });
        ///             });
        ///                 ]]>
        ///               </code>
        ///             <example>
        ///               <para>
        ///               Remove an app setting from a config file.
        ///               </para>
        ///               <para>XML string:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             <?xml version="1.0" encoding="utf-8" ?>
        ///             <configuration>
        ///                 <appSettings>
        ///                     <add key="server" value="testhost.somecompany.com" />
        ///                     <add key="testing" value="true" />
        ///                 </appSettings>
        ///             </configuration>
        ///                 ]]>
        ///               </code>
        ///               <para>Cake Task:</para>
        ///               <code>
        ///                 <![CDATA[
        ///             Task("Transform")
        ///                 .Does(() =>
        ///             {
        ///                 var result = XmlPokeString(xmlString, "/configuration/appSettings/add[@testing]", null);
        ///             });
        ///                 ]]>
        ///               </code>
        ///             </example>
        ///             <para>
        ///             Credit to NAnt for the original example.
        ///             http://nant.sourceforge.net/release/latest/help/tasks/xmlpoke.html
        ///             </para>
        ///             </example>
        ///         
        public static System.String XmlPokeString(System.String sourceXml, System.String xpath, System.String value, global::Cake.Common.Xml.XmlPokeSettings settings)
        {
            return default(System.String);
        }
    }

    public static class XmlTransformationAliasMetadata
    {
        ///              <summary>
        ///              Performs XML XSL transformation
        ///              </summary>
        ///              
        ///              <param name="xsl">XML style sheet.</param>
        ///              <param name="xml">XML data.</param>
        ///              <returns>Transformed XML string.</returns>
        ///              <example>
        ///              <code>
        ///              <para>This example code will convert xml to a new xml strucure using XmlTransform alias.</para>
        ///              <![CDATA[
        ///              string xsl = @"<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
        ///                <xsl:output method=""xml"" omit-xml-declaration=""yes"" />
        ///                <xsl:template match=""/"">
        ///                  <xsl:for-each select=""pastery/cake"" >
        ///                      <price><xsl:value-of select=""@price""/></price>
        ///                    </xsl:for-each>
        ///                </xsl:template>
        ///              </xsl:stylesheet>";
        ///             
        ///              string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
        ///              <pastery>
        ///                  <cake price=""1.62"" />
        ///              </pastery>";
        ///             
        ///              var priceTag = XmlTransform(xsl, xml);
        ///              ]]>
        ///              </code>
        ///              <para>Result:</para>
        ///              <code>
        ///              <![CDATA[<price>1.62</price>]]>
        ///              </code>
        ///              </example>
        ///         
        public static System.String XmlTransform(System.String xsl, System.String xml)
        {
            return default(System.String);
        }

        ///             <summary>
        ///             Performs XML XSL transformation
        ///             </summary>
        ///             
        ///             <param name="xslPath">Path to xml style sheet.</param>
        ///             <param name="xmlPath">Path xml data.</param>
        ///             <param name="resultPath">Transformation result path, will overwrite if exists.</param>
        ///             <example>
        ///             <code>
        ///             <para>This example code will convert the Cake nuspec into html using the XmlTransform alias.</para>
        ///             <para>XML stylesheet:</para>
        ///             <![CDATA[
        ///             <?xml version="1.0" ?>
        ///             <xsl:stylesheet
        ///               version="1.0"
        ///               xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
        ///               xmlns:p="http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd"
        ///               exclude-result-prefixes="p"
        ///               >
        ///               <xsl:output method="xml" indent="yes" omit-xml-declaration="yes" />
        ///               <xsl:template match="/">
        ///                 <html lang="en" class="static">
        ///                   <head>
        ///                     <title>
        ///                       <xsl:for-each select="package/p:metadata">
        ///                           <xsl:value-of select="p:id"/>
        ///                       </xsl:for-each>
        ///                     </title>
        ///                   </head>
        ///                   <body>
        ///                       <xsl:for-each select="package/p:metadata">
        ///                         <h1>
        ///                           <xsl:value-of select="p:id"/>
        ///                         </h1>
        ///                         <h2>Description</h2>
        ///                         <i><xsl:value-of select="p:description"/></i>
        ///                       </xsl:for-each>
        ///                     <h3>Files</h3>
        ///                     <ul>
        ///                       <xsl:for-each select="package/files/file" >
        ///                         <li><xsl:value-of select="@src"/></li>
        ///                       </xsl:for-each>
        ///                     </ul>
        ///                   </body>
        ///                 </html>
        ///               </xsl:template>
        ///             </xsl:stylesheet>
        ///             ]]>
        ///             </code>
        ///             <para>XmlTransform usage:</para>
        ///             <code>
        ///             XmlTransform("./nuspec.xsl", "./nuspec/Cake.nuspec", "./Cake.htm");
        ///             </code>
        ///             </example>
        ///         
        public static void XmlTransform(global::Cake.Core.IO.FilePath xslPath, global::Cake.Core.IO.FilePath xmlPath, global::Cake.Core.IO.FilePath resultPath)
        {
        }

        ///              <summary>
        ///              Performs XML XSL transformation
        ///              </summary>
        ///              
        ///              <param name="xsl">XML style sheet.</param>
        ///              <param name="xml">XML data.</param>
        ///              <param name="settings">Optional settings for result file xml writer</param>
        ///              <returns>Transformed XML string.</returns>
        ///              <example>
        ///              <code>
        ///              <para>This example code will convert specific part of xml to plaintext using XmlTransform alias.</para>
        ///              <![CDATA[string xsl = @"<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
        ///                <xsl:output method=""text"" omit-xml-declaration=""yes"" indent=""no""/>
        ///                <xsl:strip-space elements=""*""/>
        ///                <xsl:template match=""pastery/cake""><xsl:value-of select=""@price""/></xsl:template>
        ///              </xsl:stylesheet>";
        ///             
        ///              string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
        ///              <pastery>
        ///                  <cake price=""1.62"" />
        ///              </pastery>";
        ///             
        ///              var text = XmlTransform(xsl, xml, new XmlTransformationSettings {
        ///                  ConformanceLevel = System.Xml.ConformanceLevel.Fragment, Encoding = Encoding.ASCII });
        ///              ]]>
        ///              </code>
        ///              </example>
        ///         
        public static System.String XmlTransform(System.String xsl, System.String xml, global::Cake.Common.Xml.XmlTransformationSettings settings)
        {
            return default(System.String);
        }

        ///             <summary>
        ///             Performs XML XSL transformation
        ///             </summary>
        ///             
        ///             <param name="xslPath">Path to xml style sheet.</param>
        ///             <param name="xmlPath">Path xml data.</param>
        ///             <param name="resultPath">Transformation result path.</param>
        ///             <param name="settings">Optional settings for result file xml writer</param>
        ///             <example>
        ///             <code>
        ///             <para>This example code will convert the Cake nuspec into html using the XmlTransform alias,
        ///             specifying that result should be indented and using Unicode encoding.</para>
        ///             <para>XML stylesheet:</para>
        ///             <![CDATA[
        ///             <?xml version="1.0" ?>
        ///             <xsl:stylesheet
        ///               version="1.0"
        ///               xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
        ///               xmlns:p="http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd"
        ///               exclude-result-prefixes="p"
        ///               >
        ///               <xsl:output method="xml" indent="yes" omit-xml-declaration="yes" />
        ///               <xsl:template match="/">
        ///                 <html lang="en" class="static">
        ///                   <head>
        ///                     <title>
        ///                       <xsl:for-each select="package/p:metadata">
        ///                           <xsl:value-of select="p:id"/>
        ///                       </xsl:for-each>
        ///                     </title>
        ///                   </head>
        ///                   <body>
        ///                       <xsl:for-each select="package/p:metadata">
        ///                         <h1>
        ///                           <xsl:value-of select="p:id"/>
        ///                         </h1>
        ///                         <h2>Description</h2>
        ///                         <i><xsl:value-of select="p:description"/></i>
        ///                       </xsl:for-each>
        ///                     <h3>Files</h3>
        ///                     <ul>
        ///                       <xsl:for-each select="package/files/file" >
        ///                         <li><xsl:value-of select="@src"/></li>
        ///                       </xsl:for-each>
        ///                     </ul>
        ///                   </body>
        ///                 </html>
        ///               </xsl:template>
        ///             </xsl:stylesheet>
        ///             ]]>
        ///             </code>
        ///             <para>XmlTransform usage:</para>
        ///             <code>
        ///             XmlTransform("./nuspec.xsl", "./nuspec/Cake.nuspec", "./Cake.htm",
        ///                 new XmlTransformationSettings { Indent = true, Encoding = Encoding.Unicode});
        ///             </code>
        ///             </example>
        ///         
        public static void XmlTransform(global::Cake.Core.IO.FilePath xslPath, global::Cake.Core.IO.FilePath xmlPath, global::Cake.Core.IO.FilePath resultPath, global::Cake.Common.Xml.XmlTransformationSettings settings)
        {
        }
    }
}