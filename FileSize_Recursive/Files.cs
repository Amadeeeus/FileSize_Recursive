namespace File_Size_Count;


public class Files
{
    private long _filesSize = 1;
    
    public Files()
    {
        Console.WriteLine("Введите путь: ");
    }

    public async Task DirToThread(string path)
    {
        Console.WriteLine($"Путь: {path}");
        var dir = new DirectoryInfo(path);
        var tasks = new List<Task>();
        foreach (var subs in dir.GetDirectories())
        {
                tasks.Add(Task.Run(() => GetFileSum(subs.FullName)));
                tasks.Add(DirToThread(subs.FullName));
        }
        await Task.WhenAll(tasks);
    }

    public static string InputPath()
    {
        string? path = Console.ReadLine();
        try
        {
            if (path == null)
            {
                throw new NullReferenceException("Путь не может быть null");
            }

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException("Директории не существует");
            }
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (DirectoryNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        return path!;
    }
    
    public long SizeSum()
    {
        long sum = 1;
            if (_filesSize >= 1073741824)
            {
                sum = _filesSize / 1073741824;
                Console.WriteLine($"Общая сумма - {sum}GB");
                return sum;
            }

            if (_filesSize >= 1048576)
            {
                sum = _filesSize / 1048576;
                Console.WriteLine($"Общая сумма - {sum}MB");
                return sum;
            }

            if (_filesSize >= 1024)
            {
                sum = _filesSize / 1024;
                Console.WriteLine($"Общая сумма - {sum}КБ");
                return sum;
            }
            Console.WriteLine($"Общая сумма - {sum}Б");
        return sum;
    }
    
    private void GetFileSum(string path)
    {
            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles().Select(x => x.Length).Sum();
            Interlocked.Add(ref _filesSize, files);
    }
}
