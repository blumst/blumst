namespace Image_Processing_Pipeline.Interfaces
{
    public interface IPipelineStep
    {
        Task ExecuteAsync(IPipelineContext context);
    }
}
