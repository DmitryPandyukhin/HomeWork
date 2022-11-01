using System.Diagnostics;
using System.IO;
using System.Reflection;

List<FileInformation> fileInformationList = new();

if (args.Count() == 0)
{
    fileInformationList.Add(new FileInformation("OpenFiles.txt", true));
    fileInformationList.Add(new FileInformation("OpenLinks.txt"));
} 
else
{
    if (args[0] == "-f")
    {
        fileInformationList.Add(new FileInformation("OpenFiles.txt", true));
    }
    if (args[0] == "-l")
    {
        fileInformationList.Add(new FileInformation("OpenLinks.txt"));
    }
}

foreach (FileInformation fileInformation in fileInformationList)
    fileInformation.OpenSources();

class FileInformation : IFileInformation
{
    string filePath;
    bool checkSourcesFromFile;
    public FileInformation(string filePath, bool checkSourcesFromFile = false)
    {
        this.filePath = filePath;
        this.checkSourcesFromFile = checkSourcesFromFile;

        if (!File.Exists(filePath))
            File.Create(filePath);
    }

    public void OpenSources()
    {
        List<string> sourceList = File.ReadAllLines(filePath).ToList();
        foreach(string source in sourceList)
        {
            if ((checkSourcesFromFile) && (!File.Exists(source)))
                continue;

            OpenProcess(source);
        }

        void OpenProcess(string source)
        {
            Process process = new();
            process.StartInfo.FileName = source;
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }
    }
}
interface IFileInformation
{
    void OpenSources();
}