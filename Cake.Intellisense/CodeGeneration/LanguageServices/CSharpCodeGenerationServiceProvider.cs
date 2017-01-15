using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Host;

namespace Cake.MetadataGenerator.CodeGeneration.LanguageServices
{
    public class CSharpCodeGenerationServiceProvider : ICSharpCodeGenerationServiceProvider
    {
        public ILanguageService Get()
        {
            var project = new AdhocWorkspace().AddSolution(SolutionInfo.Create(SolutionId.CreateNewId("MetadataGenera"), VersionStamp.Default))
                                              .AddProject("MyProject", "MyAssemblyName", LanguageNames.CSharp);

            var hostLanguageServices = project.Solution.Projects.First().LanguageServices;

            var host = nameof(hostLanguageServices.GetService);
            var iservice = typeof(ILanguageService).Assembly.GetType("Microsoft.CodeAnalysis.CodeGeneration.ICodeGenerationService");

            var cos = hostLanguageServices.GetType().GetMethod(host);
            var invoke = cos.MakeGenericMethod(iservice).Invoke(hostLanguageServices, null);
            return (ILanguageService)invoke;
        }
    }
}