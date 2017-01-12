[global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null)]
public static class ArgumentAliases : global::System.Object
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static T Argument<T>(System.String name)
    {
        return default(T);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static T Argument<T>(System.String name, T defaultValue)
    {
        return default(T);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.Boolean HasArgument(System.String name)
    {
        return default(System.Boolean);
    }
}

[global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null)]
public static class EnvironmentAliases : global::System.Object
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null), global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.String EnvironmentVariable(System.String variable)
    {
        return default(System.String);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null), global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static global::System.Collections.Generic.IDictionary<System.String, System.String> EnvironmentVariables()
    {
        return default(global::System.Collections.Generic.IDictionary<System.String, System.String>);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null), global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.Boolean HasEnvironmentVariable(System.String variable)
    {
        return default(System.Boolean);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null), global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.Boolean IsRunningOnUnix()
    {
        return default(System.Boolean);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null), global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.Boolean IsRunningOnWindows()
    {
        return default(System.Boolean);
    }
}

[global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null)]
public static class ProcessAliases : global::System.Object
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static global::Cake.Core.IO.IProcess StartAndReturnProcess(global::Cake.Core.IO.FilePath fileName)
    {
        return default(global::Cake.Core.IO.IProcess);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static global::Cake.Core.IO.IProcess StartAndReturnProcess(global::Cake.Core.IO.FilePath fileName, global::Cake.Core.IO.ProcessSettings settings)
    {
        return default(global::Cake.Core.IO.IProcess);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.Int32 StartProcess(global::Cake.Core.IO.FilePath fileName)
    {
        return default(System.Int32);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.Int32 StartProcess(global::Cake.Core.IO.FilePath fileName, global::Cake.Core.IO.ProcessSettings settings)
    {
        return default(System.Int32);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.Int32 StartProcess(global::Cake.Core.IO.FilePath fileName, System.String processArguments)
    {
        return default(System.Int32);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.Int32 StartProcess(global::Cake.Core.IO.FilePath fileName, global::Cake.Core.IO.ProcessSettings settings, out global::System.Collections.Generic.IEnumerable<System.String> redirectedOutput)
    {
        redirectedOutput = default(global::System.Collections.Generic.IEnumerable<System.String>);
        return default(System.Int32);
    }
}

[global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null)]
public static class ReleaseNotesAliases : global::System.Object
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static global::System.Collections.Generic.IReadOnlyList<global::Cake.Common.ReleaseNotes> ParseAllReleaseNotes(global::Cake.Core.IO.FilePath filePath)
    {
        return default(global::System.Collections.Generic.IReadOnlyList<global::Cake.Common.ReleaseNotes>);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static global::Cake.Common.ReleaseNotes ParseReleaseNotes(global::Cake.Core.IO.FilePath filePath)
    {
        return default(global::Cake.Common.ReleaseNotes);
    }
}