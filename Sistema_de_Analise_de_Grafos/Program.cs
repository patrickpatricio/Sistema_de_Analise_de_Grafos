using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Aresta
{
    public int Destino { get; set; }
    public int Peso { get; set; }
}

public class Grafo
{
    private int V;
    private List<List<Aresta>> adj;

    public Grafo(int v)
    {
        V = v;
        adj = new List<List<Aresta>>(v);
        for (int i = 0; i < v; i++)
        {
            adj.Add(new List<Aresta>());
        }
    }
    public int ObterNumeroVertices()
    {
        return V;
    }
    public void AdicionarAresta(int origem, int destino, int peso)
    {
        adj[origem].Add(new Aresta { Destino = destino, Peso = peso });
        adj[destino].Add(new Aresta { Destino = origem, Peso = peso });
    }

    public void DFS(int inicio)
    {
        int[] cor = new int[V]; 
        int[] pred = new int[V];
        int[] d = new int[V];   
        int[] t = new int[V];   
        int tempo = 0;

        const int BRANCO = 0;
        const int AZUL = 1;
        const int VERMELHO = 2;

        for (int i = 0; i < V; i++)
        {
            cor[i] = BRANCO;
            pred[i] = -1;
        }

        Stack<int> S = new Stack<int>();
        S.Push(inicio);

        List<int> ordemVisita = new List<int>();

        while (S.Count > 0)
        {
            int u = S.Peek();

            if (cor[u] == BRANCO)
            {
                cor[u] = AZUL;
                tempo++;
                d[u] = tempo;
                ordemVisita.Add(u + 1);

                foreach (var aresta in adj[u])
                {
                    int v = aresta.Destino;
                    if (cor[v] == BRANCO)
                    {
                        pred[v] = u;
                        S.Push(v);
                    }
                }
            }
            else if (cor[u] == AZUL)
            {
                cor[u] = VERMELHO;
                tempo++;
                t[u] = tempo;
                S.Pop();
            }
            else if (cor[u] == VERMELHO)
            {
                S.Pop();
            }
        }

        Console.WriteLine("Ordem de Visita (DFS - Pilha):");
        Console.WriteLine(string.Join(" -> ", ordemVisita));
        Console.WriteLine();
    }

    public void BFS(int inicio)
    {
        int[] cor = new int[V];
        int[] pred = new int[V];
        int[] d = new int[V];

        const int BRANCO = 0;
        const int AZUL = 1;
        const int VERMELHO = 2;

        for (int i = 0; i < V; i++)
        {
            cor[i] = BRANCO;
            pred[i] = -1;
            d[i] = int.MaxValue; 
        }

        List<int> ordemVisita = new List<int>();

        ExecutarBFS(inicio, cor, pred, d, ordemVisita);

        for (int u = 0; u < V; u++)
        {
            if (cor[u] == BRANCO)
            {
                ExecutarBFS(u, cor, pred, d, ordemVisita);
            }
        }

        Console.WriteLine("Ordem de Visita (BFS):");
        Console.WriteLine(string.Join(" -> ", ordemVisita));
        
        Console.WriteLine("\nDistâncias (Níveis):");
        for(int i = 0; i < V; i++)
        {
            if (d[i] != int.MaxValue)
                Console.WriteLine($"Vértice {i + 1}: Distância {d[i]}");
        }
        Console.WriteLine();
    }

    private void ExecutarBFS(int u, int[] cor, int[] pred, int[] d, List<int> ordemVisita)
    {
        const int BRANCO = 0;
        const int AZUL = 1;
        const int VERMELHO = 2;

        cor[u] = AZUL;      
        d[u] = 0;           
        
        Queue<int> F = new Queue<int>(); 
        F.Enqueue(u);       

        while (F.Count > 0) 
        {
            int e = F.Dequeue(); 
            ordemVisita.Add(e + 1); 

            foreach (var aresta in adj[e]) 
            {
                int v = aresta.Destino;
                if (cor[v] == BRANCO) 
                {
                    cor[v] = AZUL;      
                    d[v] = d[e] + 1;    
                    pred[v] = e;        
                    F.Enqueue(v);       
                }
            }
            cor[e] = VERMELHO; 
        }
    }

    public void Dijkstra(int origem, int destino)
    {
        int[] dist = new int[V];
        int[] pred = new int[V];
        bool[] S = new bool[V];

        for (int i = 0; i < V; i++)
        {
            dist[i] = int.MaxValue;
            pred[i] = -1;
            S[i] = false;
        }

        dist[origem] = 0;
        pred[origem] = origem;

        for (int count = 0; count < V; count++)
        {
            int u = -1;
            int minDist = int.MaxValue;

            for (int i = 0; i < V; i++)
            {
                if (!S[i] && dist[i] < minDist)
                {
                    minDist = dist[i];
                    u = i;
                }
            }

            if (u == -1 || dist[u] == int.MaxValue) break;

            S[u] = true;

            foreach (var aresta in adj[u])
            {
                int v = aresta.Destino;
                int peso = aresta.Peso;

                if (!S[v] && dist[u] != int.MaxValue && dist[u] + peso < dist[v])
                {
                    dist[v] = dist[u] + peso;
                    pred[v] = u;
                }
            }
        }

        Console.WriteLine($"\nMenor custo total de {origem + 1} para {destino + 1}: {dist[destino]}");
        
        if (dist[destino] == int.MaxValue)
        {
            Console.WriteLine("Não existe caminho entre os vértices.");
            return;
        }

        List<int> caminho = new List<int>();
        int atual = destino;
        caminho.Add(atual + 1);

        while (atual != origem)
        {
            atual = pred[atual];
            caminho.Add(atual + 1);
            if (atual == -1) break; 
        }
        caminho.Reverse();

        Console.WriteLine("Caminho encontrado:");
        Console.WriteLine(string.Join(" -> ", caminho));
        Console.WriteLine();
    }

    public void PrimMST()
    {
        HashSet<int> S = new HashSet<int>();
        HashSet<int> N = new HashSet<int>();
        List<Tuple<int, int, int>> T = new List<Tuple<int, int, int>>();

        int i_inicial = 0; 
        S.Add(i_inicial);

        for (int v = 0; v < V; v++)
        {
            if (v != i_inicial)
                N.Add(v);
        }

        while (S.Count != V)
        {
            int minPeso = int.MaxValue;
            int j_sel = -1;
            int k_sel = -1;

            foreach (int j in S)
            {
                foreach (var aresta in adj[j])
                {
                    int k = aresta.Destino;
                    
                    if (N.Contains(k))
                    {
                        if (aresta.Peso < minPeso)
                        {
                            minPeso = aresta.Peso;
                            j_sel = j;
                            k_sel = k;
                        }
                    }
                }
            }

            if (j_sel == -1)
            {
                Console.WriteLine("Grafo desconexo. Não é possível gerar MST completa.");
                break;
            }

            S.Add(k_sel);
            N.Remove(k_sel);
            T.Add(new Tuple<int, int, int>(j_sel, k_sel, minPeso));
        }

        Console.WriteLine("\nÁrvore Geradora Mínima:");
        int custoTotal = 0;
        foreach (var aresta in T)
        {
            Console.WriteLine($"({aresta.Item1 + 1}, {aresta.Item2 + 1}) - Peso: {aresta.Item3}");
            custoTotal += aresta.Item3;
        }
        Console.WriteLine($"\nCusto Total da MST: {custoTotal}");
    }
}

class Program
{
    static int LerInteiro(string mensagem)
    {
        int valor;
        while (true)
        {
            Console.Write(mensagem);
            if (int.TryParse(Console.ReadLine(), out valor))
            {
                return valor;
            }
            Console.WriteLine("Entrada inválida! Digite um número inteiro.");
        }
    }

    static int LerVertice(string mensagem, int qtdVertices)
    {
        int valor;
        while (true)
        {
            Console.Write(mensagem);
            if (int.TryParse(Console.ReadLine(), out valor))
            {
                if (valor >= 1 && valor <= qtdVertices)
                {
                    return valor - 1; 
                }
                Console.WriteLine($"Erro: O vértice {valor} não existe! Digite um valor entre 1 e {qtdVertices}.");
            }
            else
            {
                Console.WriteLine("Entrada inválida! Digite um número inteiro.");
            }
        }
    }
    static Grafo ImportarDoCSV(string caminhoArquivo)
    {
        var arestasTemp = new List<Tuple<int, int, int>>();
        int maiorVertice = 0;
        int linhaAtual = 0;

        if (!File.Exists(caminhoArquivo))
        {
            Console.WriteLine("Erro: O arquivo não foi encontrado. Verifique se o nome e o caminho estão corretos.");
            return null;
        }

        try
        {
            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    linhaAtual++;
                    
                    if (string.IsNullOrWhiteSpace(linha)) continue;
                    string[] partes = linha.Split(new char[] { ';', ',' });
                    if (partes.Length < 3) continue;
                    if (int.TryParse(partes[0], out int origem) &&
                        int.TryParse(partes[1], out int destino) &&
                        int.TryParse(partes[2], out int peso))
                    {
                        arestasTemp.Add(new Tuple<int, int, int>(origem, destino, peso));

                        if (origem > maiorVertice) maiorVertice = origem;
                        if (destino > maiorVertice) maiorVertice = destino;
                    }
                    else
                    {
                        if (linhaAtual > 1) // Ignora aviso para a primeira linha se for cabeçalho
                            Console.WriteLine($"Aviso: Linha {linhaAtual} ignorada (formato inválido).");
                    }
                }
            }

            if (arestasTemp.Count == 0)
            {
                Console.WriteLine("Nenhum dado válido encontrado no CSV.");
                return null;
            }

            Console.WriteLine($"\nArquivo lido com sucesso!");
            Console.WriteLine($"Total de arestas carregadas: {arestasTemp.Count}");
            Console.WriteLine($"Maior vértice encontrado: {maiorVertice}");

            Grafo g = new Grafo(maiorVertice);

            foreach (var a in arestasTemp)
            {
                g.AdicionarAresta(a.Item1 - 1, a.Item2 - 1, a.Item3);
            }

            return g;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
            return null;
        }
    }
    static void Main(string[] args)
    {
        Console.Clear();
            
        Console.WriteLine("\n=============================");
        Console.WriteLine("SISTEMA DE ANÁLISE DE GRAFOS");
        Console.WriteLine("=============================");

        Grafo g = null;
        int v = 0; 
        Console.WriteLine("Escolha o modo de entrada:");
        Console.WriteLine("1. Digitar Manualmente");
        Console.WriteLine("2. Importar de Arquivo .CSV");

        Console.WriteLine("\n===========================================================================================================================================");
        Console.WriteLine("======= ATENÇÃO =======");
        Console.WriteLine("Para a inserção de grafo por arquivo .CSV, o grafo deve ser escito com um vértice em cada linha e seguindo o modelo 'Origem;Destino;Peso'");
        Console.WriteLine("===========================================================================================================================================");
       
        Console.Write("\nOpção: ");
        string modoEntrada = Console.ReadLine();


        if (modoEntrada == "2")
        {
            Console.Clear();

            Console.Write("Digite o nome do arquivo (ex: grafo.csv): ");
            string caminho = Console.ReadLine();
            g = ImportarDoCSV(caminho);

            if (g == null)
            {
                Console.WriteLine("Pressione qualquer tecla para sair...");
                Console.ReadKey();
                return;
            }
            v = g.ObterNumeroVertices();
        }
        else
        {
            v = LerInteiro("Digite o número de vértices: ");
            g = new Grafo(v);

            int e = LerInteiro("Digite o número de arestas: ");

            for (int i = 0; i < e; i++)
            {
                Console.WriteLine($"\n--- Aresta {i + 1} ---");
                
                int origem = LerVertice($"Origem (1 a {v}): ", v);
                int destino = LerVertice($"Destino (1 a {v}): ", v);
                int peso = LerInteiro("Peso: ");

                g.AdicionarAresta(origem, destino, peso);
            }
        }

        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("\n=============================");
            Console.WriteLine("SISTEMA DE ANÁLISE DE GRAFOS");
            Console.WriteLine($"Vértices carregados: {v}");
            Console.WriteLine("=============================");
            Console.WriteLine("1. Travessia em Profundidade (DFS)");
            Console.WriteLine("2. Travessia em Amplitude (BFS)");
            Console.WriteLine("3. Dijkstra (Caminho mínimo)");
            Console.WriteLine("4. Prim (Árvore Geradora Mínima)");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha: ");

            string opcao = Console.ReadLine();

            Console.Clear();
            
            switch (opcao)
            {
                case "1":
                    Console.WriteLine("======= Travessia em Profundidade (DFS) =======");
                    int inicioDFS = LerVertice("Vértice inicial: ", v);
                    g.DFS(inicioDFS);
                    break;
                case "2":
                    Console.WriteLine("======= Travessia em Amplitude (BFS) =======");
                    int inicioBFS = LerVertice("Vértice inicial: ", v);
                    g.BFS(inicioBFS);
                    break;
                case "3":
                    Console.WriteLine("======= Dijkstra (Caminho mínimo) =======");
                    int origemD = LerVertice("Origem: ", v);
                    int destinoD = LerVertice("Destino: ", v);
                    g.Dijkstra(origemD, destinoD);
                    break;
                case "4":
                    Console.WriteLine("======= Prim (Árvore Geradora Mínima) =======");
                    g.PrimMST();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}