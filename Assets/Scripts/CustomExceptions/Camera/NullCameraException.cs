using System;

namespace CustomExceptions.Camera
{
    public class NullCameraException : Exception
    {
        public NullCameraException(string exception) : base(exception)
        {
            
        }
    }
}