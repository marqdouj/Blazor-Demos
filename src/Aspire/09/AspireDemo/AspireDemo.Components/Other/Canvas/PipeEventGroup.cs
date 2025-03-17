namespace AspireDemo.Components.Other.Canvas
{
    internal enum PipeEventGroup
    {
        Undefined,
        Weld,
        Valve,
        MetalLoss,
        Geometry,
        General,
    }

    internal static class PipeEventGroupExtensions
    {
        public static bool IsFullPipe(this PipeEventGroup group)
        {
            return group switch
            {
                PipeEventGroup.Weld or PipeEventGroup.Valve => true,
                _ => false,
            };
        }

        public static int ToZIndex(this PipeEventGroup group)
        {
            return group switch
            {
                PipeEventGroup.Undefined => 2,
                PipeEventGroup.Weld => 0,
                PipeEventGroup.Valve => 1,
                PipeEventGroup.MetalLoss => 2,
                PipeEventGroup.Geometry => 2,
                PipeEventGroup.General => 2,
                _ => 2,
            };
        }
    }
}
