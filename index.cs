using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;

namespace ExercApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SISTEMA DE GERENCIAMENTO DE ESTOQUE - LOJA TECHFUTURE
            //Autores: Cesar Fonseca e Murilo Coutinho

            //Aqui vamos criar as listas pra armazenar cada tipo, produto, quantidade etc...
            List<string> produtos = new List<string>();
            List<int> quantidades = new List<int>();
            List<double> precos = new List<double>();
            List<string> segmentos = new List<string>();

            //Criando caminho para salvar o arquivo
            string caminhoArquivo = "estoque.csv";

            //Carregar os arquivos
            //Se o File existe ele vai percorrer todo o arquivo
            if (File.Exists(caminhoArquivo))
            {
                //Criando um array para pecorrer e ler todas as linhas do arquivo
                string[] linhas = File.ReadAllLines(caminhoArquivo);
                foreach (string linha in linhas)
                {
                    //Vai dividir cada linha em partes (usando exemplo dos exercícios resolvidos)
                    string[] partes = linha.Split(',');
                    //Pra garantir que não tenha erros, falamos "se" tiver quatro partes, faça o que está no código.
                    if (partes.Length == 4)
                    {
                        produtos.Add(partes[0]);
                        quantidades.Add(int.Parse(partes[1]));
                        precos.Add(double.Parse(partes[2]));
                        segmentos.Add(partes[3]);
                    }
                }
            }

            //Criando loop para o MENU PRINCIPAL
            bool sair = false;

            while (!sair)
            {
                Clear();

                // Cabeçalho
                WriteLine("===========================================");
                WriteLine("               LOJA TECHFUTURE");
                WriteLine("===========================================\n");

                WriteLine("   Selecione uma opção abaixo:\n");
                WriteLine("  [1] Cadastrar novo produto");
                WriteLine("  [2] Excluir produto do estoque");
                WriteLine("  [3] Mostrar estoque completo");
                WriteLine("  [4] Buscar produto por nome");
                WriteLine("  [5] Exibir valor total do estoque");
                WriteLine("  [6] Exibir produtos e quantidades");
                WriteLine("  [7] Pesquisar produtos por segmento");
                WriteLine("  [8] Salvar alterações no arquivo");
                WriteLine("  [9] Sair do sistema\n");

                Write("Digite o número da opção desejada: ");
                string opcao = ReadLine();

                //Criamos esse switch para selecionar a opção desejada
                switch (opcao)
                {
                    //CAdastro de novo produto
                    case "1":
                    inicio:
                        //Clear colocado para limpar o menu e mostrar apenas os itens que estamos adicionando
                        Clear();
                        WriteLine("    CADASTRO DE NOVO PRODUTO    \n");

                        Write("Digite o nome do produto: ");
                        string produtoInserido = ReadLine();

                        //Fizemos esse if para evitar cadastrar o mesmo item mais de uma vez
                        if (produtos.Contains(produtoInserido))
                        {
                            WriteLine("\n  Este produto já está cadastrado!");
                            Write("Deseja tentar outro nome? (S/N): ");
                            string opcaoNome = ReadLine();

                            //Colocamos essse ToUpper para caso a pessoa digite o "s" minúsculo
                            if (opcaoNome.ToUpper() == "S")
                                goto inicio;
                        }
                        //Se não existe, aí faz todo o cadastro do produto
                        else
                        {
                            produtos.Add(produtoInserido);

                            Write("Digite a quantidade em estoque: ");
                            int quantidadeInserida = int.Parse(ReadLine());
                            quantidades.Add(quantidadeInserida);

                            Write("Digite o preço do produto (R$): ");
                            double precoInserido = double.Parse(ReadLine());
                            precos.Add(precoInserido);

                            Write("Digite o segmento do produto: ");
                            string segmentoInserido = ReadLine();
                            segmentos.Add(segmentoInserido);

                            WriteLine($"\n Produto '{produtoInserido}' cadastrado com sucesso!");

                            Write("\nDeseja cadastrar outro produto? (S/N): ");
                            string simOuNao = ReadLine();

                            if (simOuNao.ToUpper() == "S")
                                goto inicio;
                        }
                        break;

                    //Exclusão de produto
                    case "2":
                        Clear();
                        WriteLine("    EXCLUSÃO DE PRODUTO    \n");

                        //Aqui ele conta os produtos, se não tem nenhum, ou seja, contou zero, ele escreve esse WriteLine
                        if (produtos.Count == 0)
                        {
                            WriteLine("  Nenhum produto cadastrado ainda.");
                            ReadKey();
                        }
                        //Se contar 1 ou mais, ai ele mostra os produtos "excluíveis" e faz o processo para excluir
                        else
                        {
                            WriteLine("Produtos cadastrados:\n");
                            //Vai percorrer item por item dentro da "sacola" produtos
                            foreach (string item in produtos)
                                WriteLine($"- {item}");

                            Write("\nDigite o nome do produto que deseja excluir: ");
                            string produtoParaExcluir = ReadLine();
                            //Se o produto digitado existe, ele vai fazer o processo de exclusão
                            if (produtos.Contains(produtoParaExcluir))
                            {
                                //Aqui criamos uma variável inteira index para o indexof exluir em cada "sacola" o produto digitado
                                int index = produtos.IndexOf(produtoParaExcluir);
                                produtos.RemoveAt(index);
                                quantidades.RemoveAt(index);
                                precos.RemoveAt(index);
                                segmentos.RemoveAt(index);

                                WriteLine($"\n Produto '{produtoParaExcluir}' removido com sucesso!");
                                ReadKey();
                            }
                            else
                            {
                                //Se o produto digitado não existe, ele mostra essa mensagem e volta para o menu inicial
                                WriteLine("X Produto não encontrado!");
                                ReadKey();
                            }
                        }
                        break;

                        //Neste case iremos pegar o relatório de todo o estoque dos produtos
                    case "3":
                        Clear();
                        WriteLine("    RELATÓRIO COMPLETO DO ESTOQUE    \n");
                        //Aqui é a mesma função do caso 2, se não tiver nenhum produto cadastrado, irá mostrar isso
                        if (produtos.Count == 0)
                        {
                            WriteLine("  Nenhum produto cadastrado.");
                        }
                        //Se existir produto cadastrado irá mostrar os dados de cada um
                        else
                        {
                            WriteLine("Produto | Quantidade | Preço (R$) | Segmento");
                            WriteLine("-----------------------------------------------");
                            //Neste loop iremos mostrar o índice começando com 0 de cada lista até contar o último índice 
                            for (int i = 0; i < produtos.Count; i++)
                            {
                                WriteLine($"{produtos[i]} | {quantidades[i]} | {precos[i]:C2} | {segmentos[i]}");
                            }
                        }
                        WriteLine("\nPressione ENTER para voltar ao menu...");
                        ReadKey();
                        break;

                    //Neste case, iremos buscar os produtos por nome
                    case "4":
                        Clear();
                        WriteLine("   BUSCA DE PRODUTO   \n");
                        Write("Digite parte do nome do produto: ");
                        string termoBusca = ReadLine().ToLower(); //Aqui criamos uma variável para o usuário digitar e procurar um produto que contenha os caracteres digitados 
                        bool encontrouProduto = false; //Aqui criamos um loop e declaramos ele falso para ele se manter no loop enquanto o for continuar sendo falso

                        for (int i = 0; i < produtos.Count; i++) //Novamente usamos o for para contar até o último item
                        {
                            if (produtos[i].ToLower().Contains(termoBusca)) //Pega o termo digitado, verifica se contém o mesmo, deixa tudo em letra minuscula, tudo isso procurando na lista "produtos"
                            {
                                WriteLine($"\n {produtos[i]} | {quantidades[i]} un. | {precos[i]:C2}");
                                encontrouProduto = true; //Aqui o loop será encerrado
                            }
                        }
                        //Aqui mostra uma mensagem caso o produto digitado não exista na lista "produtos"
                        if (!encontrouProduto)
                            WriteLine("\n X Nenhum produto encontrado!");

                        WriteLine("\nPressione ENTER para retornar ao menu...");
                        ReadKey();
                        break;
                        //Neste case, iremos mostrar o valor total do estoque
                    case "5":
                        Clear();
                        WriteLine("    VALOR TOTAL DO ESTOQUE   \n");

                        double valorTotal = 0; //Criamos uma variável para iniciar uma contagem
                        for (int i = 0; i < produtos.Count; i++)
                        {
                            //A variável subtotal foi criada para fazer o cálculo das quantidades * o preço
                            double subtotal = quantidades[i] * precos[i];
                            //Aqui passamos a usar a variável de contagem citada acima, esse += faz com que se some cada subtotal, dando o valor total como resposta
                            valorTotal += subtotal;                                   //O C2 faz com que tenha apenas duas casas após a vírgula
                            WriteLine($"{produtos[i]} - {quantidades[i]} un. x {precos[i]:C2} = {subtotal:C2}");
                        }

                        WriteLine($"\n Valor total do estoque: {valorTotal:C2}");

                        WriteLine("\nPressione ENTER para retornar ao menu...");
                        ReadKey();
                        break;

                        //Tabela que mostra produtos e quantidades apenas
                    case "6":
                        Clear();
                        WriteLine("    PRODUTOS E QUANTIDADES \n");

                        //Novamente criamos uma variável para CONTAR
                        int totalItens = 0;
                        for (int i = 0; i < produtos.Count; i++)
                        {
                            WriteLine($" {produtos[i]} - {quantidades[i]} unidade(s)");
                            //Aqui, assim como no case anterior, ele vai, a partir do totalItens iniciado em 0, contar quantidade por quantidade e ir somando
                            totalItens += quantidades[i];
                        }

                        WriteLine($"\n Quantidade total de itens no estoque: {totalItens}");
                        WriteLine("\nPressione ENTER para retornar ao menu...");
                        ReadKey();
                        break;
                        //Aqui é basicamente igual o case 4, mas, em vez de pesquisar na lista produto, pesquisa na lista segmento
                    case "7":
                        Clear();
                        WriteLine("    PESQUISA POR SEGMENTO \n");

                        //Novamente usamos o Count para previnir erros caso o item pesquisado não exista
                        if (segmentos.Count == 0)
                        {
                            WriteLine("  Nenhum produto cadastrado ainda.");
                            ReadKey();
                        }
                        else
                        {
                            Write("Digite o nome do segmento: ");
                            //Aqui criamos uma variável de pesquisa, assim como no case 4 criamos a variável termoBusca, transformamos todos os caracteres em minúsculo para prevenir erros também.
                            string termoSegmento = ReadLine().ToLower();
                            //Loop criado!
                            bool encontrouSegmento = false;

                            for (int i = 0; i < segmentos.Count; i++)
                            {
                                //Mesmo princípio do case 4, procura o termoSegmento digitado, transforma ele em lower e o procura na "sacola" segmentos
                                if (segmentos[i].ToLower().Contains(termoSegmento))
                                {
                                    WriteLine($"\n {produtos[i]} | {quantidades[i]} un. | {precos[i]:C2} | {segmentos[i]}");
                                    //Fim do loop!
                                    encontrouSegmento = true;
                                }
                            }

                            if (!encontrouSegmento)
                                WriteLine("\nX Nenhum produto encontrado nesse segmento.");

                            WriteLine("\nPressione ENTER para retornar ao menu...");
                            ReadKey();
                        }
                        break;
                        //Case para salvar
                    case "8":
                        Clear();
                        List<string> linhasParaSalvar = new List<string>();
                        for (int i = 0; i < produtos.Count; i++)
                        {
                            linhasParaSalvar.Add($"{produtos[i]},{quantidades[i]},{precos[i]},{segmentos[i]}");
                        }
                        //Faz ele salvar todas as linhas
                        File.WriteAllLines(caminhoArquivo, linhasParaSalvar);

                        WriteLine(" Dados salvos com sucesso no arquivo 'estoque.csv'!");
                        ReadKey();
                        break;
                        //A parte mais simples, fazer sair do programa
                    case "9":
                        Clear();
                        WriteLine("Encerrando o programa...");
                        sair = true;
                        break;
                        //Caso não escolham nenhuma das opções, ele mostra essa mensagem e volta pro menu inicial
                    default:
                        WriteLine("\nX Opção inválida! Tente novamente.");
                        ReadKey();
                        break;
                }
            }
        }
    }
}


