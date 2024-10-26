namespace csh_io_copy_file
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool verbose = false;
            bool overwrite = false;
            bool quiet = false;
            string sourcePath = null;
            string destinationPath = null;

            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "-v":
                    case "--verbose":
                        verbose = true;
                        break;
                    case "-o":
                    case "--overwrite":
                        overwrite = true;
                        break;
                    case "-q":
                    case "--quiet":
                        quiet = true;
                        break;
                    default:
                        if (sourcePath == null)
                        {
                            sourcePath = arg;
                        }
                        else if (destinationPath == null)
                        {
                            destinationPath = arg;
                        }
                        break;
                }
            }

            if (sourcePath == null || destinationPath == null)
            {
                if (!quiet)
                {
                    Console.WriteLine("Usage: cp <source> <destination> [-v | --verbose] [-o | --overwrite] [-q | --quiet]");
                    Console.WriteLine("Example: cp C:\\source.txt D:\\destination.txt -v -o");
                }
                return;
            }

            if (!File.Exists(sourcePath))
            {
                if (!quiet) Console.WriteLine("Error: The source file does not exist.");
                return;
            }

            if (Directory.Exists(destinationPath))
            {
                destinationPath = Path.Combine(destinationPath, Path.GetFileName(sourcePath));
            }

            if (File.Exists(destinationPath) && !overwrite)
            {
                if (!quiet) Console.WriteLine("Error: The destination file already exists. Use -o to overwrite.");
                return;
            }

            try
            {
                if (verbose && !quiet)
                {
                    Console.WriteLine("Starting copy process...");
                    Console.WriteLine($"Source: {sourcePath}");
                    Console.WriteLine($"Destination: {destinationPath}");
                    Console.WriteLine($"File '{sourcePath}' successfully copied to '{destinationPath}'.");
                }

                File.Copy(sourcePath, destinationPath, overwrite);
            }
            catch (UnauthorizedAccessException)
            {
                if (!quiet) Console.WriteLine("Error: Insufficient permissions to copy the file.");
            }
            catch (IOException ex)
            {
                if (!quiet) Console.WriteLine("Error while copying the file: " + ex.Message);
            }
            catch (Exception ex)
            {
                if (!quiet) Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
