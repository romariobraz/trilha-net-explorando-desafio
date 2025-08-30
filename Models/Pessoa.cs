namespace DesafioProjetoHospedagem.Models;

public class Pessoa
{
    public Pessoa() { }

    public Pessoa(string nome)
    {
        Nome = nome;
    }

    public Pessoa(string nome, string sobrenome)
    {
        Nome = nome;
        Sobrenome = sobrenome;
    }

    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string NomeCompleto =>
    string.Join(" ", new[] { Nome, Sobrenome }.Where(s => !string.IsNullOrWhiteSpace(s))).ToUpper();
    /*Com isto, posso inst�nciar nome, nome + sobrenome, e caso seja apenas o nome,
    n�o haver� um espa�o vazio ao final*/
}