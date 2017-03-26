using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.ClassRewriters
{
    internal class ClassSyntaxRewriter : CSharpSyntaxRewriter
    {
        private static readonly List<SyntaxKind> TabuModifiers = new List<SyntaxKind>
        {
            SyntaxKind.ProtectedKeyword,
            SyntaxKind.PrivateKeyword,
            SyntaxKind.InternalKeyword
        };

        private readonly string _classSuffix;

        public ClassSyntaxRewriter(string classSuffix)
        {
            _classSuffix = classSuffix ?? throw new ArgumentNullException(nameof(classSuffix));
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
                Token(SyntaxKind.PublicKeyword)
            };

            if (node.Modifiers.Any(val => val.Kind() == SyntaxKind.StaticKeyword))
                modifierTokens.Add(Token(SyntaxKind.StaticKeyword));

            node = node.WithModifiers(TokenList(modifierTokens))
                       .WithIdentifier(Identifier(node.Identifier.Text + _classSuffix));

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
