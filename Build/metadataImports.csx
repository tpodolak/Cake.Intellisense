#r "./tools/Cake/Cake.Core.dll"
#r "./tools/Cake/Cake.Common.dll"
#r "./tools/Cake/Cake.Powershell.dll"
#r "./tools/Cake/Cake.Core.Metadata.dll"
#r "./tools/Cake/Cake.Common.Metadata.dll"
#r "./tools/Cake/Cake.PowerShell.AliasesMetadata.dll"
#r "./tools/Cake/System.Management.Automation.dll"

using static Cake.Common.ArgumentAliases;
using static Cake.Common.ArgumentAliasesMetadata;
using static Cake.Common.EnvironmentAliasesMetadata;
using static Cake.Common.ProcessAliasesMetadata;
using static Cake.Common.ReleaseNotesAliasesMetadata;
using static Cake.Common.Xml.XmlPeekAliasesMetadata;
using static Cake.Common.Xml.XmlPokeAliasesMetadata;
using static Cake.Common.Xml.XmlTransformationAliasMetadata;
using static Cake.Common.Security.SecurityAliasesMetadata;
using static Cake.Common.Tools.DotNetBuildAliasesMetadata;
using static Cake.Common.Tools.VSTest.VSTestAliasesMetadata;
using static Cake.Common.Tools.TextTransform.TextTransformAliasesMetadata;
using static Cake.Common.Tools.SpecFlow.SpecFlowAliasesMetadata;
using static Cake.Common.Tools.ReportUnit.ReportUnitAliasesMetadata;
using static Cake.Common.Tools.ReportGenerator.ReportGeneratorAliasesMetadata;
using static Cake.Common.Tools.OpenCover.OpenCoverAliasesMetadata;
using static Cake.Common.Tools.NSIS.NSISAliasesMetadata;
using static Cake.Common.Tools.InspectCode.InspectCodeAliasesMetadata;
using static Cake.Common.Tools.InnoSetup.InnoSetupAliasesMetadata;
using static Cake.Common.Tools.ILRepack.ILRepackAliasesMetadata;
using static Cake.Common.Tools.GitVersion.GitVersionAliasesMetadata;
using static Cake.Common.Tools.GitReleaseNotes.GitReleaseNotesAliasesMetadata;
using static Cake.Common.Tools.GitReleaseManager.GitReleaseManagerAliasesMetadata;
using static Cake.Common.Tools.GitLink.GitLinkAliasesMetadata;
using static Cake.Common.Tools.Fixie.FixieAliasesMetadata;
using static Cake.Common.Tools.DupFinder.DupFinderAliasesMetadata;
using static Cake.Common.Tools.DotNetCore.DotNetCoreAliasesMetadata;
using static Cake.Common.Tools.DotCover.DotCoverAliasesMetadata;
using static Cake.Common.Tools.DNU.DNUAliasesMetadata;
using static Cake.Common.Tools.Chocolatey.ChocolateyAliasesMetadata;
using static Cake.Common.Tools.XUnit.XUnit2AliasesMetadata;
using static Cake.Common.Tools.XUnit.XUnitAliasesMetadata;
using static Cake.Common.Tools.XBuild.XBuildAliasesMetadata;
using static Cake.Common.Tools.WiX.WiXAliasesMetadata;
using static Cake.Common.Tools.SignTool.SignToolSignAliasesMetadata;
using static Cake.Common.Tools.Roundhouse.RoundhouseAliasesMetadata;
using static Cake.Common.Tools.OctopusDeploy.OctopusDeployAliasesMetadata;
using static Cake.Common.Tools.NUnit.NUnit3AliasesMetadata;
using static Cake.Common.Tools.NUnit.NUnitAliasesMetadata;
using static Cake.Common.Tools.NuGet.NuGetAliasesMetadata;
using static Cake.Common.Tools.MSTest.MSTestAliasesMetadata;
using static Cake.Common.Tools.MSBuild.MSBuildAliasesMetadata;
using static Cake.Common.Tools.ILMerge.ILMergeAliasesMetadata;
using static Cake.Common.Tools.Cake.CakeAliasesMetadata;
using static Cake.Common.Text.TextTransformationAliasesMetadata;
using static Cake.Common.Solution.SolutionAliasesMetadata;
using static Cake.Common.Solution.Project.ProjectAliasesMetadata;
using static Cake.Common.Solution.Project.XmlDoc.XmlDocAliasesMetadata;
using static Cake.Common.Solution.Project.Properties.AssemblyInfoAliasesMetadata;
using static Cake.Common.Net.HttpAliasesMetadata;
using static Cake.Common.IO.DirectoryAliasesMetadata;
using static Cake.Common.IO.FileAliasesMetadata;
using static Cake.Common.IO.GlobbingAliasesMetadata;
using static Cake.Common.IO.ZipAliasesMetadata;
using static Cake.Common.Diagnostics.LoggingAliasesMetadata;
using static Cake.Common.Build.BuildSystemAliasesMetadata;
using static Cake.Core.Scripting.ScriptHostMetadata;
using static Cake.Powershell.PowershellAliasesMetadata;
using static Cake.Common.Tools.DNU.DNUAliasesMetadata;
Cake.Core.ICakeContext Context;