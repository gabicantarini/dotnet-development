// Pilha
Stack<string> historico = new Stack<string>();

historico.Push("luisdev.com.br");
historico.Push("luisdev.com.br/artigo-1");
historico.Push("luisdev.com.br/cursos-e-mentorias");

Console.WriteLine(historico.Peek());

var paginaAnterior = historico.Pop();

Console.WriteLine($"Página anterior: {paginaAnterior}");
Console.WriteLine($"Página atual: {historico.Peek()}");

// Fila
Queue<string> filaAtendimento = new Queue<string>();

filaAtendimento.Enqueue("A-001");
filaAtendimento.Enqueue("A-002");
filaAtendimento.Enqueue("A-003");

Console.WriteLine($"Próxima da Fila: {filaAtendimento.Peek()}");

var atendido = filaAtendimento.Dequeue();

Console.WriteLine($"Atendido: {atendido}");
Console.WriteLine($"Próximo da Fila: {filaAtendimento.Peek()}");

// Matriz
int[] notas1d = new int[2]; // 0 => 85, 1 => 90

notas1d[0] = 85;
notas1d[1] = 90;

Console.WriteLine("Notas de Estudante na Matriz 1D");

for (var i = 0; i < notas1d.Length; i++)
{
    Console.WriteLine(notas1d[i]);
}

int[,] notas2d = new int[3, 2];

notas2d[0, 0] = 85; // A, Primeiro Bimestre
notas2d[0, 1] = 90; // A, Segundo Bimestre
notas2d[1, 0] = 55; // B, Primeiro Bimestre
notas2d[1, 1] = 100; // B, Segundo Bimestre
notas2d[2, 0] = 60; // C, Primeiro Bimestre
notas2d[2, 1] = 90; // C, Segundo Bimestre

Console.WriteLine("Notas de Estudantes na Matriz 2D");

for (var i = 0; i < notas2d.GetLength(0); i++)
{
    Console.WriteLine($"Estudante {i}");

    for (var j = 0; j < notas2d.GetLength(1); j++)
    {
        Console.Write(notas2d[i, j] + " ");
    }

    Console.WriteLine();
}

// HashSet
HashSet<string> youTubePlaylist = new HashSet<string>();

youTubePlaylist.Add("Video1");
youTubePlaylist.Add("Video2");
youTubePlaylist.Add("Video3");
youTubePlaylist.Add("Video2");

Console.WriteLine($"Tamanho da Playlist: {youTubePlaylist.Count}");

youTubePlaylist.Remove("Video2");

var contemVideo3 = youTubePlaylist.Contains("Video3");

Console.WriteLine($"Contem video 3: {contemVideo3}");

Console.WriteLine($"Tamanho da Playlist: {youTubePlaylist.Count}");

// Dicitonary
Dictionary<string, string> dicionarioTraducao = new Dictionary<string, string>
{
    { "Confirm", "Confirmar" },
    { "Cancel", "Cancelar" },
    { "Home", "Inicio" },
    { "Job Role", "Cargo" }
};

Console.WriteLine($"Tradução de Cancel: {dicionarioTraducao["Cancel"]}");

if (dicionarioTraducao.TryGetValue("Cancel", out var traducao))
{
    Console.WriteLine($"Tradução de Cancel é: {traducao}");
} else
{
    Console.WriteLine("Palavra não encontrada.");
}

Dictionary<string, Dictionary<string, string>> dicionarioTraducaoV2 = new Dictionary<string, Dictionary<string, string>>
{
    { "pt", new Dictionary<string, string> {
        { "confirm", "Confirmar" },
        { "cancel", "Cancelar" }
    }},
    { "en", new Dictionary<string, string>{
        { "confirm", "Confirm" },
        { "cancel", "Cancel" }
    } },
    { "es", new Dictionary<string, string>{
        { "confirm", "Confirmar" },
        { "cancel", "Cancelar" }
    }}
};

var selectedLanguage = "pt";

Console.WriteLine($"confirm in Portuguese is {dicionarioTraducaoV2[selectedLanguage]["confirm"]}");


// Linked List
LinkedList<string> playlist = new LinkedList<string>();

playlist.AddLast("Pretty Fly");
playlist.AddLast("The Kids Aren't Alright");
playlist.AddLast("You're Gonna Go Far, Kid");

foreach (var musica in playlist)
{
    Console.WriteLine(musica);
}

playlist.Remove("The Kids Aren't Alright");

playlist.AddFirst("Gone away");

foreach (var musica in playlist)
{
    Console.WriteLine(musica);
}

Console.Read();