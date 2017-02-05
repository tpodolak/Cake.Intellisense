using System.Collections.Generic;
using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices;
using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters;
using Xunit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.Tests.Unit.SyntaxRewriterServicesTests
{
    public sealed class CakeSyntaxRewriterServiceTests
    {
        public sealed class RewriteMethod : Test<CakeSyntaxRewriterService>
        {
            [Fact]
            public void ShouldCallRewritersInProperOrder()
            {
                var rewriters = FakeOf<IEnumerable<ISyntaxRewriterService>>();

                Subject.Rewrite(CompilationUnit(), GetType().Assembly);

            }
        }
    }
}