namespace Image_Processing_Pipeline.Interfaces
{
    public interface IStepPrototype
    {
        IPipelineStep Clone();
    }
}
