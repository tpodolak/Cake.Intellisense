using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CSharpExtensions = Microsoft.CodeAnalysis.CSharp.CSharpExtensions;

namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriters
{
    public class ClassSyntaxRewriter : CSharpSyntaxRewriter
    {
        private static readonly List<SyntaxKind> TabuModifiers = new List<SyntaxKind>
        {
            SyntaxKind.ProtectedKeyword,
            SyntaxKind.PrivateKeyword,
            SyntaxKind.InternalKeyword
        };

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node.Modifiers.All(val => CSharpExtensions.Kind((SyntaxToken) val) != SyntaxKind.PublicKeyword))
                return null;

            node = node.WithIdentifier(SyntaxFactory.Identifier(node.Identifier.Text + "Metadata"));
            return base.VisitClassDeclaration(node);
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.Modifiers.Any(modifier => TabuModifiers.Any(tabu => tabu == modifier.Kind())))
                return null;

            return base.VisitMethodDeclaration(node);
        }
    }
}
