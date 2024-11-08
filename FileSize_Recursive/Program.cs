using File_Size_Count;


Files fileSize = new();
string path = Files.InputPath();
Console.WriteLine($"Путь: {path}");
await fileSize.DirToThread(path!);
fileSize.SizeSum();