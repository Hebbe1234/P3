using System.IO;

namespace NorthernWinterBeatLibrary.Managers
{
    public interface IBlobStorageManager
    {
        string ContainerURL { get; }

        void Upload(string name, Stream file);
    }
}