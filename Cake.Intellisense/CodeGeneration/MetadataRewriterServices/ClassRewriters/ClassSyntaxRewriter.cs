using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.ClassRewriters
{
    internal class ClassSyntaxRewriter : CSharpSyntaxRewriter
    {
        private readonly string classSuffix;

        private static readonly List<SyntaxKind> TabuModifiers = new List<SyntaxKind>
        {
            SyntaxKind.ProtectedKeyword,
            SyntaxKind.PrivateKeyword,
            SyntaxKind.InternalKeyword
        };

        public ClassSyntaxRewriter(string classSuffix)
        {
            this.classSuffix = classSuffix;
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitBaseList(BaseListSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node.Modifiers.All(val => val.Kind() != SyntaxKind.PublicKeyword))
                return null;

            var modifierTokens = new List<SyntaxToken>
            {
                SyntaxFactory.Token(SyntaxKind.PublicKeyword)
            };

            if (node.Modifiers.Any(val => val.Kind() == SyntaxKind.StaticKeyword))
                modifierTokens.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));

            node = node.WithModifiers(SyntaxFactory.TokenList(modifierTokens))
                       .WithIdentifier(SyntaxFactory.Identifier(node.Identifier.Text + classSuffix));

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