using System.IO;

namespace NorthernWinterBeatLibrary.Managers
{
    public interface IImageManager
    {
        string ContainerURL { get; }

        void Upload(string name, Stream file);
    }
}