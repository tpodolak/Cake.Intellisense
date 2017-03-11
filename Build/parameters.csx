#load "./imports.csx"
public class BuildParameters
{
    public string Target { get; private set; }
    public string Configuration { get; private set; }

    public bool SkipOpenCover { get; set; }
    public static BuildParameters GetParameters(ICakeContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        return new BuildParameters {
            Target = context.Argument("target", "Build"),
            Configuration = context.Argument("configuration", "Debug"),
            SkipOpenCover = context.Argument<bool>("SkipOpenCover", false)
        };
    }
}
