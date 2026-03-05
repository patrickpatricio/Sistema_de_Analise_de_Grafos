### 📊 Sistema de Análise de Grafos
Solução computacional robusta para análise de grafos ponderados e não direcionados, desenvolvida em C# e .NET 9.0.

## 📋 Sobre o Projeto
Este sistema foi desenvolvido como parte da disciplina de Algoritmos em Grafos. O software implementa algoritmos clássicos de teoria dos grafos com foco em integridade de dados e experiência do usuário, utilizando uma camada de validação que impede o encerramento por erros de entrada.

## 🛠️ Algoritmos Implementados
O sistema oferece quatro funcionalidades principais de travessia e otimização:
  - Travessia em Profundidade (DFS): Explora o grafo seguindo o caminho mais profundo possível, registrando tempos de descoberta e término para cada vértice;
  - Travessia em Amplitude (BFS): Descobre todos os vértices adjacentes a um nível antes de progredir, ideal para calcular distâncias em saltos;
  - Algoritmo de Dijkstra: Calcula o caminho de menor custo entre um vértice raiz e todos os demais vértices do grafo;
  - Algoritmo de Prim: Gera uma Árvore Geradora Mínima (AGM) utilizando uma abordagem gulosa para conectar todos os vértices com o menor peso total.

## 🚀 Funcionalidades e Uso
O sistema permite dois modos de entrada de dados:
  - Digitação Manual: O usuário informa o número de vértices, arestas e popula cada conexão individualmente;
  - Importação via CSV: Leitura automatizada de arquivos estruturados no modelo Origem; Destino; Peso;
### Fluxo de Execução
Selecione o modo de entrada de dados => Visualize os vértices carregados no menu principal => Escolha o algoritmo desejado para análise.

<img width="709" height="142" alt="image" src="https://github.com/user-attachments/assets/717c4d0c-8e5f-48ea-8803-fc63d05e90f9" />
<img width="573" height="238" alt="image" src="https://github.com/user-attachments/assets/cc4ec971-441f-4151-ac38-d1e9b5f18476" />

## 🏗️ Estrutura Técnica
  - Representação: O grafo é armazenado em uma Lista de Adjacência (List<List<Aresta>>), otimizando o consumo de memória;
  <img width="409" height="240" alt="image" src="https://github.com/user-attachments/assets/969a9c11-275a-4d51-8672-18f9426dc6ed" />
  
  - Robustez: Métodos auxiliares como LerVertice validam se as entradas do usuário estão dentro dos limites do grafo, convertendo a lógica base 1 (usuário) para base 0 (sistema);
  <img width="525" height="223" alt="image" src="https://github.com/user-attachments/assets/67f85b80-94b8-4b2a-bd39-7644c3660366" />
