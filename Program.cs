using Nathan_2.src.Despesas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nathan_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Tipos de despesas
            List<string> tiposDespesas = new List<string>();
            tiposDespesas.Add("Mercado");
            tiposDespesas.Add("Gasolina");
            tiposDespesas.Add("Saude");
            //---


            List<Conta> contas = new List<Conta>();
            int countContas = 1;
            List<Meta> metas = new List<Meta>();

            Cabecalho();

        NovaConta:
            Console.Write("Digite o nome da conta: ");
            string contaNome = Console.ReadLine();
            Console.Write("Escreva o saldo da Conta: ");
            float contaSaldo = float.Parse(Console.ReadLine());
            Console.Write("Escreva o mês que vai controlar: ");
            string contaMes = Console.ReadLine();

            Console.Write("Deseja adiconar alguma meta? S/N ");
            string resposta = Console.ReadLine();
            if (resposta.ToUpper() == "S")
            {
                Console.Write("Qual o objetivo da Meta? ");
                string nomeDescricao = Console.ReadLine();
                Console.Write("Qual o valor que deseja econimizar? ");
                int valorMeta = int.Parse(Console.ReadLine());
                Console.Write("Qual o Prazo da sua meta? ");
                string diaPrazo = Console.ReadLine();

                Console.Clear();
                Cabecalho();

                Meta novaMeta = new Meta(nomeDescricao, valorMeta, diaPrazo);
                metas.Add(novaMeta);
            }



            Conta novaConta = new Conta(contaNome, contaSaldo, contaMes, countContas);
            contas.Add(novaConta);
            countContas++;
            Console.WriteLine("\n* Conta adicionada com sucesso *\n");

            Console.Write("Deseja adicionar uma nova conta? [S/N] ");
            string respostas = Console.ReadLine();

            if (respostas.ToUpper() == "S")
            {
                Console.Clear();
                Cabecalho();
                goto NovaConta;
            }

            Console.Write("Pronto, agora selecione a conta que você deseja acessar");
            Console.ReadKey(true);
            Console.Clear();

        //Selecionar Conta
        ReturnConta:
            Cabecalho();
            Console.WriteLine("- Contas disponíveis:");
            foreach (Conta conta in contas)
            {
                Console.WriteLine("\t" + conta.getID() + " - " + conta.getNome() + "");
            }
            Console.Write("\nDigite o ID da conta que você deseja acessar: ");
            int id_Acessar = int.Parse(Console.ReadLine());

            Conta contaSelect = null;
            foreach (Conta conta in contas)
            {
                if (conta.getID() == id_Acessar)
                {
                    contaSelect = conta;
                    break;
                }
            }
            if (contaSelect == null)
            {
                Console.Write("Você digitou ID inválido!");
                Console.ReadKey(true);
                Console.Clear();
                goto ReturnConta;
            }

            Console.Write("Pronto, vamos adionar as Despesas");
            Console.ReadKey(true);
            Console.Clear();

        //Despesas
        ReturnDespesas:
            Cabecalho();
            Console.WriteLine("Conta selecionada: " + contaSelect.getNome() + "\n");
            Console.Write("Qual foi o dia da compra? ");
            int diaCompra = int.Parse(Console.ReadLine());
            Console.WriteLine("\nSelecione o tipo de despesa: ");
            foreach (string name in tiposDespesas)
            {
                Console.WriteLine("\t- " + name + "");
            }
            Console.Write("Digite qual despesa você deseja adicionar: ");
            string despesaSelect = Console.ReadLine();
            bool exists = false;
            foreach (string name in tiposDespesas)
            {
                if (name.ToUpper() == despesaSelect.ToUpper())
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                Console.WriteLine("\nTipo de despesa não encontrado");
                Console.ReadKey(true);
                Console.Clear();
                goto ReturnDespesas;
            }

            if (despesaSelect.ToUpper() == "MERCADO")
            {
                Console.Write("Qual foi o produto? ");
                string produto = Console.ReadLine();
                Console.Write("Qual o valor do produto? ");
                float valor = float.Parse(Console.ReadLine());
                Console.Write("Qual foi a quantidade? ");
                int quantidade = int.Parse(Console.ReadLine());

                Mercado compra = new Mercado(despesaSelect, diaCompra, valor, produto, quantidade);
                contaSelect.AddDespesa(compra);
            }
            else if (despesaSelect.ToUpper() == "GASOLINA")
            {
                Console.Write("Qual o valor da gasolina? ");
                float valor = float.Parse(Console.ReadLine());
                Console.Write("Quantos litros? ");
                int litros = int.Parse(Console.ReadLine());
                Console.Write("Qual a quangtidade de gasolina ");
                int quantidade = int.Parse(Console.ReadLine());
                Gasolina compra = new Gasolina(despesaSelect, diaCompra, valor, litros, quantidade);
                contaSelect.AddDespesa(compra);
            }
            else if (despesaSelect.ToUpper() == "SAUDE")
            {
                Console.Write("Qual foi o remédio? ");
                string remedio = Console.ReadLine();
                Console.Write("Qual o valor do remédio? ");
                int valor = int.Parse(Console.ReadLine());
                Console.Write("Quantas unidades do remédio? ");
                int quantidade = int.Parse(Console.ReadLine());

                Saude compra = new Saude(despesaSelect, diaCompra, remedio, quantidade, valor);
                contaSelect.AddDespesa(compra);
            }
            Console.Write("\nDeseja adicionar mais despesas? (S/N)");
            string decisao = Console.ReadLine();
            if (decisao.ToUpper() == "S")
            {
                Console.Clear();
                goto ReturnDespesas;
            }
            else
            {
                EscreverRelatorio();
                Console.ReadKey();

                void EscreverRelatorio()
                {
                    List<Despesa> despesas = novaConta.GetDespesas();

                    try
                    {
                        string path = "C:\\Users\\User\\Desktop\\Apresentação\\" + novaConta.getNome() + ".txt";
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            sw.Write("-------------[Compra do Mês de " + novaConta.getMes() + "]-------------\r\n");
                            sw.Write("\r\n Dono da conta: " + novaConta.getNome());
                            sw.Write("\r\n Saldo inicial: " + novaConta.getSaldo());

                            float[] diascomprados = new float[31];

                            float somavalor = 0;
                            for (int i = 0; i < despesas.Count; i++)
                            {
                                Despesa despesa = despesas[i];
                                int diacompra = despesa.getDia();
                                Console.WriteLine("dia da compra=" + diacompra);
                                diascomprados[diacompra - 1] += despesa.multValor();

                                // int value = produtosValues[i];
                                sw.Write("\r\n Comprado no dia " + despesa.getDia() + "\r\n Produto:" + despesa.getNome() + "\r\n Preço:R$" + despesa.getValor() + ",00 \r\n Quantidade:" + despesa.getQuant() + "\r\n Total:R$" + despesa.multValor() + ",00 \r\n");
                                somavalor += diascomprados[diacompra - 1];
                            }

                          
                            sw.Write("\r\n Gastos totais por dia:");

                            for (int i = 0; i < diascomprados.Length; i++)
                            {
                                if (diascomprados[i] > 0)
                                {
                                    sw.Write("\r\n Gastos do dia " + (i + 1) + " : R$" + diascomprados[i] + ",00");
                                }
                            }
                            Console.ReadKey();

                            sw.Write("\r\n Gasto total: R$" + (somavalor) + ",00");
                            sw.Write("\r\n Saldo atual: R$" + (novaConta.getSaldo() - somavalor) + ",00 \r\n");

                            Console.WriteLine("\nO relatório foi escrito com sucesso! ");
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Não foi possível realizar a escrita do arquivo.");
                        Console.ReadKey(true);
                    }
                }
            }

            void Cabecalho()
            {
                Console.WriteLine("\nBem-vindo ao Daily Control! \nAqui você pode registrar e visualizar seus gastos\n");
            }
        }
    }

    class Conta
    {
        private int id;
        private string nome;
        private float saldo;
        private string mes;
        List<Despesa> despesas = new List<Despesa>();

        public Conta(string contaNome, float contaSaldo, string contaMes, int cID)
        {
            this.nome = contaNome;
            this.saldo = contaSaldo;
            this.mes = contaMes;
            this.id = cID;
        }
        public List<Despesa> GetDespesas()
        {
            return despesas;
        }
        public void AddDespesa(Despesa despesa)
        {
            despesas.Add(despesa);
        }
        public string getNome()
        {
            return nome;
        }
        public int getID()
        {
            return this.id;
        }
        public float getSaldo()
        {
            return saldo;
        }
        public string getMes()
        {
            return mes;
        }
        public void mostrarSaldo(float value)
        {
            this.saldo += value;
        }
    }

    class Meta
    {
        public string descricao;
        public int valor;
        public string prazo;

        public Meta(string nomeDescricao, int valorMeta, string diaPrazo)
        {
            this.descricao = nomeDescricao;
            this.valor = valorMeta;
            this.prazo = diaPrazo;
        }
        public string getDescricaoMeta()
        {
            return descricao;
        }
        public int getValorMeta()
        {
            return valor;
        }
        public string getDiaPrazo()
        {
            return prazo;
        }


        // método = Saber quanto faltou pra alcançar a meta
        // se valor do saldo > meta = alcançou a meta
        // se valor do saldo < meta = não alcancou
    }

}