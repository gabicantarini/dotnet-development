# Índice CantaBank

## Strings

- No C# e .NET strings são imutáveis;
- Podemos criar uma nova string, a partir da porção de outra, com o método Substring.

## Métodos e Propriedades

- Método IndexOf;
- Método estático IsNullOrEmpty;
- A palavra reservada string é, na verdade, o tipo String;
- A palavra reservada int é, na verdade, o tipo Int32;
- A palavra reservada double é, na verdade, o tipo Double;
- A sobrecarga String::IndexOf(string);
- A propriedade String::Length.

## Métodos de Maninpulação

- O IndexOf retorna sempre o índice da primeira ocorrência que buscamos;
- O método Remove(char);
- O método Remove(char, int);
- Seleção quadrada no Visual Studio com o Shift + Alt;
- O IndexOf retorna -1 quando o valor não é encontrado.
- O método Replace;
- O método ToUpper e ToLower;
- O método StartsWith, EndsWith e Contains.

## Expressões Regulares

- Grupos de caracteres, como [0123456789];
- Intervalos de caracteres, como [0-9];
- Quantificadores: {4,5}, {4} e {?};
- O método estático Regex.IsMatch e Regex.Match.

## Classe Object

- Todos os tipos derivam de Object;
- O método ToString;
- Como usar interpolações de string (string interpolation);
- Cast com a palavra reservada as.



## Array e Tipos Genéricos

### Array

- Sintaxe de criação de variáveis de array;
- O valor padrão é utilizado em todas as posições de um array ao ser criado;
- A propriedade Length dos arrays;
- A sintaxe de inicialização de array new int[] { 5, 12, 64 }.

### Lista

- Mais lógica com arrays;
- Argumentos opcionais;
- Argumentos nomeados;

### Método Remover

- Mais lógica com arrays;
- Como implementar o método Remover;
- Como iterar por intervalos específicos de um array.

### Indexadores e Argumentos Params

- Como criar indexadores;
- Como criar um argumento params;
- O foreach.

### Tipos Genéricos

- O que são classes genéricas;
- Como criar classes genéricas;
- null em tipos genéricos.

## List, Lambda, Linq

### List e Métodos de Extensão

- O tipo List<T> do .Net;
- Como criar métodos de extensão.

### Método de Extensão Genérico
  
- Como criar métodos genéricos;
- Como criar métodos de extensão genéricos;
- Como o compilador encontra os métodos de extensão.
  
### Var e Método Sort

- A inferência de variáveis locais com o var;
- Sintaxe de inicialização de listas;
- O método Sort.
  
### I Comparable e I Comparer
  
- Como usar a interface IComparable;
- Como usar a interface IComparer<T>;
- A sobrecarga List<T>::Sort(IComparer<T>).
  
### OrderBy e Expressões Lambda
  
- Expressões lambdas;
- O método OrderBy.
  
### Linq e Operador Where
  
- O método Where;
- A interface IEnumerable<T>;
- Os métodos de extensão da classe Enumerable;
- Introdução ao Linq.
